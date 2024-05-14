import { Column, Text, PDFRequest, Table, Cell, TableColumn, Row } from ".";

const buildExample = () => {
    console.log("Building example...");
    const table = new Table()
        .withColumns([
            new TableColumn().withType("Constant").withSize(40).build(),
            new TableColumn().withType("Relative").withSize(2).build(),
            new TableColumn().withType("Relative").withSize(1).build(),
            new TableColumn().withType("Relative").withSize(1).build(),
            new TableColumn().withType("Relative").withSize(1).build(),
        ])
        .withCells([
            new Cell(new Text().withText("ت")).build(),
            new Cell(new Text().withText("المادة")).build(),
            new Cell(new Text().withText("السعر")).build(),
            new Cell(new Text().withText("الكمية")).build(),
            new Cell(new Text().withText("المجموع")).build(),
        ])
        .withBorderWidth(1);
    for (let i = 1; i <= 5; i++) {
        const price = 5000 + Math.random() * 10000;
        const quantity = 1 + Math.random() * 10;
        table.withCells([
            new Cell(new Text().withText(i.toString()).build()).withBorderColor("#000000").withBorderWidth(1).build(),
            new Cell(new Text().withText("مادة " + i).build()).withBorderColor("#000000").withBorderWidth(1).build(),
            new Cell(new Text().withText(price.toLocaleString("en-US")).build()).withBorderColor("#000000").withBorderWidth(1).build(),
            new Cell(new Text().withText(quantity.toLocaleString("en-US")).build()).withBorderColor("#000000").withBorderWidth(1).build(),
            new Cell(new Text().withText((price * quantity).toLocaleString("en-US")).build()).withBorderColor("#000000").withBorderWidth(1).build(),
        ]);
    }
    const request: PDFRequest = {
        page: {
            pageSize: "A4",
            backgroundColor: "#ffffff",
            margin: 12,
            marginUnit: "Point",
            direction: "RTL",
        },
        header: new Row().withElements([new Text().withText("قائمة بيع رقم مدري شگد").build()]).build(),
        content: table.build(),
    };

    console.log("Example built:\n");
    console.log(JSON.stringify(request, null, 4));

    return request;
};

const generatePDF = async (request: PDFRequest) => {
    try {
        const res = await fetch("http://127.0.0.1:5877/pdf", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(request),
        });
        if (res.ok) {
            console.log("PDF generated successfully!");
        } else {
            console.error("Failed to generate PDF:", res.statusText);
            console.error(await res.text());
        }
    } catch (error) {
        console.error("Error generating PDF:", error);
    }
};

const example = buildExample();
generatePDF(example);
