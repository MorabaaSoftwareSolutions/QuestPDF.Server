import Page from "./elements/Page";
import Element from "./elements/Base";

export default interface PDFRequest {
    page: Page;
    content: Element;
    header?: Element | null;
    footer?: Element | null;
    outputFileName?: string | null;
}
