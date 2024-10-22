# Text Element

## Description

The Text Element is a simple element that displays a text.

## Properties

| Property | Type | Description | Default value |
| --- | --- | --- | --- |
| Text | string | The text to display | |
| FontSize | int | The font size | document's default |
| FontWeight | enum | The font weight. | `Normal` |
| LineHeight | int | The line height | `0` |
| Color | string | The text color | `#000000` |
| IsItalic | bool? | Whether the text should be italic | `null` |
| TextDecoration | enum | The text decoration | `None` |
| Script | enum | The script | `Normal` |
| LetterSpacing | float? | The letter spacing | `null` |
| Padding | float? | The padding | `null` |
| PaddingUnit | enum | The padding unit | `Point` |
| BorderUnit | enum | The border unit | `Point` |
| BorderColor | string? | The border color | `#000000` |
| BorderWidth | float? | The border width | `null` |
| BorderTopWidth | float? | The top border width | `null` |
| BorderRightWidth | float? | The right border width | `null` |
| BorderBottomWidth | float? | The bottom border width | `null` |
| BorderLeftWidth | float? | The left border width | `null` |

Available values for `FontWeight`:

- Thin (100)
- ExtraLight (200)
- Light (300)
- Normal (400)
- Medium (500)
- SemiBold (600)
- Bold (700)
- ExtraBold (800)
- Black (900)
- ExtraBlack (1000)

Available values for `TextDecoration`:

- None
- Underline
- Strike

## Example

A sample JSON of a Text Element:

```json
{
    "$type": "text",
    "text": "Hello World!",
    "fontSize": 24,
    "fontWeight": "bold",
    "color": "#ff0000"
}
```
