import Page from "./elements/Page";
import Element, { AnyElement } from "./elements/Base";

export default interface PDFRequest {
    page: Page;
    content: AnyElement;
    header?: AnyElement | null;
    footer?: AnyElement | null;
    outputFileName?: string | null;
}
