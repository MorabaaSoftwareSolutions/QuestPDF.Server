using QuestPDF.Drawing;

namespace QuestPDF.Server.Core.Services;

/// <summary>
/// Represents a service for fetching fonts using HTTP.
/// </summary>
public sealed class HttpFontFetcher : IFontFetcher
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HashSet<string> _loadedFonts = new();

    public HttpFontFetcher(IHttpClientFactory httpClientFactory)
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
        if (_loadedFonts.Contains(uri.AbsoluteUri))
        {
            return;
        }

        using var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync(uri, cancellationToken);
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        stream.Seek(0, SeekOrigin.Begin);
        FontManager.RegisterFont(stream);
        _loadedFonts.Add(uri.AbsoluteUri);
    }
}
