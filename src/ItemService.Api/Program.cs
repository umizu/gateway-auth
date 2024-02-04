var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/items", (HttpContext ctx) =>
{
    // pretend an item object was passed in through the request body;
    var itemId = Guid.NewGuid();

    return Results.Ok(new
    {
        Id = itemId,
        UserId = ctx.Request.Headers["X-User-Id"].ToString()
    });
});

app.MapGet("/items", (HttpContext ctx) =>
{
    return Results.Ok(new
    {
        items = "...items"
    });
});

app.Run();
