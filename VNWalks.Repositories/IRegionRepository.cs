using System.Linq.Expressions;
using VNWalks.Shared.EntityModels.SqlServer.DTO;
using VNWalks.Shared.EntityModels.SqlServer.EntityModels;

namespace VNWalks.Application;

public interface IRegionRepository
{
    Task<List<Region>> GetAllAsync();
    Task<Region> GetFirstOrDefaultAsync(Expression<Func<Region, bool>> filter);
    Task CreateAsync(Region region);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(Guid id, UpdateRegionRequestDTO region);

}
