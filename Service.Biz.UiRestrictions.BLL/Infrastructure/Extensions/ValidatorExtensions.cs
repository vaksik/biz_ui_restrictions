using FluentValidation;

namespace Service.Biz.UiRestrictions.BLL.Infrastructure.Extensions;

public static class ValidatorExtensions
{
    public static async Task ThrowIfInvalidAsync<T>(this IValidator<T> validator, T instance, CancellationToken cancellation = default)
    {
        var result = await validator.ValidateAsync(instance, cancellation);
        if (result.IsValid)
            return;
        throw new ValidationException($"{result.ToString("; ")}");
    }
    public static async Task ThrowIfInvalidAsync<T>(this IValidator<T> validator, IEnumerable<T> instances, CancellationToken cancellation = default)
    {
        if (instances is null)
            throw new ArgumentNullException(nameof(instances));
        var enumerable = instances as List<T> ?? instances.ToList();
        if (enumerable.Any())
        {
            async Task Validator(T x) => await validator.ThrowIfInvalidAsync(x, cancellation: cancellation);
            foreach (var item in enumerable)
            {
                await Validator(item);
            }
        }
    }
}