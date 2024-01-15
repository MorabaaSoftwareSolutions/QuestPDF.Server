namespace QuestPDF.Server.Core.Elements;

/// <summary>
/// Represents a column element in a document.
/// </summary>
public class ColumnElement : Element, IElement
{
    /// <summary>
    /// Gets or sets the collection of elements within the column.
    /// </summary>
    public required ICollection<IElement> Elements { get; set; } = [];

    /// <summary>
    /// Gets or sets the spacing between elements within the column.
    /// </summary>
    public float? Spacing { get; set; }
}

