using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace QuestPDF.Server.Core.Elements;

/// <summary>
/// Represents the specifications for a page in a document.
/// </summary>
public class PageSpecs
{
    /// <summary>
    /// Gets or sets the background color of the page.
    /// </summary>
    public string BackgroundColor { get; set; } = "#ffffff";

    /// <summary>
    /// Gets or sets the margin of the page.
    /// </summary>
    public float? Margin { get; set; }

    /// <summary>
    /// Gets or sets the unit of measurement for the margin.
    /// </summary>
    public Unit? MarginUnit { get; set; }

    /// <summary>
    /// Gets or sets the default font size of the page.
    /// </summary>
    public float DefaultFontSize { get; set; } = 12;

    /// <summary>
    /// Gets or sets the size of the page.
    /// </summary>
    public string PageSize { get; set; } = "A4";

    /// <summary>
    /// Gets or sets the font family to use for the page.
    /// </summary>
    public string? FontFamily { get; set; }

    /// <summary>
    /// Gets or sets the font URI to use for the page.
    /// </summary>
    public string[]? FontUris { get; set; }

    public ContinuousSizeData? ContinuousSize { get; set; }

    public ContentDirection Direction { get; set; } = ContentDirection.LTR;

    /// <summary>
    /// Gets or sets the direction of the content on the page.
    /// </summary>
    public enum ContentDirection
    {
        /// <summary>
        /// Left to right.
        /// </summary>
        LTR,

        /// <summary>
        /// Right to left.
        /// </summary>
        RTL,
    }

    public sealed class ContinuousSizeData
    {
        public required float Width { get; set; }
        public Unit? Unit { get; set; }
    }

    /// <summary>
    /// Gets the parsed page size based on the <see cref="PageSize"/>.
    /// </summary>
    internal PageSize ParsedPageSize
        => PageSize switch
        {
            "A0" => PageSizes.A0,
            "A1" => PageSizes.A1,
            "A2" => PageSizes.A2,
            "A3" => PageSizes.A3,
            "A4" => PageSizes.A4,
            "A5" => PageSizes.A5,
            "A6" => PageSizes.A6,
            "A7" => PageSizes.A7,
            "A8" => PageSizes.A8,
            "A9" => PageSizes.A9,
            "A10" => PageSizes.A10,
            "B0" => PageSizes.B0,
            "B1" => PageSizes.B1,
            "B2" => PageSizes.B2,
            "B3" => PageSizes.B3,
            "B4" => PageSizes.B4,
            "B5" => PageSizes.B5,
            "B6" => PageSizes.B6,
            "B7" => PageSizes.B7,
            "B8" => PageSizes.B8,
            "B9" => PageSizes.B9,
            "B10" => PageSizes.B10,
            "Env10" => PageSizes.Env10,
            "EnvC4" => PageSizes.EnvC4,
            "EnvDL" => PageSizes.EnvDL,
            "Executive" => PageSizes.Executive,
            "Legal" => PageSizes.Legal,
            "Letter" => PageSizes.Letter,
            "ARCH_A" => PageSizes.ARCH_A,
            "ARCH_B" => PageSizes.ARCH_B,
            "ARCH_C" => PageSizes.ARCH_C,
            "ARCH_D" => PageSizes.ARCH_D,
            "ARCH_E" => PageSizes.ARCH_E,
            "ARCH_E1" => PageSizes.ARCH_E1,
            "ARCH_E2" => PageSizes.ARCH_E2,
            "ARCH_E3" => PageSizes.ARCH_E3,
            _ => throw new ArgumentException($"Page size {PageSize} is not supported")
        };
}
