using QuestPDF.Server.Core.Elements;

namespace QuestPDF.Server.Core;

/// <summary>
/// Represents a request to create a PDF.
/// </summary>
public sealed class CreatePDFRequest
{
    /// <summary>
    /// Gets or sets the specifications for the page.
    /// </summary>
    public required PageSpecs Page { get; set; }

    /// <summary>
    /// Gets or sets the title element of the PDF.
    /// </summary>
    public IElement? Header { get; set; }

    /// <summary>
    /// Gets or sets the content element of the PDF.
    /// </summary>
    public required IElement Content { get; set; }

    /// <summary>
    /// Gets or sets the footer element of the PDF.
    /// </summary>
    public IElement? Footer { get; set; }

    /// <summary>
    /// Gets or sets the name of the output file.
    /// <para />
    /// If not specified, the output file will be named "output.pdf".
    /// </summary>
    public string? OutputFileName { get; set; }

    /// <summary>
    /// If accept header was set to images, this property will be used to specify the DPI for the rasterization of the images.
    /// <para />
    /// If not specified, the default DPI of 300 will be used.
    /// </summary>
    public int? ImageRasterizationDpi { get; set; }
}
