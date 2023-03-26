using ClosedXML.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Attributes;
using Service.Application.GraphDataFeature.Queries;
using System.Data;
using System.Reflection;

namespace ngay8.Helper
{
    public static class Helper
    {
        public static byte[] ExporttoExcel<T>(this List<T> table, string filename)
        {
            using ExcelPackage pack = new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(filename);

            //To get all members without having EpplusIgnore attribute added
            MemberInfo[] membersToInclude = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => !Attribute.IsDefined(p, typeof(EpplusIgnore)))
            .ToArray();

            int id = 1;
            var p = typeof(T).GetProperties().ToArray();
            foreach (var c in p)
            {
                if (c.PropertyType == typeof(DateTime?))
                {
                    ws.Column(id).Style.Numberformat.Format = "dd/mm/yyyy hh:mm:ss tt";
                }
                id++;
            }

            //ws.Column(7).Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
            ws.Cells["A1"].LoadFromCollection(table, true, OfficeOpenXml.Table.TableStyles.Light1, BindingFlags.Default, membersToInclude);
            return pack.GetAsByteArray();
        }
        public static byte[] ExporttoExcelCloseXML<T>(this List<T> table, GetExcelwithDate date)
        {
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Collections");
            string header = $"Top 10 Project by keyIn From: {date.From} To: {date.To}";


            ws.Cell(1, 1).Value = header;
            ws.Range(1, 1, 1, 2).Merge().AddToNamed("Titles");
            ws.Cell(2, 1).InsertTable(table);

            // Prepare the style for the titles
            var titlesStyle = wb.Style;
            titlesStyle.Font.Bold = true;
            titlesStyle.Font.FontSize = 30;
            titlesStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // Set format
            ws.Column(2).Style.NumberFormat.Format = "#,##0";

            // Format all titles in one shot
            wb.NamedRanges.NamedRange("Titles").Ranges.Style = titlesStyle;

            ws.Column(1).AdjustToContents();
            using MemoryStream stream = new MemoryStream();
            wb.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
