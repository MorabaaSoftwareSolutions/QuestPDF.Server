using QuestPDF.Server.Core.Elements;

namespace QuestPDF.Server.Core.Services;

/// <summary>
/// Represents a service for fetching images.
/// </summary>
public interface IImageFetcher
{
    /// <summary>
    /// Loads an image asynchronously based on the provided ImageElement.
    /// </summary>
    /// <param name="element">The ImageElement containing the image information.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the image data as a byte array.</returns>
    Task<byte[]> LoadImageAsync(ImageElement element, CancellationToken cancellationToken);

    /// <summary>
    /// Loads an image asynchronously based on the provided URI.
    /// </summary>
    /// <param name="uri">The URI of the image.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the image data as a byte array.</returns>
    Task<byte[]> LoadImageAsync(string uri, CancellationToken cancellationToken);
}
