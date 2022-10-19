using GM.Store.Server.Config;

namespace GM.Store.Server.Database
{
   
        record PeriodicHostedServiceState(bool IsEnabled);
    
    public class PeriodicHostedService : BackgroundService
    {
        private readonly TimeSpan _period = TimeSpan.FromMinutes(5);
        private readonly ILogger<PeriodicHostedService> _logger;
        private readonly IServiceScopeFactory _factory;
        private readonly ServiceConfiguration _serviceConfig;
        private int _executionCount = 0;
        public bool IsEnabled { get; set; }

        public PeriodicHostedService(
            ILogger<PeriodicHostedService> logger, ServiceConfiguration serviceConfig,
            IServiceScopeFactory factory)
        {
            _logger = logger;
            _factory = factory;
            _serviceConfig = serviceConfig;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);
            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
                    IDataAccessManager syncSerivce = asyncScope.ServiceProvider.GetRequiredService<IDataAccessManager>();
                    //need to change api Url- 
                    
                    var lastSyncTime = await syncSerivce.GetLastSyncTimeAsync($"api/sync/lastupdated");
                    //  await sampleService.CartSyncAsync($"api/sync/{businessId}/cart", lastSyncTime.Model.LastUpdatedTime);
                    //await syncSerivce.OrderSync($"api/sync/order", lastSyncTime.Model.LastUpdatedTime);
                    //await syncSerivce.RemoveBusinessSettingAsync("settings");
                    //await syncSerivce.RemoveInactiveCartAsync();
                    _executionCount++;
                    _logger.LogInformation($"Executed PeriodicHostedService - Count: {_executionCount}");

                }
                catch (Exception ex)
                {
                    _logger.LogInformation(
                        $"Failed to execute PeriodicHostedService with exception message {ex.Message}. Good luck next round!");
                }
            }
        }
    }
}
