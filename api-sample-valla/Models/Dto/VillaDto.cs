using System.ComponentModel.DataAnnotations;

namespace api_sample_valla.Models.Dto;

public class VillaDto
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string Name { get; set; }
}