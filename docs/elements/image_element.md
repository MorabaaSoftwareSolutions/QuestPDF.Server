# Image Element

## Description

The Image Element is a simple element that displays an image.

## Properties

| Property | Type | Description | Default value |
| --- | --- | --- | --- |
| Source | `string` | The image source | |
| Width | `float?` | The width | `null` |
| Height | `float?` | The height | `null` |
| Fit | `ImageFit` | The fit | `Width` |
| CompressionQuality | `ImageCompressionQuality?` | The compression quality | `null` |
| Dpi | `int?` | Rastorizor Dpi | `null` |

Available values for `ImageFit`:

- Width
- Height
- Area
- Unproportional

Available values for `ImageCompressionQuality`:

- VeryLow
- Low
- Medium
- High
- VeryHigh
- Best

## Example

A sample JSON of an Image Element:

```json
{
    "$type": "image",
    "source": "https://www.google.com/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png",
    "width": 100,
    "height": 100,
    "fit": "Unproportional",
    "compressionQuality": "high",
    "dpi": 300
}
```
