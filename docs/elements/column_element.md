# Column Element

## Description

The Column Element is a container that displays its children in a column.

## Properties

| Property | Type | Description | Default value |
| --- | --- | --- | --- |
| Elements | `Element[]` | The elements to display | |
| Spacing | `float?` | The spacing between elements | null |

## Example

A sample JSON of a Column Element:

```json
{
    "$type": "column",
    "elements": [
        {
            "$type": "text",
            "text": "Hello World!"
        },
        {
            "$type": "text",
            "text": "Hello World two!"
        }
    ],
    "spacing": 10
}
```
