import Column from "./elements/Column";
import Text from "./elements/Text";
import PDFRequest from "./request";

const buildExample = () => {
    console.log("Building example...");
    const column = new Column().withSpacing(12);
    for (let i = 1; i <= 5; i++) {
        column.withElement(
            new Text()
                .withText(`Text ${i}`)
                .withFontSize(12 + i)
                .withColor(`#0000${i}0`)
                .build()
        );
    }
    const request: PDFRequest = {
        page: {
            pageSize: "A4",
            backgroundColor: "#ffffff",
            margin: 12,
            marginUnit: "Point",
        },
        content: column.build(),
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
