using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Module.HttpClient;
using Module.HttpClient.Base.Context;
using Module.HttpClient.Default;
using Module.Tracing.Contract;
using Module.Tracing.Contract.Tracer;
using Service.Biz.UiRestrictions.Dto.System;
using Service.Biz.UiRestrictions.Dto.WebApi;

namespace Service.Biz.UiRestrictions.Api.Client
{
    public class UiRestrictionsServiceClient : IUiRestrictionsServiceClient
    {
        private readonly HttpClientWrapper<SimpleHttpContext> _wrapper;
        protected readonly string _routePrefix = "api/v1/features/";

        public UiRestrictionsServiceClient(Uri baseAddress, TimeSpan timeout, ITracer tracer,
        Action<IHttpContext> setup = null, HttpMessageHandler httpMessageHandler = null)
        {
            _wrapper = new HttpClientWrapper<SimpleHttpContext>(
                ContextFactory.Create(baseAddress, timeout)
                    .WithDefaultJsonSettings()
                    .WithTracer(tracer.ForComponent(MetadataDto.ComponentName))
                    .WithCustom(setup),
                httpMessageHandler
            );
        }
        
        public async Task<OrganizationsNotAvailableResponseDto> GetNotAvailableOrganizationsToFeatureAsync(
            GetOrganizationsNotAvailableRequestDto request, CancellationToken cancellationToken = default) =>  
            await _wrapper.RequestAsync<GetOrganizationsNotAvailableRequestDto, OrganizationsNotAvailableResponseDto>(
                BuildRoute("organizations/notAvailable"), request, Method.Post);

        public async Task<OrganizationsRestrictionsResponseDto> GetOrganizationsRestrictionsAsync(GetOrganizationsRestrictionsRequestDto request,
            CancellationToken cancellationToken = default) =>  
            await _wrapper.RequestAsync<GetOrganizationsRestrictionsRequestDto, OrganizationsRestrictionsResponseDto>(
                BuildRoute("organizations/restrictions"), request, Method.Post);

        private string BuildRoute(string resource) => $"{_routePrefix.Trim('/')}/{resource}";
    }
}