export default interface Element<T extends Element<T>> {
    rowItemConfig?: RowItemConfig | null;

    build(): { $type: "table" | "cell" | "text" | "image" | "column" | "row" } & Element<T>;
}

export interface RowItemConfig {
    type?: "Auto" | "Constant" | "Relative" | null;
    size?: number | null;
}
