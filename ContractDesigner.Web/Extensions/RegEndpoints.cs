using ContractDesigner.Web.Endpoints;

namespace ContractDesigner.Web.Extensions
{
    public static class RegEndpoints
    {
        public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPageEndpoints();
            app.MapGenerateEndpoints();
            return app;
        }
    }
}
