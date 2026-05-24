using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using ProductionSystem.Application.Helpers;
using ProductionSystem.Application.Security;
using ProductionSystem.Domain.DTOs;
using ProductionSystem.Domain.IRepositories;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionSystem.Controllers
{
    public class ReportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [PermissionChecker(RoleChecker.Report)]

        public IActionResult Production()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetData(int? productId, int? parameterId, string fromDate, string toDate)
        {
            var receipts = await _unitOfWork.ProductionReceipts.GetAllAsync();

            // فیلتر کالا
            if (productId.HasValue && productId > 0)
                receipts = receipts.Where(r => r.ProductId == productId.Value).ToList();

            // فیلتر پارامتر
            if (parameterId.HasValue && parameterId > 0)
                receipts = receipts.Where(r => r.ProductReceiptParameters
                    .Any(p => p.ProductParameterId == parameterId.Value)).ToList();

            // فیلتر تاریخ
            if (!string.IsNullOrEmpty(fromDate))
            {
                var from = fromDate.ToMiladi();
                receipts = receipts.Where(r => r.CreatedAt >= from).ToList();
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                var to = toDate.ToMiladi();
                receipts = receipts.Where(r => r.CreatedAt <= to).ToList();
            }

            var result = receipts.Select(r => new
            {
                orderTitle = r.Order != null ? r.Order.Title : "-",
                productTitle = r.Product != null ? r.Product.Title : "-",
                productCode = r.Product != null ? r.Product.Code : "-",

                // ✅ نمایش پارامتر و مقدار (اصلاح شده)
                parameterTitle = r.ProductReceiptParameters != null && r.ProductReceiptParameters.Any()
                    ? string.Join(" | ", r.ProductReceiptParameters.Select(p =>
                        p.ProductParameter != null ? p.ProductParameter.Title : "-"))
                    : "-",

                parameterValue = r.ProductReceiptParameters != null && r.ProductReceiptParameters.Any()
                    ? string.Join(" | ", r.ProductReceiptParameters.Select(p =>
                        p.ParameterValue != null ? p.ParameterValue.Title : ""))
                    : "",

                quantity = r.Quantity,
                createdAt = r.CreatedAt.ToShamsi()
            });

            return Json(result);
        }
        public async Task<IActionResult> ExportExcel(int? productId, int? parameterId, string fromDate, string toDate)
        {
            var receipts = await _unitOfWork.ProductionReceipts.GetAllAsync();

            if (productId.HasValue && productId > 0)
                receipts = receipts.Where(r => r.ProductId == productId.Value).ToList();
            //if (parameterId.HasValue && parameterId > 0)
            //    receipts = receipts.Where(r => r.ProductParameterId == parameterId.Value).ToList();
            if (!string.IsNullOrEmpty(fromDate))
            {
                var from = fromDate.ToMiladi();
                receipts = receipts.Where(r => r.CreatedAt.Date >= from.Date).ToList();
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                var to = toDate.ToMiladi();
                receipts = receipts.Where(r => r.CreatedAt.Date <= to.Date).ToList();
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("گزارش تولید");

            // راست چین کردن sheet
            sheet.View.RightToLeft = true;

            // تیترها
            var headers = new[] { "ردیف", "سفارش", "کالا", "کد کالا", "پارامتر", "مقدار", "تاریخ" };
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = sheet.Cells[1, i + 1];
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Font.Color.SetColor(System.Drawing.Color.White);
                cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(0, 70, 127));
                cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }

            // داده‌ها
            for (int i = 0; i < receipts.Count; i++)
            {
                var r = receipts[i];
                sheet.Cells[i + 2, 1].Value = i + 1;
                sheet.Cells[i + 2, 2].Value = r.Order != null ? r.Order.Title : "-";
                sheet.Cells[i + 2, 3].Value = r.Product != null ? r.Product.Title : "-";
                sheet.Cells[i + 2, 4].Value = r.Product != null ? r.Product.Code : "-"; 
                sheet.Cells[i + 2, 5].Value = r.ProductReceiptParameters != null && r.ProductReceiptParameters.Any()
                    ? string.Join(" | ", r.ProductReceiptParameters.Select(p => p.ProductParameter?.Title ?? "-"))
                    : "-"; sheet.Cells[i + 2, 6].Value = r.Quantity;
                sheet.Cells[i + 2, 7].Value = r.CreatedAt.ToShamsi();

                // یک در میان رنگ ردیف‌ها
                if (i % 2 == 0)
                {
                    using var range = sheet.Cells[i + 2, 1, i + 2, 7];
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(235, 241, 250));
                }
            }

            // auto fit
            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

            var stream = new System.IO.MemoryStream(package.GetAsByteArray());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
        }
        public async Task<IActionResult> ExportPdf(int? productId, int? parameterId, string fromDate, string toDate)
        {
            var receipts = await _unitOfWork.ProductionReceipts.GetAllAsync();

            if (productId.HasValue && productId > 0)
                receipts = receipts.Where(r => r.ProductId == productId.Value).ToList();

            //if (parameterId.HasValue && parameterId > 0)
            //    receipts = receipts.Where(r => r.ProductParameterId == parameterId.Value).ToList();

            if (!string.IsNullOrEmpty(fromDate))
            {
                var from = fromDate.ToMiladi();
                receipts = receipts.Where(r => r.CreatedAt >= from).ToList();
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                var to = toDate.ToMiladi();
                receipts = receipts.Where(r => r.CreatedAt <= to).ToList();
            }

            // ====================== ساخت PDF ======================
            var document = new MigraDocCore.DocumentObjectModel.Document();
            var section = document.AddSection();

            section.PageSetup.PageFormat = MigraDocCore.DocumentObjectModel.PageFormat.A4;
            section.PageSetup.Orientation = MigraDocCore.DocumentObjectModel.Orientation.Portrait;
            section.PageSetup.LeftMargin = MigraDocCore.DocumentObjectModel.Unit.FromCentimeter(1.5);
            section.PageSetup.RightMargin = MigraDocCore.DocumentObjectModel.Unit.FromCentimeter(1.5);
            section.PageSetup.TopMargin = MigraDocCore.DocumentObjectModel.Unit.FromCentimeter(2);
            section.PageSetup.BottomMargin = MigraDocCore.DocumentObjectModel.Unit.FromCentimeter(2);

            // عنوان گزارش
            var title = section.AddParagraph("گزارش تولید");
            title.Format.Font.Size = 16;
            title.Format.Font.Bold = true;
            title.Format.Alignment = MigraDocCore.DocumentObjectModel.ParagraphAlignment.Center;
            title.Format.SpaceAfter = MigraDocCore.DocumentObjectModel.Unit.FromCentimeter(1);

            // جدول
            var table = section.AddTable();
            table.Borders.Color = MigraDocCore.DocumentObjectModel.Colors.Black;
            table.Borders.Width = 0.5;

            // ستون‌ها
            table.AddColumn(MigraDocCore.DocumentObjectModel.Unit.FromCentimeter(1));     // ردیف
            table.AddColumn(MigraDocCore.DocumentObjectModel.Unit.FromCentimeter(3.5));   // سفارش
            table.AddColumn(MigraDocCore.DocumentObjectModel.Unit.FromCentimeter(4));     // کالا
            table.AddColumn(MigraDocCore.DocumentObjectModel.Unit.FromCentimeter(2.5));   // کد کالا
            table.AddColumn(MigraDocCore.DocumentObjectModel.Unit.FromCentimeter(3.5));   // پارامتر
            table.AddColumn(MigraDocCore.DocumentObjectModel.Unit.FromCentimeter(2));     // مقدار
            table.AddColumn(MigraDocCore.DocumentObjectModel.Unit.FromCentimeter(2.5));   // تاریخ

            // هدر جدول
            var headerRow = table.AddRow();
            headerRow.Shading.Color = MigraDocCore.DocumentObjectModel.Colors.Gray;

            headerRow.Cells[0].AddParagraph("ردیف").Format.Font.Bold = true;
            headerRow.Cells[1].AddParagraph("سفارش").Format.Font.Bold = true;
            headerRow.Cells[2].AddParagraph("کالا").Format.Font.Bold = true;
            headerRow.Cells[3].AddParagraph("کد کالا").Format.Font.Bold = true;
            headerRow.Cells[4].AddParagraph("پارامتر").Format.Font.Bold = true;
            headerRow.Cells[5].AddParagraph("مقدار").Format.Font.Bold = true;
            headerRow.Cells[6].AddParagraph("تاریخ").Format.Font.Bold = true;

            // ردیف‌های داده
            for (int i = 0; i < receipts.Count; i++)
            {
                var r = receipts[i];
                var row = table.AddRow();

                row.Cells[0].AddParagraph((i + 1).ToString());
                row.Cells[1].AddParagraph(r.Order?.Title ?? "-");
                row.Cells[2].AddParagraph(r.Product?.Title ?? "-");
                row.Cells[3].AddParagraph(r.Product?.Code ?? "-");
                row.Cells[4].AddParagraph(
                    r.ProductReceiptParameters != null && r.ProductReceiptParameters.Any()
                        ? string.Join(" | ", r.ProductReceiptParameters.Select(p => p.ProductParameter?.Title ?? "-"))
                        : "-"); row.Cells[5].AddParagraph(r.Quantity.ToString());
                row.Cells[6].AddParagraph(r.CreatedAt.ToString("yyyy/MM/dd"));
            }

            // تبدیل به PDF
            var pdfRenderer = new MigraDocCore.Rendering.PdfDocumentRenderer(true);
            pdfRenderer.Document = document;
            pdfRenderer.RenderDocument();

            using var memoryStream = new MemoryStream();
            pdfRenderer.PdfDocument.Save(memoryStream);
            var bytes = memoryStream.ToArray();

            return File(bytes, "application/pdf", $"گزارش_تولید_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
        }

    }
}