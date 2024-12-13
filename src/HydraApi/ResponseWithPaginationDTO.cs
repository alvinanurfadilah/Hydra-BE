namespace HydraApi;

public class ResponseWithPaginationDTO<T>
{
    public string Message { get; set; } = null!;
    public string Status { get; set; } = null!;
    public T? Data { get; set; }
    public PaginationDTO? Pagination { get; set; }
}
