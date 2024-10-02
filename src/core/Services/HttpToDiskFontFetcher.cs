
using Microsoft.Extensions.Options;

namespace QuestPDF.Server.Core.Services;

/// <summary>
/// Represents a service for fetching fonts using HTTP.
/// </summary>
public sealed class HttpToDiskFontFetcher : IFontFetcher
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HashSet<string> _loadedFonts = new();

    public HttpToDiskFontFetcher(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public Task LoadFontAsync(string uri, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(uri))
        {
            return Task.CompletedTask;
        }
        if (!Uri.TryCreate(uri, UriKind.Absolute, out var fontUri))
        {
            return Task.CompletedTask;
        }
        return LoadFontAsync(fontUri, cancellationToken);
    }

    public async Task LoadFontAsync(Uri uri, CancellationToken cancellationToken)
    {
        var fontFileName = Path.GetFileName(uri.LocalPath);
        var dir = Environment.SpecialFolder.LocalApplicationData.ToString();
        Directory.CreateDirectory(dir);
        var path = Path.Combine(dir, Path.GetFileName(uri.LocalPath));
        if (_loadedFonts.Contains(fontFileName))
        {
            return;
        }
        if (File.Exists(path))
        {
            _loadedFonts.Add(fontFileName);
            return;
        }

        using var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync(uri, cancellationToken);
        response.EnsureSuccessStatusCode();
        var bytes = await response.Content.ReadAsByteArrayAsync(cancellationToken);
        if (bytes.Length < 1)
        {
            return;
        }
        await File.WriteAllBytesAsync(path, bytes, cancellationToken);
        _loadedFonts.Add(fontFileName);
    }
}
