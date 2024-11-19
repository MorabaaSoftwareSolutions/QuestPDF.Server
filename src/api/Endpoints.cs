using Microsoft.AspNetCore.Mvc;
using QuestPDF.Server.Core;

namespace QuestPDF.Server.Api;

public static class Endpoints
{
    public static async Task<IResult> GeneratePdfEndpoint([FromBody] CreatePDFRequest request, [FromServices] PDFCreator creator, HttpContext httpContext)
    {
        var acceptHeaderValue = httpContext.Request.Headers.Accept.FirstOrDefault() ?? "";
        if (acceptHeaderValue is "image/jpeg" or "image/jpg")
        {
            var images = await creator.CreatePdfImagesAsync(request, httpContext.RequestAborted);
            return Results.Json(images, contentType: "application/json", statusCode: 200);
        }
        else
        {
            var stream = await creator.CreateAsync(request, httpContext.RequestAborted);
            return Results.File(stream, "application/pdf", request.OutputFileName ?? "output.pdf", DateTimeOffset.UtcNow, enableRangeProcessing: true);
        }
    }
}
