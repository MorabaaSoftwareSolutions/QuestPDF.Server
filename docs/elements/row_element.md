# Row Element

## Description

The Row Element is a container that displays its children in a row.

## Properties

| Property | Type | Description | Default value |
| --- | --- | --- | --- |
| Elements | `Element[]` | The elements to display | |
| Spacing | `float?` | The spacing between elements | null |

## Example

A sample JSON of a Row Element:

```json
{
    "$type": "row",
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
