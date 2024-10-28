using QuestPDF.Infrastructure;

namespace QuestPDF.Server.Core.Elements;

/// <summary>
/// Represents an element that displays an image in a PDF document.
/// </summary>
public class ImageElement : Element, IElement
{
    /// <summary>
    /// Gets or sets the source of the image.
    /// </summary>
    public required string Source { get; set; }

    /// <summary>
    /// Gets or sets the width of the image in points.
    /// </summary>
    public float? Width { get; set; }

    /// <summary>
    /// Gets or sets the height of the image in points.
    /// </summary>
    public float? Height { get; set; }

    /// <summary>
    /// Gets or sets the fit mode of the image.
    /// </summary>
    public ImageFit Fit { get; set; } = ImageFit.Width;

    /// <summary>
    /// Gets or sets the compression quality of the image.
    /// </summary>
    public ImageCompressionQuality? CompressionQuality { get; set; }

    /// <summary>
    /// Gets or sets the DPI (dots per inch) of the image.
    /// </summary>
    public int? Dpi { get; set; }

    internal Image? Image { get; set; }

    public float? BorderWidth { get; set; }
    public float? BorderTopWidth { get; set; }
    public float? BorderRightWidth { get; set; }
    public float? BorderBottomWidth { get; set; }
    public float? BorderLeftWidth { get; set; }
    public string? BorderColor { get; set; }
    public Unit? BorderUnit { get; set; }

    /// <summary>
    /// Overriding to generate a unique hash code for each instance of ImageElement.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Source, Width, Height, Fit, CompressionQuality, Dpi);
    }
}

/// <summary>
/// Specifies how the image should be resized to fit the container.
/// </summary>
public enum ImageFit
{
    Width,
    Height,
    Area,
    Unproportional,
}
