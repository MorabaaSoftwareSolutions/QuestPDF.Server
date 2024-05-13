import Element, { RowItemConfig } from "./Base";

export interface IText extends Element {
    text: string;
    fontSize?: number | null;
    fontWeight: "Thin" | "ExtraLight" | "Normal" | "Medium" | "SemiBold" | "Bold" | "ExtraBold" | "Black" | "ExtraBlack";
    script: "Normal" | "Sub" | "Super";
    padding?: number | null;
    letterSpacing?: number | null;
    alignment?: "Left" | "Center" | "Right" | "Justify" | null;
    textDecoration: "None" | "Underline" | "Strike";
    lineHeight?: number | null;
    color?: string | null;
    isItalic?: boolean | null;
}

export default class Text implements IText {
    rowItemConfig?: RowItemConfig | null;
    text: string;
    fontSize?: number | null;
    fontWeight: "Thin" | "ExtraLight" | "Normal" | "Medium" | "SemiBold" | "Bold" | "ExtraBold" | "Black" | "ExtraBlack";
    script: "Normal" | "Sub" | "Super";
    padding?: number | null;
    letterSpacing?: number | null;
    alignment?: "Left" | "Center" | "Right" | "Justify" | null;
    textDecoration: "None" | "Underline" | "Strike";
    lineHeight?: number | null;
    color?: string | null;
    isItalic?: boolean | null;

    constructor() {
        this.text = "";
        this.fontWeight = "Normal";
        this.textDecoration = "None";
        this.script = "Normal";
    }

    withFontSize(fontSize: number): Text {
        this.fontSize = fontSize;
        return this;
    }

    withFontWeight(fontWeight: "Thin" | "ExtraLight" | "Normal" | "Medium" | "SemiBold" | "Bold" | "ExtraBold" | "Black" | "ExtraBlack"): Text {
        this.fontWeight = fontWeight;
        return this;
    }

    withScript(script: "Normal" | "Sub" | "Super"): Text {
        this.script = script;
        return this;
    }

    withPadding(padding: number): Text {
        this.padding = padding;
        return this;
    }

    withLetterSpacing(letterSpacing: number): Text {
        this.letterSpacing = letterSpacing;
        return this;
    }

    withAlignment(alignment: "Left" | "Center" | "Right" | "Justify"): Text {
        this.alignment = alignment;
        return this;
    }

    withTextDecoration(textDecoration: "None" | "Underline" | "Strike"): Text {
        this.textDecoration = textDecoration;
        return this;
    }

    withLineHeight(lineHeight: number): Text {
        this.lineHeight = lineHeight;
        return this;
    }

    withColor(color: string): Text {
        this.color = color;
        return this;
    }

    withItalic(): Text {
        this.isItalic = true;
        return this;
    }

    withRowItemConfig(rowItemConfig: RowItemConfig): Text {
        this.rowItemConfig = rowItemConfig;
        return this;
    }

    withRowItemConfigType(type: "Auto" | "Constant" | "Relative"): Text {
        if (!this.rowItemConfig) {
            this.rowItemConfig = {};
        }
        this.rowItemConfig.type = type;
        return this;
    }

    withRowItemConfigSize(size: number): Text {
        if (!this.rowItemConfig) {
            this.rowItemConfig = {
                type: "Relative",
            };
        }
        this.rowItemConfig.size = size;
        return this;
    }

    withRowItemConfigTypeAndSize(type: "Auto" | "Constant" | "Relative", size: number): Text {
        if (!this.rowItemConfig) {
            this.rowItemConfig = {};
        }
        this.rowItemConfig.type = type;
        this.rowItemConfig.size = size;
        return this;
    }

    withText(text: string): Text {
        this.text = text;
        return this;
    }

    build() {
        return {
            $type: "text",
            ...this,
        };
    }
}
