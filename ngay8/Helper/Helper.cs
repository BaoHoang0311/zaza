using ClosedXML.Excel;
using Microsoft.AspNetCore.WebUtilities;
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
            var ws = wb.Worksheets.Add("Top 10 Project");
            string header = $"Top 10 Project by keyin From: {date.From?.ToString("dd-MMM-yyyy")} To: {date.To?.ToString("dd-MMM-yyyy")}";

            ws.Cell(1, 1).Value = header;
            ws.Range(1, 1, 1, 3).Merge().AddToNamed("Titles");
            using MemoryStream stream = new MemoryStream();
            if (table != null)
            {

                ws.Cell(2, 1).InsertTable(table);
                // Set format
                ws.Column(3).Style.NumberFormat.Format = "$#,##0.00";

                // Prepare the style for the titles
                var titlesStyle = wb.Style;
                titlesStyle.Font.Bold = true;
                titlesStyle.Font.FontSize = 40;
                titlesStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                // Format all titles in one shot
                wb.NamedRanges.NamedRange("Titles").Ranges.Style = titlesStyle;
                ws.Range(2, 1, 2, 3).AddToNamed("headers");
                var headerStyle = wb.Style;
                headerStyle.Fill.BackgroundColor = XLColor.LightCyan;
                headerStyle.Font.Bold = true;

                ws.Column(1).AdjustToContents();
                wb.SaveAs(stream);
            }
            else
            {
                wb.SaveAs(stream);
            }
            return stream.ToArray();
        }
    }
    //public static class PageHelper
    //{
    //public static PagedResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, PaginationFilter validFilter, int totalRecords, IUriService uriService, string route)
    //{
    //    var respose = new PagedResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
    //    var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
    //    int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
    //    respose.NextPage =
    //        validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
    //        ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize), route)
    //        : null;
    //    respose.PreviousPage =
    //        validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
    //        ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize), route)
    //        : null;
    //    respose.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize), route);
    //    respose.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize), route);
    //    respose.TotalPages = roundedTotalPages;
    //    respose.TotalRecords = totalRecords;
    //    return respose;
    //}
    //}
    public class Response<T>
    {
        public Response(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
        public PagedResponse(T data, int pageNumber, int pageSize) : base(data)
        {
            this.PageNumber =
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
        public static async Task<PagedResponse<List<T>>> CreatePagedReponse(List<T> pagedData, IUriService uriService, string route, PaginationFilter validFilter = null)
        {
            var TotalRecords = pagedData.Count();
            var TotalPages = (int)((double)TotalRecords / (double)validFilter.PageSize);
            var respose = new PagedResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);

            validFilter = new PaginationFilter(validFilter.PageNumber, validFilter.PageSize);

            validFilter.PageNumber = validFilter.PageNumber > TotalPages ? TotalPages : (validFilter.PageNumber < 1) ? 1 : validFilter.PageNumber;

            respose.PageNumber = validFilter.PageNumber;
            respose.TotalRecords = TotalRecords;
            respose.TotalPages = TotalPages;

            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < respose.TotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize), route)
                : null;

            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= respose.TotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize), route)
                : null;

            respose.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize), route);
            respose.LastPage = uriService.GetPageUri(new PaginationFilter(respose.TotalPages, validFilter.PageSize), route);

            respose.Data = pagedData.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                            .Take(validFilter.PageSize).ToList();

            return respose;
        }
    }
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize >= 20 ? 20 : pageSize;
        }
        public PaginationFilter()
        {

        }
    }
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPageUri(PaginationFilter filter, string route)
        {
            // localhost:44312/api/customer
            var _enpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            // localhost:44312/api/customer?pageNumber=1&pageSize=10
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}
