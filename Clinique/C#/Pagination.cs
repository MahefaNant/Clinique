namespace AspNetCoreTemplate.C_;

public class Pagination<T>
{
    
    public int SizeList { get; set; }
    public int PageId { get; set; }
    public int TotalItemsCount { get; set; }
    public int TotalPages { get; set; }

    public readonly IQueryable<T> Query;

    public Pagination(int sizeList, int? pagId, IQueryable<T> query)
    {
        SizeList = sizeList;
        PageId = pagId ?? 1;
        Query = query;
        TotalItemsCount = Query.Count();
        TotalPages = (int)Math.Ceiling((double)TotalItemsCount / SizeList);
    }

    public IQueryable<T> Paginate()
    {
        var res = Query
            .Skip((PageId - 1) * SizeList)
            .Take(SizeList);
        return res;
    }
}