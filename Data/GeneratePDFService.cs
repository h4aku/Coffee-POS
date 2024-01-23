using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using static BisleriumCafe.Components.Pages.GeneratePDF;

namespace BisleriumCafe.Data
{
    internal class GeneratePDFServices
    {
        public void GenerateDailyReport(string reportFilePath, DateTime day, float TotalSales, List<CoffeeStatistics> topFiveCoffees, List<AddinStatistics> topFiveAddIns)
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.DefaultTextStyle(x => x.FontSize(18));

                    page.Header()
                        .Text("Report")
                        .SemiBold().FontSize(30);

                    page.Content().Column(x =>
                    {
                        x.Item().Text($"DateTime: {day}");
                        x.Spacing(20);
                        x.Item().Text("Top 5 Coffees");

                        x.Item().Table(table =>
                        {
                            table.ColumnsDefinition(column =>
                            {
                                column.RelativeColumn();
                                column.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Coffee Name");
                                header.Cell().Text("Quantity");
                            });

                            foreach (var item in topFiveCoffees)
                            {
                                table.Cell().Text(item.CoffeeName);
                                table.Cell().Text(item.TotalCoffeeQuantitySold);
                            }
                        });

                        x.Item().Text("Top 5 AddIns");

                        x.Item().Table(table =>
                        {
                            table.ColumnsDefinition(column =>
                            {
                                column.RelativeColumn();
                                column.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("AddIn Name");
                                header.Cell().Text("Quantity");
                            });

                            foreach (var item in topFiveAddIns)
                            {
                                table.Cell().Text(item.AddinsName);
                                table.Cell().Text(item.TotalAddinsQuantitySold);
                            }
                        });

                        x.Spacing(20);
                        x.Item().Text($"Total Sales Amount: Rs. {TotalSales}").Bold();
                    });

                });
            })
        .GeneratePdf(reportFilePath);
        }
    }
}