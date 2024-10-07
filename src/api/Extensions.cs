using System.Text.Json;
using System.Text.Json.Serialization;
using QuestPDF.Infrastructure;
using QuestPDF.Server.Core.Elements;

namespace QuestPDF.Server.Api;

public static class Extensions
{
    public static IServiceCollection AddQuestPdfServer(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.TypeInfoResolverChain.Insert(0, QuestPDFServerJsonSerializerContext.Default);
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter<FontWeight>());
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter<TypeInRow>());
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter<TextDecoration>());
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter<Unit>());
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter<PageSpecs.ContentDirection>());
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter<TableElement.Column.ColumnType>());
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter<CellAlignment>());
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter<TextAlignment>());
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter<ImageFit>());
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter<Script>());
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });
        Core.Extensions.QuestServerExtensions.AddQuestPdfServer(services);
        return services;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => Results.Text($"QuestPDF Server v{typeof(Extensions).Assembly.GetName().Version}"));
        app.MapGet("/health", () => Results.Ok());
        app.MapPost("/pdf", Endpoints.GeneratePdfEndpoint);
        return app;
    }
}
