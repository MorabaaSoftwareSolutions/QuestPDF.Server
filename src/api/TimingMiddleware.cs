namespace QuestPDF.Server.Api;

public class TimingMiddleware
{
    private readonly RequestDelegate _next;

    public TimingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var now = DateTimeOffset.UtcNow;
        context.Response.OnStarting(() =>
        {
            var elapsed = DateTimeOffset.UtcNow - now;
            context.Response.Headers.Append("X-Response-Time", $"{elapsed.TotalMilliseconds}ms");
            return Task.CompletedTask;
        });
        await _next(context);
    }
}
