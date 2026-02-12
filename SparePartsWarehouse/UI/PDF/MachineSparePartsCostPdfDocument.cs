using System;
using System.Collections.Generic;
using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SparePartsWarehouse.CORE.DTO;
namespace SparePartsWarehouse.UI.PDF
{
    public class MachineSparePartsCostPdfDocument : IDocument
    {
        private readonly List<MachineSparePartsCostDetailDto> _items;
        private readonly string? _department;
        private readonly string? _machine;
        private readonly DateTime? _from;
        private readonly DateTime? _to;

        public MachineSparePartsCostPdfDocument(
            List<MachineSparePartsCostDetailDto> items,
            string? department,
            string? machine,
            DateTime? from,
            DateTime? to)
        {
            _items = items;
            _department = department;
            _machine = machine;
            _from = from;
            _to = to;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(25);
                page.ContentFromRightToLeft();
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header().Element(Header);
                page.Content().Element(Table);
                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("تاريخ الطباعة: ");
                    x.Span(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                });
            });
        }

        private void Header(IContainer container)
        {
            container.Column(col =>
            {
                col.Item().Text("تقرير تكلفة قطع غيار الماكينات")
                    .FontSize(18)
                    .Bold()
                    .AlignCenter();

                col.Item().PaddingTop(5).AlignCenter().Text(txt =>
                {
                    if (!string.IsNullOrEmpty(_department))
                        txt.Span($"القسم: {_department}   ");
                    if (!string.IsNullOrEmpty(_machine))
                        txt.Span($"الماكينة: {_machine}");
                });

                col.Item().AlignCenter().Text(txt =>
                {
                    if (_from != null || _to != null)
                        txt.Span($"الفترة: {_from:yyyy/MM/dd} → {_to:yyyy/MM/dd}");
                });

                col.Item().PaddingVertical(8).LineHorizontal(1);
            });
        }

        private void Table(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(c =>
                {
                    c.RelativeColumn(2); // القسم
                    c.RelativeColumn(2); // الماكينة
                    c.RelativeColumn(2); // الفئة
                    c.RelativeColumn(3); // الصنف
                    c.RelativeColumn(2); // الوصف
                    c.RelativeColumn(1); // الكمية
                    c.RelativeColumn(2); // التكلفة
                });

                // Header
                table.Header(header =>
                {
                    HeaderCell(header, "القسم");
                    HeaderCell(header, "الماكينة");
                    HeaderCell(header, "الفئة");
                    HeaderCell(header, "الصنف");
                    HeaderCell(header, "الوصف");
                    HeaderCell(header, "الكمية");
                    HeaderCell(header, "التكلفة");
                });

                decimal totalCost = 0;

                foreach (var i in _items)
                {
                    BodyCell(table, i.FactoryDepartmentName);
                    BodyCell(table, i.MachineName);
                    BodyCell(table, i.CategoryName);
                    BodyCell(table, i.ItemName);
                    BodyCell(table, i.ItemCode);
                    BodyCell(table, i.TotalQuantity.ToString("N2"));
                    BodyCell(table, i.TotalCost.ToString("N2"));

                    totalCost += i.TotalCost;
                }

                // إجمالي
                table.Cell().ColumnSpan(6)
                    .PaddingTop(5)
                    .AlignRight()
                    .Text("إجمالي التكلفة:")
                    .Bold();

                table.Cell()
                    .PaddingTop(5)
                    .AlignCenter()
                    .Text(totalCost.ToString("N2"))
                    .FontColor(Colors.Green.Darken2)
                    .Bold();
            });
        }

        private void HeaderCell(TableCellDescriptor cell, string text)
        {
            cell.Cell()
                .Background(Colors.Grey.Darken2)
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
                .Padding(4)
                .AlignCenter()
                .Text(text);
        }
    }


}
