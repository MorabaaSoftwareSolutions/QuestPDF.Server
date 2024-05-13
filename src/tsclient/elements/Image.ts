import Element, { RowItemConfig } from "./Base";

export interface IImage extends Element {
    source: string;
    width?: number | null;
    height?: number | null;
    fit: "Width" | "Height" | "Area" | "Unproportional";
    compressionQuality?: "Best" | "VeryHigh" | "High" | "Medium" | "Low" | "VeryLow" | null;
    dpi?: number | null;
}

export default class Image implements IImage {
    source: string;
    width?: number | null;
    height?: number | null;
    fit: "Width" | "Height" | "Area" | "Unproportional";
    compressionQuality?: "Best" | "VeryHigh" | "High" | "Medium" | "Low" | "VeryLow" | null;
    dpi?: number | null;
    rowItemConfig?: RowItemConfig | null;

    constructor() {
        this.source = "";
        this.fit = "Width";
    }

    withSource(source: string): Image {
        this.source = source;
        return this;
    }

    withWidth(width: number): Image {
        this.width = width;
        return this;
    }

    withHeight(height: number): Image {
        this.height = height;
        return this;
    }

    withCompressionQuality(compressionQuality: "Best" | "VeryHigh" | "High" | "Medium" | "Low" | "VeryLow"): Image {
        this.compressionQuality = compressionQuality;
        return this;
    }

    withDpi(dpi: number): Image {
        this.dpi = dpi;
        return this;
    }

    withFit(fit: "Width" | "Height" | "Area" | "Unproportional"): Image {
        this.fit = fit;
        return this;
    }

    withRowItemConfig(rowItemConfig: RowItemConfig): Image {
        this.rowItemConfig = rowItemConfig;
        return this;
    }

    withRowItemConfigType(type: "Auto" | "Constant" | "Relative"): Image {
        if (!this.rowItemConfig) {
            this.rowItemConfig = {};
        }
        this.rowItemConfig.type = type;
        return this;
    }

    withRowItemConfigSize(size: number): Image {
        if (!this.rowItemConfig) {
            this.rowItemConfig = {
                type: "Relative",
            };
        }
        this.rowItemConfig.size = size;
        return this;
    }

    withRowItemConfigTypeAndSize(type: "Auto" | "Constant" | "Relative", size: number): Image {
        if (!this.rowItemConfig) {
            this.rowItemConfig = {};
        }
        this.rowItemConfig.type = type;
        this.rowItemConfig.size = size;
        return this;
    }

    build(): IImage {
        if (!this.source) {
            throw new Error("Image source is required");
        }
        return {
            $type: "image",
            ...this,
        };
    }
}
