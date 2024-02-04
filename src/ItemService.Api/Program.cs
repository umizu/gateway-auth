var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/items", (HttpContext ctx) =>
{
    // pretend an item was passed in through the request body;
    var itemId = Guid.NewGuid();

    return Results.Ok(new
    {
        itemId,
        userInfo = new
        {
            id = ctx.Request.Headers["X-User-Id"].ToString(),
            roles = ctx.Request.Headers["X-User-Roles"].ToString()
        }
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
