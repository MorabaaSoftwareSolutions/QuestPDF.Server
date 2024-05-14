import Element, { RowItemConfig } from "./Base";
import { IImage } from "./Image";
import IRow from "./Row";
import { ITable } from "./Table";
import { IText } from "./Text";

type AnyElement = Element<IColumn | IRow | IText | IImage | ITable>;

export interface IColumn extends Element<IColumn> {
    elements: AnyElement[];
    spacing: number | null;
    build(): { $type: "column" } & Element<IColumn>;
}

export default class Column implements IColumn {
    elements: AnyElement[];
    spacing: number | null;
    rowItemConfig?: RowItemConfig | null;

    constructor() {
        this.elements = [];
        this.spacing = null;
    }

    withSpacing(spacing: number): Column {
        this.spacing = spacing;
        return this;
    }

    withElements(elements: AnyElement[]): Column {
        this.elements = elements;
        return this;
    }

    withElement(element: AnyElement): Column {
        this.elements.push(element);
        return this;
    }

    withoutElement(element: AnyElement): Column {
        this.elements = this.elements.filter((e) => e !== element);
        return this;
    }

    withRowItemConfig(rowItemConfig: RowItemConfig): Column {
        this.rowItemConfig = rowItemConfig;
        return this;
    }

    build(): { $type: "column" } & Element<IColumn> {
        return {
            $type: "column",
            ...this,
        };
    }
}
