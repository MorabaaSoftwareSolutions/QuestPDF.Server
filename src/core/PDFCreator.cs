using QuestPDF.Fluent;
using QuestPDF.Server.Core.Elements;
using QuestPDF.Server.Core.Extensions;
using QuestPDF.Server.Core.Services;
using QImage = QuestPDF.Infrastructure.Image;

namespace QuestPDF.Server.Core;

/// <summary>
/// Represents a PDF creator that generates PDF documents based on the provided specifications.
/// </summary>
public sealed class PDFCreator
{
    private readonly IImageFetcher _imageFetcher;
    private readonly IFontFetcher _fontFetcher;
    private readonly Dictionary<int, QImage> _images = [];

    public PDFCreator(IImageFetcher imageLoader, IFontFetcher fontFetcher)
    {
        _imageFetcher = imageLoader;
        _fontFetcher = fontFetcher;
    }

    /// <summary>
    /// Creates a PDF asynchronously based on the provided request.
    /// </summary>
    /// <param name="request">The request containing the specifications for the PDF creation.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A stream containing the generated PDF.</returns>
    public async Task<Stream> CreateAsync(CreatePDFRequest request, CancellationToken cancellationToken)
    {
        await LoadImagesAsync(request, cancellationToken);
        await LoadFontAsync(request, cancellationToken);
        var doc = Document.Create(container =>
            container.Page(page =>
            {
                page.FromSpecs(request.Page);
                if (request.Header is not null)
                {
                    page.Header().Element(request.Header);
                }
                page.Content().Element(request.Content);
                if (request.Footer is not null)
                {
                    page.Footer().Element(request.Footer);
                }
            })
        );
        var stream = new MemoryStream();
        doc.GeneratePdf(stream);
        stream.Position = 0;

        return stream;
    }

    private async Task LoadFontAsync(CreatePDFRequest request, CancellationToken cancellationToken)
    {
        if (request.Page.FontUris?.Length > 0)
        {
            var tasks = request.Page.FontUris.Select(uri => _fontFetcher.LoadFontAsync(uri, cancellationToken));
            await Task.WhenAll(tasks);
        }
    }

    private async Task LoadImagesAsync(CreatePDFRequest request, CancellationToken cancellationToken)
    {
        if (request.Header is not null)
        {
            await LoadImagesAsync(request.Header, cancellationToken);
        }
        if (request.Footer is not null)
        {
            await LoadImagesAsync(request.Footer, cancellationToken);
        }
        await LoadImagesAsync(request.Content, cancellationToken);
    }

    private async Task LoadImagesAsync(IElement element, CancellationToken cancellationToken)
    {
        if (element is ImageElement imageElement)
        {
            await LoadImageAsync(imageElement, cancellationToken);
        }
        else if (element is ColumnElement columnElement)
        {
            await LoadImagesAsync(columnElement, cancellationToken);
        }
        else if (element is RowElement rowElement)
        {
            await LoadImagesAsync(rowElement, cancellationToken);
        }
    }

    private async Task LoadImagesAsync(ColumnElement columnElement, CancellationToken cancellationToken)
    {
        foreach (var element in columnElement.Elements)
        {
            if (element is ImageElement imageElement)
            {
                await LoadImageAsync(imageElement, cancellationToken);
            }
            else if (element is ColumnElement columnElement1)
            {
                await LoadImagesAsync(columnElement1, cancellationToken);
            }
            else if (element is RowElement rowElement)
            {
                await LoadImagesAsync(rowElement, cancellationToken);
            }
        }
    }

    private async Task LoadImagesAsync(RowElement rowElement, CancellationToken cancellationToken)
    {
        foreach (var element in rowElement.Elements)
        {
            if (element is ImageElement imageElement)
            {
                await LoadImageAsync(imageElement, cancellationToken);
            }
            else if (element is ColumnElement columnElement)
            {
                await LoadImagesAsync(columnElement, cancellationToken);
            }
            else if (element is RowElement rowElement1)
            {
                await LoadImagesAsync(rowElement1, cancellationToken);
            }
        }
    }

    private async Task LoadImageAsync(ImageElement imageElement, CancellationToken cancellationToken)
    {
        if (imageElement.Image is not null)
        {
            return;
        }
        var bytes = await _imageFetcher.LoadImageAsync(imageElement, cancellationToken);
        if (bytes is null || bytes.Length < 1)
        {
            throw new InvalidOperationException("Image bytes are not loaded");
        }
        if (_images.TryGetValue(imageElement.GetHashCode(), out var image))
        {
            imageElement.Image = image;
            return;
        }
        image = QImage.FromBinaryData(bytes);
        _images[imageElement.GetHashCode()] = image;
        imageElement.Image = image;
    }
}
