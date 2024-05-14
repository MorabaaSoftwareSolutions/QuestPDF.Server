import Element, { AnyElement, RowItemConfig } from "./Base";

export interface ITable extends Element<ITable> {
    cells: ICell[];
    columns: ITableColumn[];
    borderWidth?: number | null;
    rowItemConfig?: RowItemConfig | null;

    build(): { $type: "table" } & Element<ITable>;
}

export interface ITableColumn {
    size: number;
    type: "Constant" | "Relative";
}

export interface ICell extends Element<ICell> {
    element: AnyElement;
    padding?: number | null;
    borderWidth?: number | null;
    backgroundColor?: string | null;
    borderColor?: string | null;
    minHeight?: number | null;
    maxHeight?: number | null;
    verticalAlign?: "Top" | "Bottom" | "Center" | "Middle" | "Left" | "Right" | null;
    horizontalAlign?: "Top" | "Bottom" | "Center" | "Middle" | "Left" | "Right" | null;
    row?: number | null;
    column?: number | null;
    rowSpan?: number | null;
    columnSpan?: number | null;

    build(): { $type: "cell" } & ICell;
}

export class TableColumn implements ITableColumn {
    size: number;
    type: "Constant" | "Relative";

    constructor() {
        this.size = 0;
        this.type = "Constant";
    }

    withSize(size: number): TableColumn {
        this.size = size;
        return this;
    }

    withType(type: "Constant" | "Relative"): TableColumn {
        this.type = type;
        return this;
    }

    build() {
        return this;
    }
}

export class Cell implements ICell {
    element: AnyElement;
    padding?: number | null;
    borderWidth?: number | null;
    backgroundColor?: string | null;
    borderColor?: string | null;
    minHeight?: number | null;
    maxHeight?: number | null;
    verticalAlign?: "Top" | "Bottom" | "Center" | "Middle" | "Left" | "Right" | null;
    horizontalAlign?: "Top" | "Bottom" | "Center" | "Middle" | "Left" | "Right" | null;
    row?: number | null;
    column?: number | null;
    rowSpan?: number | null;
    columnSpan?: number | null;

    constructor(el: AnyElement) {
        if ("$type" in el) {
            this.element = el;
        } else {
            this.element = el.build();
        }
    }

    withPadding(padding: number): Cell {
        this.padding = padding;
        return this;
    }

    withBorderWidth(borderWidth: number): Cell {
        this.borderWidth = borderWidth;
        return this;
    }

    withBackgroundColor(backgroundColor: string): Cell {
        this.backgroundColor = backgroundColor;
        return this;
    }

    withBorderColor(borderColor: string): Cell {
        this.borderColor = borderColor;
        return this;
    }

    withMinHeight(minHeight: number): Cell {
        this.minHeight = minHeight;
        return this;
    }

    withMaxHeight(maxHeight: number): Cell {
        this.maxHeight = maxHeight;
        return this;
    }

    withVerticalAlign(verticalAlign: "Top" | "Bottom" | "Center" | "Middle" | "Left" | "Right"): Cell {
        this.verticalAlign = verticalAlign;
        return this;
    }

    withHorizontalAlign(horizontalAlign: "Top" | "Bottom" | "Center" | "Middle" | "Left" | "Right"): Cell {
        this.horizontalAlign = horizontalAlign;
        return this;
    }

    withRow(row: number): Cell {
        this.row = row;
        return this;
    }

    withColumn(column: number): Cell {
        this.column = column;
        return this;
    }

    withRowSpan(rowSpan: number): Cell {
        this.rowSpan = rowSpan;
        return this;
    }

    withColumnSpan(columnSpan: number): Cell {
        this.columnSpan = columnSpan;
        return this;
    }

    build(): { $type: "cell" } & ICell {
        return {
            $type: "cell",
            ...this,
        };
    }
}

export class Table implements ITable {
    cells: ICell[];
    columns: ITableColumn[];
    borderWidth?: number | null;
    rowItemConfig?: RowItemConfig | null;

    constructor() {
        this.cells = [];
        this.columns = [];
    }

    withCells(cells: ICell[]): Table {
        this.cells = [...this.cells, ...cells];
        return this;
    }

    withColumns(columns: ITableColumn[]): Table {
        this.columns = [...this.columns, ...columns];
        return this;
    }

    withCell(cell: ICell): Table {
        this.cells.push(cell);
        return this;
    }

    withColumn(column: ITableColumn): Table {
        this.columns.push(column);
        return this;
    }

    withBorderWidth(borderWidth: number): Table {
        this.borderWidth = borderWidth;
        return this;
    }

    build(): { $type: "table" } & Element<ITable> {
        for (let i = 0; i < this.cells.length; i++) {
            if ("$type" in this.cells[i]) {
                continue;
            }
            this.cells[i] = this.cells[i].build();
        }
        return {
            $type: "table",
            ...this,
        };
    }
}
