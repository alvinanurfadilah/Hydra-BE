using System;

namespace HydraApi.DTOs.Categories;

public class CategoryResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Level { get; set; }
    public string Description { get; set; }
}
