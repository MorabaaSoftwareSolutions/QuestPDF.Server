import Element, { RowItemConfig } from "./Base";

export interface IRow extends Element {
    elements: Element[];
    spacing: number | null;
}

export default class Row implements IRow {
    elements: Element[];
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

    withElements(elements: Element[]): Row {
        this.elements = elements;
        return this;
    }

    withElement(element: Element): Row {
        this.elements.push(element);
        return this;
    }

    withoutElement(element: Element): Row {
        this.elements = this.elements.filter((e) => e !== element);
        return this;
    }

    withRowItemConfig(rowItemConfig: RowItemConfig): Row {
        this.rowItemConfig = rowItemConfig;
        return this;
    }

    build() {
        return {
            $type: "row",
            ...this,
        };
    }
}
