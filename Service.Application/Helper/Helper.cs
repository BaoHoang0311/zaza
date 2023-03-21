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
        public static IPagedList<T> GetAll<T>(this IQueryable<T> query, int page, int pageSize)
        {
            var result = query.ToPagedList();
            return result;
        }

    }
}
