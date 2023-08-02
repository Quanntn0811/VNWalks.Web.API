using System.Linq.Expressions;
using VNWalks.Shared.EntityModels.SqlServer.DTO;
using VNWalks.Shared.EntityModels.SqlServer.EntityModels;

namespace VNWalks.Application;

public interface IWalkRepository
{
    Task<List<Walk>> GetAllAsync(Expression<Func<Walk, bool>>? filter = null, string? includeProperties = null,
        string? filterOn = null, string? filterQuery = null,
        string? sortBy = null, bool isAscending = true, int pageNumber = 1);
    Task<Walk> GetFirstOrDefaultAsync(Expression<Func<Walk, bool>> filter, string? includeProperties = null);
    Task CreateAsync(Walk walk);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(Guid id, UpdateWalkRequestDTO region);

}
