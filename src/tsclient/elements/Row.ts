import Element, { RowItemConfig } from "./Base";
import { IColumn } from "./Column";
import { IImage } from "./Image";
import { ITable } from "./Table";
import { IText } from "./Text";

type AnyElement = Element<IColumn | IRow | IText | IImage | ITable>;

export interface IRow extends Element<IRow> {
    elements: AnyElement[];
    spacing: number | null;
    build(): { $type: "row" } & Element<IRow>;
}

export default class Row implements IRow {
    elements: AnyElement[];
    spacing: number | null;
    rowItemConfig?: RowItemConfig | null;

    constructor() {
        this.elements = [];
        this.spacing = null;
    }

    withSpacing(spacing: number): Row {
        this.spacing = spacing;
        return this;
    }

    withElements(elements: AnyElement[]): Row {
        this.elements = elements;
        return this;
    }

    withElement(element: AnyElement): Row {
        this.elements.push(element);
        return this;
    }

    withoutElement(element: AnyElement): Row {
        this.elements = this.elements.filter((e) => e !== element);
        return this;
    }

    withRowItemConfig(rowItemConfig: RowItemConfig): Row {
        this.rowItemConfig = rowItemConfig;
        return this;
    }

    build(): { $type: "row" } & Element<IRow> {
        return {
            $type: "row",
            ...this,
        };
    }
}
