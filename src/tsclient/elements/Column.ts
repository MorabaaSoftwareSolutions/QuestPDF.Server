import Element, { RowItemConfig } from "./Base";

export interface IColumn extends Element {
    elements: Element[];
    spacing: number | null;
}

export default class Column implements IColumn {
    elements: Element[];
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

    withElements(elements: Element[]): Column {
        this.elements = elements;
        return this;
    }

    withElement(element: Element): Column {
        this.elements.push(element);
        return this;
    }

    withoutElement(element: Element): Column {
        this.elements = this.elements.filter((e) => e !== element);
        return this;
    }

    withRowItemConfig(rowItemConfig: RowItemConfig): Column {
        this.rowItemConfig = rowItemConfig;
        return this;
    }

    build() {
        return {
            $type: "column",
            ...this,
        };
    }
}
