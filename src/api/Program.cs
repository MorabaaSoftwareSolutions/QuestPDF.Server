using System.Text.Json;
using System.Text.Json.Serialization;
using QuestPDF.Infrastructure;
using QuestPDF.Server.Api;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateSlimBuilder(args);

        builder.Configuration.AddEnvironmentVariables();
        builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        var licenseType = builder.Configuration.GetValue<string>("QuestPDF:LicenseType") ?? throw new InvalidOperationException("License type is not specified");

        if (licenseType.Equals("community", StringComparison.InvariantCultureIgnoreCase))
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }
        else if (licenseType.Equals("professional", StringComparison.InvariantCultureIgnoreCase))
        {
            QuestPDF.Settings.License = LicenseType.Professional;
        }
        else if (licenseType.Equals("enterprise", StringComparison.InvariantCultureIgnoreCase))
        {
            QuestPDF.Settings.License = LicenseType.Enterprise;
        }
        else
        {
            throw new InvalidOperationException("Invalid license type. Please console https://www.questpdf.com/license/ for more information.");
        }

        QuestPDF.Settings.EnableDebugging = builder.Environment.IsDevelopment();

        builder.Services.AddQuestPdfServer();

        var app = builder.Build();

        app.UseMiddleware<TimingMiddleware>();
        app.MapEndpoints();
        app.Run();
    }
}
