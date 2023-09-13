using System.Diagnostics;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Module.Amqp;
using Module.AspNetCore;
using Module.AspNetCore.Extensions;
using Module.AspNetCore.Infrastructure;
using Module.AspNetCore.Metrics;
using Module.Gql.Federation.AspNetCore;
using Module.Logging.Core;
using Module.Logging.Core.Settings;
using Module.Tracing.BasicTracer;
using Module.Tracing.Contract.Tracer;
using Module.Tracing.Logging;
using Service.Biz.UiRestrictions.BLL;
using Service.Biz.UiRestrictions.BLL.Amqp;
using Service.Biz.UiRestrictions.DAL;
using Service.Biz.UiRestrictions.GraphQL;
using Service.Biz.UiRestrictions.Host.Auth;

namespace Service.Biz.UiRestrictions.Host;

public class Startup
{
    public const string ServiceName = "Service.Biz.UiRestrictions";
    public const string ServiceNameForBizGqlSchemaRegistry = "biz.ui.restrictions";
    
    private readonly IWebHostEnvironment hostingEnvironment;
    private readonly IConfiguration configuration;
    private ILogger? logger;
    
    public Startup(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
    {
        this.configuration = configuration;
        this.hostingEnvironment = hostingEnvironment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var loggerFactory = LoggingManager.Factory;
        services.AddSingleton(loggerFactory);

        var influxDbUri = configuration.GetValue<Uri>("Logging:InfluxDb:uri");
        if (influxDbUri != null)
            LoggingSettings.SetLoggingSettings(influxDbUri.Host, influxDbUri.Port, true);

        logger = loggerFactory.GetLogger(typeof(Startup));
        logger.Debug("Register services..");
        
        AppDomain.CurrentDomain.UnhandledException += (_, args) =>
            logger.Error("Global handler catch unhandled exception",
                (args.ExceptionObject as AggregateException)?.GetBaseException() ??
                args.ExceptionObject as Exception);
        
        var tracer = Tracer.Factory.CreateTracer(configurator => configurator.UseLogging(logger)).ForComponent(ServiceName);
        services.AddSingleton(tracer);

        services.AddStorage(configuration, hostingEnvironment.IsTest());
        services.AddServices(configuration);
        
        services.AddApolloFederation<UiRestrictionsFederationEntityType, UiRestrictionsSchema>();

        services.AddMvc();
        services.AddHttpContextAccessor();
        
        services.AddCustomHealthChecks();
        services.AddCustomAuthentication();
        services.AddSwaggerGen();
        services.AddControllers(opt=> opt.WithLoggingAndTracing(LoggingManager.Factory, tracer, 
                new RestOperationNameResolver()))
            .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddSingleton(_ =>
            Tracer.Factory.CreateTracer(configurator => configurator.UseLogging(logger))
                .ForComponent(ServiceName));
        services.AddMessageBus(configuration.GetConnectionString("rabbit")!);

        services.ConfigureStartupDependencies((sp, deps) =>
        {
            var database = sp.GetRequiredService<IDbManager>();
            if (hostingEnvironment.IsTest())
                deps.Add("Create Db", database.CreateDatabaseIfNotExistsAsync);
            else
                deps.Add("Check Db", database.IsServerAvailableAsync);
            deps.Add("Run migrations", database.MigrationAllUpAsync);
            deps.Add("Register scheme", async _ 
                => await SchemeRegister.IsSchemeRegistered(sp, ServiceNameForBizGqlSchemaRegistry, GetGQLSchemaVersion()));
            deps.Add("Check RabbitMq", _ => StartupChecks.IsRabbitMqAvailable(configuration.GetConnectionString("rabbit")));
            deps.Add("Init Message Bus", _ => sp.GetRequiredService<IMessageBus>().InitAsync());
        });
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseHttpMetrics(new HttpRequestMetricsParameters(ServiceName, GetServiceVersion()));
        app.UseExceptionHandler(options =>
        {
            options.Run(ExceptionHandler);
        });
        app.UseRouting();
        app.UseAuthentication();
        
        if (hostingEnvironment.IsTest())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "biz-ui-restrictions API v1");
                options.RoutePrefix = string.Empty;
            });

        }
        app.UseCustomHealthChecks();
        app.UseRequestLogging();
        app.UseApolloFederation();
        app.UseEndpoints(e => e.MapControllers());
    }
    
    
    private static string? GetServiceVersion() =>
        FileVersionInfo.GetVersionInfo(typeof(Startup).Assembly.Location).FileVersion;
    
    private string GetGQLSchemaVersion() =>
        Environment.GetEnvironmentVariable("GQL_SCHEMA_VERSION") ?? DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
    private async Task ExceptionHandler(HttpContext context)
    {
        await Task.Yield();
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error?.GetBaseException();
        if (exception == null)
            return;
        try
        { 
            logger?.Error(exception.Message, exception.GetBaseException());
        }
        catch (Exception e)
        {
            logger?.Error(exception.Message, e.GetBaseException());
        }
    }
}