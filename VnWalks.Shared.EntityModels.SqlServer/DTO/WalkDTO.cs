using VNWalks.Shared.EntityModels.SqlServer.EntityModels;

namespace VNWalks.Shared.EntityModels.SqlServer.DTO;

public class WalkDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Length { get; set; }
    public string? WalkImageUrl { get; set; }
    public DifficultyDTO Difficulty { get; set; }
    public RegionDTO Region { get; set; }
}
