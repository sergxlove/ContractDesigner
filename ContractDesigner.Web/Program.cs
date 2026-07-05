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
            builder.WebHost.UseUrls("http://localhost:5000");
            var app = builder.Build();
            app.MapHub<ExitHub>("/hub");
            app.UseStaticFiles();
            app.MapGet("/", () => "Hello World!");

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
