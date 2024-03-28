using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

QuestPDF.Settings.License = LicenseType.Community;

var documents = new List<IDocument>();

for (int i = 0; i < 3; i++)
{
    documents.Add(CreateDocument(Guid.NewGuid().ToString().Substring(0, 6)));
}

var doc = Document.Merge(documents);

doc.ShowInPreviewer();


static IDocument CreateDocument(string name)
{
    var abeePrimaryColor = "#594ae2";
    var abeePrimaryLightenColor = "#766ae7";

    var tableTitleFontSize = 16;
    var tableHeaderFontSize = 14;
    var tableBodyFontSize = 12;

    return Document.Create(container =>
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(1.5f, Unit.Centimetre);
            page.PageColor(Colors.White);
            page.DefaultTextStyle(x => x.FontSize(16));

            page.Header()
                .Text("Lista de compras (16 lojas)")
                .SemiBold().FontSize(30).FontColor(Colors.Blue.Medium);
            
            page.Content()
                .PaddingVertical(1, Unit.Centimetre)
                .Column(column =>
                {
                    column.Item().Row(row =>
                    {
                        row.RelativeItem()
                            .Background(abeePrimaryColor)
                            .ExtendHorizontal()
                            .Height(30)
                            .Text(x =>
                            {
                                x.Span(name)
                                    .FontColor(Colors.White)
                                    .Italic();

                                x.AlignCenter();
                            });
                    });

                    column.Spacing(15);

                    foreach (var company in new List<int> { 1, 2, 3 })
                    {
                        column.Item().Text(x =>
                        {
                            x.Element(y => y
                                .Text($"Abare {company}")
                                .FontSize(26)
                                .Bold()
                            );
                        });

                        column.Item().Border(1).Table(table =>
                        {
                            table.ColumnsDefinition(x =>
                            {
                                x.RelativeColumn();
                                x.RelativeColumn();
                                x.RelativeColumn();
                            });

                            //Table title
                            table.Cell().Row(1).ColumnSpan(3).Border(1)
                                .Background(abeePrimaryColor)
                                .Text(text =>
                                {
                                    text.DefaultTextStyle(y => y
                                        .FontColor(Colors.White)
                                        .FontSize(tableTitleFontSize)
                                        .Bold()
                                    );

                                    text.Span("Água verde");
                                    text.AlignCenter();
                                });

                            //Table HEADER
                            table.Cell().Row(2).Column(1).Background(abeePrimaryLightenColor).Border(1).AlignCenter()
                                .Text("Produto").FontSize(tableHeaderFontSize).FontColor(Colors.White);

                            table.Cell().Row(2).Column(2).Background(abeePrimaryLightenColor).Border(1).AlignCenter()
                                .Text("Quantidade")
                                .FontSize(tableHeaderFontSize).FontColor(Colors.White);

                            table.Cell().Row(2).Column(3).Background(abeePrimaryLightenColor).Border(1).AlignCenter()
                                .Text("Un. Medida")
                                .FontSize(tableHeaderFontSize).FontColor(Colors.White);


                            //Table body
                            for (uint i = 3; i <= 50; i++)
                            {
                                var isEvenNumber = i % 2 == 0;

                                table.Cell().Row(i).Column(1)
                                    .Background(isEvenNumber ? Colors.Grey.Lighten2 : Colors.White)
                                    .AlignCenter().Text("Cerveja").FontSize(tableBodyFontSize);

                                table.Cell().Row(i).Column(2)
                                    .Background(isEvenNumber ? Colors.Grey.Lighten2 : Colors.White).AlignCenter()
                                    .Text("1")
                                    .FontSize(tableBodyFontSize);

                                table.Cell().Row(i).Column(3)
                                    .Background(isEvenNumber ? Colors.Grey.Lighten2 : Colors.White)
                                    .AlignCenter().Text("KG")
                                    .FontSize(tableBodyFontSize);
                            }
                        });

                        column.Item().PageBreak();
                    }
                });

            page.Footer()
                .AlignCenter()
                .Text(x =>
                {
                    x.DefaultTextStyle(x => x
                        .FontSize(14)
                        .Light()
                    );

                    x.Span("Página ");
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });
        });
    });
}