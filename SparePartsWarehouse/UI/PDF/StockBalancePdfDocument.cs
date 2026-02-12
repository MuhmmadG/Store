using System;
using System.Collections.Generic;
using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SparePartsWarehouse.CORE.DTO;
namespace SparePartsWarehouse.UI.PDF
{
   

    public class StockBalancePdfDocument : IDocument
    {
        private readonly List<StockBalanceDto> _items;
        private readonly string _category;

        public StockBalancePdfDocument(
            List<StockBalanceDto> items,
            string category)
        {
            _items = items;
            _category = category;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.DefaultTextStyle(x => x.FontSize(11));
                page.ContentFromRightToLeft();

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeTable);
                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("تاريخ الطباعة: ");
                    x.Span(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                });
            });
        }

        private void ComposeHeader(IContainer container)
        {
            container.Column(col =>
            {
                col.Item().Text("تقرير رصيد المخزن")
                    .FontSize(18)
                    .Bold()
                    .AlignCenter();

                col.Item().Text($"التصنيف: {_category}")
                    .AlignCenter();

                col.Item().PaddingVertical(10).LineHorizontal(1);
            });
        }

        private void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(2); // القسم
                    columns.RelativeColumn(3); // الصنف
                    columns.RelativeColumn(2); // الكود
                    columns.RelativeColumn(2); // الوارد
                    columns.RelativeColumn(2); // المنصرف
                    columns.RelativeColumn(2); // الرصيد
                });

                // Header
                table.Header(header =>
                {
                    TableCellDescriptor header1 = header;
                    HeaderCell(header, "القسم");
                    HeaderCell(header, "اسم الصنف");
                    HeaderCell(header, "الكود");
                    HeaderCell(header, "الوارد");
                    HeaderCell(header, "المنصرف");
                    HeaderCell(header, "الرصيد");
                });

                // Rows
                foreach (var item in _items)
                {
                    BodyCell(table, item.CategoryName);
                    BodyCell(table, item.ItemName);
                    BodyCell(table, item.ItemCode);
                    BodyCell(table, item.TotalInQty.ToString());
                    BodyCell(table, item.TotalOutQty.ToString());

                    table.Cell().Element(c =>
                    {
                        c.BorderBottom(1)
                         .Padding(5)
                         .AlignCenter()
                         .Text(item.BalanceQty.ToString())
                         .FontColor(item.BalanceQty < 0 ? Colors.Red.Medium : Colors.Green.Darken2);
                    });
                }
            });
        }

        private void HeaderCell(TableCellDescriptor cell, string text)
        {
            cell.Cell()
                .Background(Colors.Grey.Darken2)
                .Border(1)
                .Padding(5)
                .AlignCenter()
                .Text(text)
                .FontColor(Colors.White)
                .Bold();
        }

        private void BodyCell(TableDescriptor table, string text)
        {
            table.Cell()
                .BorderBottom(1)
                .Padding(5)
                .AlignCenter()
                .Text(text);
        }

    }

}
