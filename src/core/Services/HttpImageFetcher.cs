using Microsoft.Extensions.Caching.Memory;
using QuestPDF.Server.Core.Elements;

namespace QuestPDF.Server.Core.Services;

/// <summary>
/// Represents a class that fetches images from HTTP URLs.
/// </summary>
public sealed class HttpImageFetcher : IImageFetcher
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _cache;

    public HttpImageFetcher(IHttpClientFactory httpClientFactory, IMemoryCache cache)
    {
        _httpClientFactory = httpClientFactory;
        _cache = cache;
    }

    /// <summary>
    /// Loads an image from the specified URI asynchronously.
    /// </summary>
    /// <param name="uri">The URI of the image to load.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the loaded image as a byte array.</returns>
    public async Task<byte[]> LoadImageAsync(string uri, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(uri))
        {
            throw new ArgumentException("The URI cannot be null or whitespace.", nameof(uri));
        }
        if (_cache.TryGetValue<byte[]>(uri, out var cachedImage) && cachedImage is not null)
        {
            return cachedImage;
        }
        using var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync(uri, cancellationToken);
        response.EnsureSuccessStatusCode();
        var bytes = await response.Content.ReadAsByteArrayAsync(cancellationToken);
        _cache.Set(uri, bytes, TimeSpan.FromMinutes(15));
        return bytes;
    }

    /// <summary>
    /// Loads an image from the specified <see cref="ImageElement"/> asynchronously.
    /// </summary>
    /// <param name="element">The <see cref="ImageElement"/> containing the source URI of the image to load.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the loaded image as a byte array.</returns>
    public Task<byte[]> LoadImageAsync(ImageElement element, CancellationToken cancellationToken)
        => LoadImageAsync(element.Source, cancellationToken);
}
