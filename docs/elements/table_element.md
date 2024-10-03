# Table Element

## Description

The Table Element is a container that displays its children in a table.

## Properties

| Property | Type | Description | Default value |
| --- | --- | --- | --- |
| Cells | `Cell[]` | The cells to display | |
| Columns | `Column[]` | The columns of the table | |
| BorderWidth | `float?` | The width of the border | `null` |

## Column Properties

| Property | Type | Description | Default value |
| --- | --- | --- | --- |
| Size | `float` | The size of the column | |
| Type | `Constant` or `Relative` | The type of the column | |

## Cell Properties

| Property | Type | Description | Default value |
| --- | --- | --- | --- |
| Elemet | `Element` | The element to display | |
| Padding | `float?` | The padding of the cell | `null` |
| BorderWidth | `float?` | The width of the border | `null` |
| BorderColor | `Color?` | The color of the border | `null` |
| BackgroundColor | `Color?` | The color of the background | `null` |
| MinHeight | `float?` | The minimum height of the cell | `null` |
| MaxHeight | `float?` | The maximum height of the cell | `null` |
| Row | `uint?` | The row of the cell | `null` |
| Column | `uint?` | The column of the cell | `null` |
| RowSpan | `uint?` | The row span of the cell | `null` |
| ColumnSpan | `uint?` | The column span of the cell | `null` |
| HorizontalAlignment | `CellAlignment?` | The horizontal alignment of the cell | `null` |
| VerticalAlignment | `CellAlignment?` | The vertical alignment of the cell | `null` |

CellAlignment is an enum with the following values:
`Top`, `Bottom`, `Center`, `Middle`, `Left`, `Right`,

## Example

A sample JSON of a Table Element:

```json
{
    "$type": "table",
    "cells": [
        {
            "$type": "cell",
            "element": {
                "$type": "text",
                "text": "Hello World!"
            }
        },
        {
            "$type": "cell",
            "element": {
                "$type": "text",
                "text": "Hello World two!"
            }
        }
    ],
    "columns": [
        {
            "$type": "column",
            "size": 50,
            "type": "Constant"
        },
        {
            "$type": "column",
            "size": 50,
            "type": "Constant"
        }
    ],
    "borderWidth": 1
}
```
