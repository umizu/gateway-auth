using RecommendationService.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();
builder.Services.AddHostedService<ItemBackgroundService>();

var app = builder.Build();
app.MapHealthChecks("/_health");
app.Run();
