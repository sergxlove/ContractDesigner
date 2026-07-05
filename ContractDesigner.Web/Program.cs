using ContractDesigner.Core.Abstractions;
using ContractDesigner.Core.Models;
using ContractDesigner.Core.Services;
using ContractDesigner.Web.Hubs;
using System.Diagnostics;

namespace ContractDesigner.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSignalR();
            builder.Services.AddScoped<IRentalApartamentAgreement, RentalApartamentAgreement>();
            builder.WebHost.UseUrls("http://localhost:5000");
            var app = builder.Build();
            app.MapHub<ExitHub>("/hub");
            //app.UseStaticFiles();
            app.MapGet("/", () => "Hello World!");

            app.MapGet("/api/contract/generate", async (IRentalApartamentAgreement service) =>
            {
                try
                {
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

            Task.Run(() =>
            {
                Thread.Sleep(500);
                Process.Start(new ProcessStartInfo("http://localhost:5000")
                {
                    UseShellExecute = true
                });
            });

            app.Run();
        }
    }
}
