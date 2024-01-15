using Microsoft.AspNetCore.Mvc;
using QuestPDF.Server.Core;

namespace QuestPDF.Server.Api;

public static class Endpoints
{
    public static async Task<IResult> GeneratePdfEndpoint([FromBody] CreatePDFRequest request, [FromServices] PDFCreator creator, HttpContext httpContext)
    {
        var stream = await creator.CreateAsync(request, httpContext.RequestAborted);
        return Results.File(stream, "application/pdf", "output.pdf", DateTimeOffset.UtcNow, enableRangeProcessing: true);
    }
}
