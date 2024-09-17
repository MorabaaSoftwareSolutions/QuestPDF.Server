namespace QuestPDF.Server.Core.Services;

/// <summary>
/// Represents a service for fetching fonts.
/// </summary>
public interface IFontFetcher
{
    /// <summary>
    /// Loads a font asynchronously based on the provided URI.
    /// </summary>
    /// <param name="uri">The URI of the font.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task LoadFontAsync(string uri, CancellationToken cancellationToken);

    /// <summary>
    /// Loads a font asynchronously based on the provided URI.
    /// </summary>
    /// <param name="uri">The URI of the font.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task LoadFontAsync(Uri uri, CancellationToken cancellationToken);
}
