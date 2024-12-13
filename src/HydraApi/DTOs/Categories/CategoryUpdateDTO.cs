using System;
using System.ComponentModel.DataAnnotations;

namespace HydraApi.DTOs.Categories;

public class CategoryUpdateDTO
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Level { get; set; }
    [Required]
    public string Description { get; set; }
}
