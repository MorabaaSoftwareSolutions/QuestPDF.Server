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
        var doc = await CreateDocumentAsync(request, cancellationToken);
        var stream = new MemoryStream();
        doc.GeneratePdf(stream);
        stream.Position = 0;

        return stream;
    }

    /// <summary>
    /// Creates a PDF asynchronously based on the provided request and returns the images in base64 format.
    /// </summary>
    /// <param name="request">The request containing the specifications for the PDF creation.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>An array of strings containing the images in base64 format.</returns>
    public async Task<string[]> CreatePdfImagesAsync(CreatePDFRequest request, CancellationToken cancellationToken)
    {
        var doc = await CreateDocumentAsync(request, cancellationToken);
        var images = doc.GenerateImages(new Infrastructure.ImageGenerationSettings
        {
            ImageFormat = Infrastructure.ImageFormat.Jpeg,
            ImageCompressionQuality = Infrastructure.ImageCompressionQuality.High,
            RasterDpi = request.ImageRasterizationDpi ?? 300,
        });

        return images.Select(Convert.ToBase64String).ToArray();
    }

    private async Task<Infrastructure.IDocument> CreateDocumentAsync(CreatePDFRequest request, CancellationToken cancellationToken)
    {

        await LoadImagesAsync(request, cancellationToken);
        await LoadFontAsync(request, cancellationToken);
        return Document.Create(container =>
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
        await LoadImageAsync(request.Page, cancellationToken);
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
        else if (element is Cell cellElement)
        {
            await LoadImagesAsync(cellElement, cancellationToken);
        }
        else if (element is TableElement tableElement)
        {
            await LoadImagesAsync(tableElement, cancellationToken);
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
            else if (element is Cell cellElement)
            {
                await LoadImagesAsync(cellElement, cancellationToken);
            }
            else if (element is TableElement tableElement)
            {
                await LoadImagesAsync(tableElement, cancellationToken);
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
            else if (element is Cell cellElement)
            {
                await LoadImagesAsync(cellElement, cancellationToken);
            }
            else if (element is TableElement tableElement)
            {
                await LoadImagesAsync(tableElement, cancellationToken);
            }
        }
    }

    private async Task LoadImagesAsync(Cell cellElement, CancellationToken cancellationToken)
    {
        if (cellElement.Element is ImageElement imageElement)
        {
            await LoadImageAsync(imageElement, cancellationToken);
        }
        else if (cellElement.Element is ColumnElement columnElement)
        {
            await LoadImagesAsync(columnElement, cancellationToken);
        }
        else if (cellElement.Element is RowElement rowElement1)
        {
            await LoadImagesAsync(rowElement1, cancellationToken);
        }
    }

    private async Task LoadImagesAsync(TableElement tableElement, CancellationToken cancellationToken)
    {
        foreach (var cell in tableElement.Cells)
        {
            await LoadImagesAsync(cell, cancellationToken);
        }
    }

    private async Task LoadImageAsync(ImageElement imageElement, CancellationToken cancellationToken)
    {
        if (imageElement.Image is not null)
        {
            return;
        }
        if (_images.TryGetValue(imageElement.GetHashCode(), out var image))
        {
            imageElement.Image = image;
            return;
        }
        var bytes = await _imageFetcher.LoadImageAsync(imageElement, cancellationToken);
        if (bytes is null || bytes.Length < 1)
        {
            throw new InvalidOperationException("Image bytes are not loaded");
        }
        image = QImage.FromBinaryData(bytes);
        _images[imageElement.GetHashCode()] = image;
        imageElement.Image = image;
    }

    private async Task LoadImageAsync(PageSpecs pageSpecs, CancellationToken cancellationToken)
    {
        if (pageSpecs.BackgroundImage is not null || pageSpecs.BackgroundImageUri is null)
        {
            return;
        }
        if (_images.TryGetValue(pageSpecs.BackgroundImageUri.GetHashCode(), out var image))
        {
            pageSpecs.BackgroundImage = image;
            return;
        }
        var bytes = await _imageFetcher.LoadImageAsync(pageSpecs.BackgroundImageUri, cancellationToken);
        if (bytes is null || bytes.Length < 1)
        {
            throw new InvalidOperationException("Image bytes are not loaded");
        }
        image = QImage.FromBinaryData(bytes);
        _images[pageSpecs.BackgroundImageUri.GetHashCode()] = image;
        pageSpecs.BackgroundImage = image;
    }
}
