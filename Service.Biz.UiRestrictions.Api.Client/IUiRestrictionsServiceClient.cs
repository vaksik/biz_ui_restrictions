using System.Threading;
using System.Threading.Tasks;
using Service.Biz.UiRestrictions.Dto.WebApi;

namespace Service.Biz.UiRestrictions.Api.Client
{
    public interface IUiRestrictionsServiceClient
    {
        /// <summary>
        ///  Получить список организаций с доступом к feature (ui компонент) для заданных организаций
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OrganizationsNotAvailableResponseDto> GetNotAvailableOrganizationsToFeatureAsync(
            GetOrganizationsNotAvailableRequestDto request,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///  Получить список ограничений для заданных организаций
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OrganizationsRestrictionsResponseDto> GetOrganizationsRestrictionsAsync(
            GetOrganizationsRestrictionsRequestDto request,
            CancellationToken cancellationToken = default);

    }
}