using Module.AspNetCore;
using Module.Logging.Core;
using Module.Logging.NLog;
using Service.Biz.UiRestrictions.Host;

NLogBootstrapper.ConfigureFromEnvironment(Startup.ServiceName);
LoggingManager.Configure(configurator =>
{
    configurator.SetComponentName(Startup.ServiceName).UseNLog();
});
Bootstrapper.Run<Startup>("http://0.0.0.0:9099", args);