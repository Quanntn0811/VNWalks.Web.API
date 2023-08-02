using System.ComponentModel.DataAnnotations;

namespace VNWalks.Shared.EntityModels.SqlServer.DTO;

public class CreateRegionRequestDTO
{
    [Required]
    [MinLength(2)]
    public string Code { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")]
    public string Name { get; set; }
    public string? RegionImageUrl { get; set; }
}
