namespace RecommendationService.Api;

public class ItemBackgroundService : BackgroundService
{
    private readonly HttpClient _httpclient;
    private readonly ILogger<ItemBackgroundService> _logger;


    public ItemBackgroundService(ILogger<ItemBackgroundService> logger)
    {
        _logger = logger;
        _httpclient = new HttpClient
        {
            BaseAddress = new Uri("http://item-service:80")
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var resp = await _httpclient.GetAsync("/items");
            if (resp.IsSuccessStatusCode)
                _logger.LogInformation("items response: {Content}", await resp.Content.ReadAsStringAsync());

            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }

    }
}