using QuestPDF.Infrastructure;

namespace QuestPDF.Server.Core.Elements;

/// <summary>
/// Represents a text element in a document.
/// </summary>
public class TextElement : Element, IElement
{
    /// <summary>
    /// Gets or sets the text content of the element.
    /// </summary>
    public required string Text { get; set; }

    /// <summary>
    /// Gets or sets the font size of the text.
    /// </summary>
    public float? FontSize { get; set; }

    /// <summary>
    /// Gets or sets the font family of the text.
    /// </summary>
    public string? FontFamily { get; set; }

    /// <summary>
    /// Gets or sets the color of the text.
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Gets or sets the line height of the text.
    /// </summary>
    public float? LineHeight { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the text is italic.
    /// </summary>
    public bool? IsItalic { get; set; }

    /// <summary>
    /// Gets or sets the font weight of the text.
    /// </summary>
    public FontWeight FontWeight { get; set; } = FontWeight.Normal;

    /// <summary>
    /// Gets or sets the text decoration of the text.
    /// </summary>
    public TextDecoration TextDecoration { get; set; } = TextDecoration.None;

    /// <summary>
    /// Gets or sets the script of the text.
    /// </summary>
    public Script Script { get; set; } = Script.Normal;

    public TextAlignment? Alignment { get; set; }

    /// <summary>
    /// Gets or sets the padding of the text.
    /// </summary>
    public float? Padding { get; set; }
    public Unit? PaddingUnit { get; set; }

    /// <summary>
    /// Gets or sets the margin of the text.
    /// </summary>
    public float? LetterSpacing { get; set; }

    public float? BorderWidth { get; set; }
    public float? BorderTopWidth { get; set; }
    public float? BorderRightWidth { get; set; }
    public float? BorderBottomWidth { get; set; }
    public float? BorderLeftWidth { get; set; }
    public string? BorderColor { get; set; }
    public Unit? BorderUnit { get; set; }
}

/// <summary>
/// Represents the script style of a text element.
/// </summary>
public enum Script
{
    Normal,
    Sub,
    Super,
}

/// <summary>
/// Represents the decoration styles for text.
/// </summary>
public enum TextDecoration
{
    None,
    Underline,
    Strike,
}

/// <summary>
/// Represents the text alignment of a text element.
/// </summary>
public enum TextAlignment
{
    Start,
    Center,
    End,
    Justify,
}
