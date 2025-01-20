namespace Cwiczenie4_KamilWolak.Domain.Dtos;

public class PaginationFilterDto
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SearchPhrase { get; set; }
    public PaginationFilterDto()
    {

    }

    public PaginationFilterDto(int pageNumber, int pageSize, string? searchPhrase)
    {
        this.PageNumber = pageNumber < 0 ? 0 : pageNumber;
        this.PageSize = pageSize < 1 ? 1 : pageSize;
        this.SearchPhrase = searchPhrase;

    }
}