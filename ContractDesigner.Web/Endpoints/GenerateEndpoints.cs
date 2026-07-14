using ContractDesigner.Core.Abstractions;
using ContractDesigner.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractDesigner.Web.Endpoints
{
    public static class GenerateEndpoints
    {
        public static IEndpointRouteBuilder MapGenerateEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/contract/generate", async (
                //[FromBody] RentalApartamentAgreementOptions request,
                [FromServices] IRentalApartamentAgreement service,
                CancellationToken token) =>
            {
                try
                {
                    //if (request is null)
                        //return Results.BadRequest("Данные не переданы");
                    RentalApartamentAgreementOptions options = new();
                    var pdfBytes = service.Generate(options);
                    return Results.File(
                        fileContents: pdfBytes,
                        contentType: "application/pdf",
                        fileDownloadName: $"Договор_аренды_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                    );
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            });

            return app;
        }
    }
}
