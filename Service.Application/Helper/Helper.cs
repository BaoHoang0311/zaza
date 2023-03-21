using OfficeOpenXml.Attributes;
using OfficeOpenXml.Table;
using OfficeOpenXml;
using System.Reflection;
using X.PagedList;

namespace Service.Application.Helper
{
    public static class Helper
    {
        public static IPagedList<T> PageResultAsync<T>(this IQueryable<T> query, int page, int pageSize)
        {
            var result = query.ToPagedList(page, pageSize);
            return result;
        }
        public static IPagedList<T> ExcelAsync<T>(this IQueryable<T> query)
        {
            var result = query.ToPagedList();
            return result;
        }
        public static byte[] ExporttoExcel<T>(this List<T> table,string filename)
        {
            using ExcelPackage pack = new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(filename);

            //To get all members without having EpplusIgnore attribute added
            MemberInfo[] membersToInclude = typeof(T)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => !Attribute.IsDefined(p, typeof(EpplusIgnore)))
            .ToArray();

            ws.Cells["A1"].LoadFromCollection(table, true, TableStyles.Light1, BindingFlags.Default, membersToInclude);
            return pack.GetAsByteArray();
        }
    }
}
