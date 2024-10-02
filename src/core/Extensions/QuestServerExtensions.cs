using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Server.Core.Services;

namespace QuestPDF.Server.Core.Extensions;

public static class QuestServerExtensions
{
    public static IServiceCollection AddQuestPdfServer(this IServiceCollection services)
    {
        return services
            .AddMemoryCache()
            .AddHttpClient()
            .AddSingleton<IImageFetcher, HttpImageFetcher>()
            .AddSingleton<IFontFetcher, HttpFontFetcher>()
            .AddScoped<PDFCreator>();
    }
}
