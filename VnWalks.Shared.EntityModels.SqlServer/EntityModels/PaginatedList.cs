
namespace VNWalks.Shared.EntityModels.SqlServer.EntityModels;

public class PaginatedList<T> : List<T> where T : class
{
    public int PageIndex { get; set; }
    public int TotalPage { get; set; }

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPage = (int) Math.Ceiling(count / (double) pageSize);
        AddRange(items);
    }

    public static PaginatedList<T> Create(IQueryable<T> query, int pageIndex, int pageSize)
    {
        var count = query.Count();
        var items = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
}
