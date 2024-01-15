namespace QuestPDF.Server.Core.Elements;

/// <summary>
/// Represents the configuration for a row item.
/// </summary>
public class RowItemConfig
{
    /// <summary>
    /// Gets or sets the type of the row item.
    /// </summary>
    public TypeInRow Type { get; set; } = TypeInRow.Auto;

    /// <summary>
    /// Gets or sets the size of the row item.
    /// </summary>
    public float? Size { get; set; }
}

public enum TypeInRow
{
    Auto,
    Constant,
    Relative,
}
