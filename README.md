# QuestPDF Server

A simple server that accepts json data and generates a PDF using [QuestPDF](https://github.com/QuestPDF/QuestPDF).

## Installation

### Clone

1. Clone this repo to your local machine using

2. Make sure you have [.NET 9+](https://dotnet.microsoft.com/download/dotnet/9.0) installed

3. Add `QuestPDF:LicenseType` to your environment or `appsettings.json` file. The value should be either `Community`, `Professional` or `Enterprise`.

4. Run the server using `dotnet run` or `dotnet watch run` in `src/api`

### Docker

```bash
docker run -p 5000:5000 -e QuestPDF:LicenseType=Community questpdf/server
```

## Docs

Docs are available at [here](./docs/README.md)

### Available Elements

To see the available elements and their properties, check [here](./docs/elements/README.md)

## Usage

### POST /pdf

#### Request

```json
{
    "page": {
        "backgroundColor": "#ffffff",
        "margin": "4",
        "marginUnit": "millimetre"
    },
    "title": {
        "$type": "text",
        "text": "Hey!",
        "fontWeight": "bold",
        "fontSize": 36
    },
    "content": {
        "$type": "column",
        "elements": [
            {
                "$type": "row",
                "elements": [
                    {
                        "$type": "text",
                        "text": "id",
                        "fontWeight": "bold",
                        "rowItemConfig": {
                            "type": "Constant",
                            "size": 64
                        }
                    },
                    {
                        "$type": "text",
                        "text": "image",
                        "fontWeight": "bold",
                        "rowItemConfig": {
                            "type": "Constant",
                            "size": 100
                        }
                    },
                ]
            }
        ]
    }
}
```

## License

QuestPDF Server is licensed under the [MIT License](/LICENSE).
