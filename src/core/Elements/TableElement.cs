namespace QuestPDF.Server.Core.Elements;

/// <summary>
/// Represents a table element in a document.
/// </summary>
public class TableElement : Element, IElement
{
    /// <summary>
    /// Gets or sets the collection of cells in the table.
    /// </summary>
    public required ICollection<Cell> Cells { get; set; }

    /// <summary>
    /// Gets or sets the collection of columns in the table.
    /// </summary>
    public required ICollection<Column> Columns { get; set; }

    /// <summary>
    /// Gets or sets the width of the table border.
    /// </summary>
    public float? BorderWidth { get; set; }

    /// <summary>
    /// Represents a column in the table.
    /// </summary>
    public class Column
    {
        /// <summary>
        /// Gets or sets the size of the column.
        /// </summary>
        public float Size { get; set; }

        /// <summary>
        /// Gets or sets the type of the column.
        /// </summary>
        public ColumnType Type { get; set; }

        /// <summary>
        /// Represents the type of a column.
        /// </summary>
        public enum ColumnType
        {
            /// <summary>
            /// Represents a column with a constant size.
            /// </summary>
            Constant,

            /// <summary>
            /// Represents a column with a relative size.
            /// </summary>
            Relative,
        }
    }
}

/// <summary>
/// Represents a cell in a table.
/// </summary>
public class Cell : Element, IElement
{
    /// <summary>
    /// Gets or sets the element contained within the cell.
    /// </summary>
    public required IElement Element { get; set; }

    /// <summary>
    /// Gets or sets the padding of the cell.
    /// </summary>
    public float? Padding { get; set; }

    /// <summary>
    /// Gets or sets the border width of the cell.
    /// </summary>
    public float? BorderWidth { get; set; }

    /// <summary>
    /// Gets or sets the background color of the cell.
    /// </summary>
    public string? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the border color of the cell.
    /// </summary>
    public string? BorderColor { get; set; }

    /// <summary>
    /// Gets or sets the minimum height of the cell.
    /// </summary>
    public float? MinHeight { get; set; }

    /// <summary>
    /// Gets or sets the maximum height of the cell.
    /// </summary>
    public float? MaxHeight { get; set; }

    /// <summary>
    /// Gets or sets the alignment of the cell.
    /// </summary>
    public CellAlignment? VerticalAlign { get; set; }

    /// <summary>
    /// Gets or sets the alignment of the cell.
    /// </summary>
    public CellAlignment? HorizontalAlign { get; set; }

    /// <summary>
    /// Gets or sets the row index of the cell.
    /// </summary>
    public uint? Row { get; set; }

    /// <summary>
    /// Gets or sets the column index of the cell.
    /// </summary>
    public uint? Column { get; set; }

    /// <summary>
    /// Gets or sets the number of rows spanned by the cell.
    /// </summary>
    public uint? RowSpan { get; set; }

    /// <summary>
    /// Gets or sets the number of columns spanned by the cell.
    /// </summary>
    public uint? ColumnSpan { get; set; }
}

/// <summary>
/// Specifies the alignment of a cell within a table.
/// </summary>
public enum CellAlignment
{
    Top,
    Bottom,
    Center,
    Middle,
    Left,
    Right,
}
