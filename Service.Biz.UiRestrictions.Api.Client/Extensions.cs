using Module.HttpClient.Default;
using Module.Tracing.Contract;
using Newtonsoft.Json;

namespace Service.Biz.UiRestrictions.Api.Client
{
    internal static class Extensions
    {
        internal static SimpleHttpContext WithTracer(this SimpleHttpContext simpleHttpContext, ITracer tracer)
        {
            simpleHttpContext.Tracer = tracer;
            return simpleHttpContext;
        }

        internal static SimpleHttpContext WithDefaultJsonSettings(this SimpleHttpContext context)
        {
            return context.WithCustomJsonSettings(settings =>
            {
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.TypeNameHandling = TypeNameHandling.Auto;
                settings.Formatting = Formatting.None;
#if DEBUG
                settings.Formatting = Formatting.Indented;
#endif
            });
        }
    }
}