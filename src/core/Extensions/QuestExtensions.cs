using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Server.Core.Elements;

namespace QuestPDF.Server.Core.Extensions;

/// <summary>
/// Contains extension methods for the <see cref="IContainer"/> interface and other related types.
/// </summary>
internal static class QuestExtensions
{
    internal static IContainer Element(this IContainer container, Elements.IElement element)
    {
        if (element is TextElement textElement)
        {
            container.Text(textElement);
        }
        else if (element is ImageElement imageElement)
        {
            container.Image(imageElement);
        }
        else if (element is ColumnElement columnElement)
        {
            container.Column(columnElement);
        }
        else if (element is RowElement rowElement)
        {
            container.Row(rowElement);
        }
        else if (element is TableElement tableElement)
        {
            container.Table(tableElement);
        }
        else
        {
            throw new ArgumentException($"Element type {element.GetType()} is not supported");
        }
        return container;
    }

    internal static PageDescriptor FromSpecs(this PageDescriptor page, PageSpecs specs)
    {
        if (specs.Direction == PageSpecs.ContentDirection.RTL)
            page.ContentFromRightToLeft();
        page.Size(specs.ParsedPageSize);
        if (specs.Margin.HasValue && specs.MarginUnit.HasValue)
            page.Margin(specs.Margin.Value, specs.MarginUnit.Value);
        page.PageColor(specs.BackgroundColor);
        page.DefaultTextStyle(x =>
        {
            x.FontSize(specs.DefaultFontSize);
            if (!string.IsNullOrWhiteSpace(specs.FontFamily))
                x.FontFamily(specs.FontFamily);
            return x;
        });

        return page;
    }

    internal static void Text(this IContainer container, TextElement textElement)
    {
        container
        .Padding(textElement.Padding ?? 0)
        .Text(x =>
        {
            var text = x
                .Span(textElement.Text)
                .LetterSpacing(textElement.LetterSpacing ?? 0);
            if (textElement.Alignment.HasValue)
            {
                if (textElement.Alignment.Value == TextAlignment.Start)
                {
                    x.AlignStart();
                }
                else if (textElement.Alignment.Value == TextAlignment.Center)
                {
                    x.AlignCenter();
                }
                else if (textElement.Alignment.Value == TextAlignment.End)
                {
                    x.AlignEnd();
                }
                else if (textElement.Alignment.Value == TextAlignment.Justify)
                {
                    x.Justify();
                }
            }

            if (textElement.FontSize is not null)
            {
                text.FontSize(textElement.FontSize.Value);
            }
            if (textElement.Color is not null)
            {
                text.FontColor(textElement.Color);
            }
            if (textElement.TextDecoration == TextDecoration.Underline)
            {
                text.Underline();
            }
            else if (textElement.TextDecoration == TextDecoration.Strike)
            {
                text.Strikethrough();
            }
            if (textElement.Script == Script.Sub)
            {
                text.Subscript();
            }
            else if (textElement.Script == Script.Super)
            {
                text.Superscript();
            }
            if (textElement.LineHeight.HasValue)
            {
                text.LineHeight(textElement.LineHeight.Value);
            }
            if (textElement.IsItalic.HasValue && textElement.IsItalic.Value)
            {
                text.Italic();
            }
        });
    }

    internal static TextSpanDescriptor ProcessFontWeight(this TextSpanDescriptor text, FontWeight fontWeight)
    {
        return fontWeight switch
        {
            FontWeight.Thin => text.Thin(),
            FontWeight.ExtraLight => text.ExtraLight(),
            FontWeight.Light => text.Light(),
            FontWeight.Normal => text.NormalWeight(),
            FontWeight.Medium => text.Medium(),
            FontWeight.SemiBold => text.SemiBold(),
            FontWeight.Bold => text.Bold(),
            FontWeight.ExtraBold => text.ExtraBold(),
            FontWeight.Black => text.Black(),
            FontWeight.ExtraBlack => text.ExtraBlack(),
            _ => throw new ArgumentException($"Font weight {fontWeight} is not supported"),
        };
    }

    internal static void Image(this IContainer container, ImageElement imageElement)
    {
        var image = (imageElement.Width, imageElement.Height) switch
        {
            (not null, not null) => container.Container().Height(imageElement.Height.Value).Width(imageElement.Width.Value).Image(imageElement.Image ?? throw new InvalidOperationException("Image bytes are not loaded")),
            (not null, null) => container.Container().Width(imageElement.Width.Value).Image(imageElement.Image ?? throw new InvalidOperationException("Image bytes are not loaded")),
            (null, not null) => container.Container().Height(imageElement.Height.Value).Image(imageElement.Image ?? throw new InvalidOperationException("Image bytes are not loaded")),
            _ => container.Image(imageElement.Image ?? throw new InvalidOperationException("Image bytes are not loaded")),
        };

        if (imageElement.Fit == ImageFit.Width)
            image.FitWidth();
        else if (imageElement.Fit == ImageFit.Height)
            image.FitHeight();
        else if (imageElement.Fit == ImageFit.Area)
            image.FitArea();
        else if (imageElement.Fit == ImageFit.Unproportional)
            image.FitUnproportionally();

        if (imageElement.CompressionQuality.HasValue)
            image.WithCompressionQuality(imageElement.CompressionQuality.Value);

        if (imageElement.Dpi.HasValue)
            image.WithRasterDpi(imageElement.Dpi.Value);
    }

    internal static void Column(this IContainer container, ColumnElement columnElement)
    {
        container.Column(column =>
        {
            if (columnElement.Spacing.HasValue)
            {
                column.Spacing(columnElement.Spacing.Value);
            }
            foreach (var childElement in columnElement.Elements)
            {
                column.Item().Element(childElement);
            }
        });
    }

    internal static void Row(this IContainer container, RowElement rowElement)
    {
        container.Row(row =>
        {
            if (rowElement.Spacing.HasValue)
            {
                row.Spacing(rowElement.Spacing.Value);
            }
            foreach (var childElement in rowElement.Elements)
            {
                if (childElement.RowItemConfig is null || childElement.RowItemConfig?.Type == TypeInRow.Auto)
                {
                    row.AutoItem().Element(childElement);
                }
                else if (childElement.RowItemConfig?.Type == TypeInRow.Relative)
                {
                    row.RelativeItem(childElement.RowItemConfig.Size ?? 1).Element(childElement);
                }
                else if (childElement.RowItemConfig?.Type == TypeInRow.Constant)
                {
                    row.ConstantItem(childElement.RowItemConfig.Size ?? 1).Element(childElement);
                }
                else
                {
                    throw new ArgumentException($"Row item type {childElement.RowItemConfig?.Type} is not supported");
                }
            }
        });
    }

    internal static void Table(this IContainer container, TableElement tableElement)
    {
        container.Table(table => table.ColumnsDefinition(tableElement).Cells(tableElement));
    }

    internal static TableDescriptor ColumnsDefinition(this TableDescriptor table, TableElement tableElement)
    {
        table.ColumnsDefinition((x) =>
        {
            foreach (var column in tableElement.Columns)
            {
                if (column.Type == TableElement.Column.ColumnType.Constant)
                {
                    x.ConstantColumn(column.Size);
                }
                else if (column.Type == TableElement.Column.ColumnType.Relative)
                {
                    x.RelativeColumn(column.Size);
                }
                else
                {
                    throw new ArgumentException($"Column type {column.Type} is not supported");
                }
            }
        });
        return table;
    }

    internal static TableDescriptor Cells(this TableDescriptor table, TableElement tableElement)
    {
        foreach (var cellElement in tableElement.Cells)
        {
            var cell = table.Cell();
            if (cellElement.Row.HasValue)
            {
                cell.Row(cellElement.Row.Value);
            }
            if (cellElement.Column.HasValue)
            {
                cell.Column(cellElement.Column.Value);
            }
            if (cellElement.RowSpan.HasValue)
            {
                cell.RowSpan(cellElement.RowSpan.Value);
            }
            if (cellElement.ColumnSpan.HasValue)
            {
                cell.ColumnSpan(cellElement.ColumnSpan.Value);
            }
            cell.Element((cellContainer) => Cell(cellContainer, cellElement)).Element(cellElement.Element);
        }
        return table;
    }

    private static IContainer Cell(IContainer cellContainer, Elements.Cell cellElement)
    {
        if (cellElement.Padding.HasValue)
        {
            cellContainer = cellContainer.Padding(cellElement.Padding.Value);
        }
        if (cellElement.BorderWidth.HasValue)
        {
            cellContainer = cellContainer.Border(cellElement.BorderWidth.Value);
        }
        if (!string.IsNullOrWhiteSpace(cellElement.BackgroundColor))
        {
            cellContainer = cellContainer.Background(cellElement.BackgroundColor);
        }
        if (cellElement.MinHeight.HasValue)
        {
            cellContainer = cellContainer.MinHeight(cellElement.MinHeight.Value);
        }
        if (cellElement.MaxHeight.HasValue)
        {
            cellContainer = cellContainer.MaxHeight(cellElement.MaxHeight.Value);
        }
        cellContainer = cellElement.VerticalAlign switch
        {
            CellAlignment.Top => cellContainer.AlignTop(),
            CellAlignment.Bottom => cellContainer.AlignBottom(),
            CellAlignment.Middle => cellContainer.AlignMiddle(),
            _ => cellContainer,
        };
        cellContainer = cellElement.HorizontalAlign switch
        {
            CellAlignment.Center => cellContainer.AlignCenter(),
            CellAlignment.Left => cellContainer.AlignLeft(),
            CellAlignment.Right => cellContainer.AlignRight(),
            _ => cellContainer,
        };
        return cellContainer;
    }

}
