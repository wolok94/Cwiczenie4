namespace Ćwiczenie4_KamilWolak.Domain.Dtos;

public class PaginationDto<T> where T : class
{
    public List<T> Items { get; set; }
    public int TotalPages { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }
    public int TotalItemsCount { get; set; }

    public PaginationDto(List<T> items, int pageSize, int pageNumber, int totalCount)
    {
        Items = items;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        ItemsFrom = (pageNumber -1) * pageSize +1;
        ItemsTo = ItemsFrom + pageSize - 1;
        TotalItemsCount = totalCount;

    }
}
