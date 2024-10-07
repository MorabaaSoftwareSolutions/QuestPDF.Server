# QuestPDF Server Docs

QuestPDF Server is an open-source API server that allows you to generate PDF documents using a simple JSON-based API.

It's powered by [QuestPDF](https://github.com/QuestPDF/QuestPDF), a .NET library that allows you to create PDF documents using a fluent API.

## Elements

To create a document, you need to create a JSON file that describes the document. The JSON file is composed of elements. Each element has a type and a set of properties. The type is used to determine which element to use, and the properties are used to configure the element.

See the [Elements](/docs/elements/README.md) page for a list of all available elements.

## Page Specs

The page specs are used to configure the document. They are used to set the page size, margins, and other properties.

| Property | Type | Description | Default value |
| --- | --- | --- | --- |
| BackgroundColor | `string` | The background color of the page | `#ffffff` |
| Margin | `float` | The margin of the page | `null` |
| MarginUnit | `Unit?` | The unit of the margin | `null` |
| DefaultFontSize | `float` | The default font size | `12` |
| PageSize | `PageSize` | The size of the page | `A4` |
| FontFamily | `string?` | The font family | `null` |
| FontUris | `string[]` | The font uris | `null` |
| Direction | `LTR` or `RTL` | The direction of the page | `LTR` |

Allowed values for `Unit`:

- `Point`
- `Meter`
- `Centimeter`
- `Millimeter`
- `Feet`
- `Inch`
- `Mil` // 1/1000th of an inch

Allowed values for `PageSize`:

- `A0`
- `A1`
- `A2`
- `A3`
- `A4`
- `A5`
- `A6`
- `A7`
- `A8`
- `A9`
- `A10`
- `B0`
- `B1`
- `B2`
- `B3`
- `B4`
- `B5`
- `B6`
- `B7`
- `B8`
- `B9`
- `B10`
- `Env10`
- `EnvC4`
- `EnvDL`
- `Executive`
- `Legal`
- `Letter`
- `ARCH_A`
- `ARCH_B`
- `ARCH_C`
- `ARCH_D`
- `ARCH_E`
- `ARCH_E1`
- `ARCH_E2`
- `ARCH_E3`
