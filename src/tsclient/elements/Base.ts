import { IColumn } from "./Column";
import { IImage } from "./Image";
import { IRow } from "./Row";
import { ITable } from "./Table";
import { IText } from "./Text";

export default interface Element<T extends Element<T>> {
    rowItemConfig?: RowItemConfig | null;

    build(): { $type: "table" | "cell" | "text" | "image" | "column" | "row" } & Element<T>;
}

export interface RowItemConfig {
    type?: "Auto" | "Constant" | "Relative" | null;
    size?: number | null;
}

export type AnyElement = Element<IColumn | IRow | IText | IImage | ITable>;
