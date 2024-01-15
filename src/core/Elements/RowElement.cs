namespace QuestPDF.Server.Core.Elements;

/// <summary>
/// Represents a row element in a document.
/// </summary>
public class RowElement : Element, IElement
{
    /// <summary>
    /// Gets or sets the collection of elements in the row.
    /// </summary>
    public required ICollection<IElement> Elements { get; set; } = [];

    /// <summary>
    /// Gets or sets the spacing between elements in the row.
    /// </summary>
    public float? Spacing { get; set; }
}

