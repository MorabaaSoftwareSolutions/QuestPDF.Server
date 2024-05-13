export default interface Element {
    rowItemConfig?: RowItemConfig | null;
}

export interface RowItemConfig {
    type?: "Auto" | "Constant" | "Relative" | null;
    size?: number | null;
}
