using Microsoft.AspNetCore.SignalR;

namespace ContractDesigner.Web.Hubs
{
    public class ExitHub : Hub
    {
        private readonly IHostApplicationLifetime _lifetime;

        public ExitHub(IHostApplicationLifetime lifetime)
        {
            _lifetime = lifetime;
        }

        public Task CloseApplication()
        {
            _lifetime.StopApplication();
            return Task.CompletedTask;
        }
    }
}
