namespace HydraApi;

public class PaginationDTO
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalData { get; set; }
    public int TotalPage
    {
        get  {
            return (int)Math.Ceiling((double)TotalData / PageSize);
        }
    }
}
