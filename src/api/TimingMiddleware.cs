namespace QuestPDF.Server.Api;

public class TimingMiddleware
{
    private readonly RequestDelegate _next;

    public TimingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<TimingMiddleware> logger)
    {
        var now = DateTimeOffset.UtcNow;
        context.Response.OnStarting(() =>
        {
            var elapsed = DateTimeOffset.UtcNow - now;
            context.Response.Headers.Append("X-Response-Time", $"{elapsed.TotalMilliseconds}ms");
            logger.LogInformation("Request {Method} {Path} took {Elapsed}ms", context.Request.Method, context.Request.Path, elapsed.TotalMilliseconds);
            return Task.CompletedTask;
        });
        await _next(context);
    }
}
