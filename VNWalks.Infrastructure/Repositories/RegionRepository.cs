using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VNWalks.Application;
using VNWalks.Infrastructure.Data;
using VNWalks.Shared.EntityModels.SqlServer.DTO;
using VNWalks.Shared.EntityModels.SqlServer.EntityModels;

namespace VNWalks.Infrastructure.Repositories;

public class RegionRepository : IRegionRepository
{
    private readonly VNWalksDbContext _context;

    public RegionRepository(VNWalksDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Region region)
    {
        await _context.Regions.AddAsync(region);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var regionFromDb = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);

        if (regionFromDb != null)
        {
            _context.Regions.Remove(regionFromDb);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Region does not exist.");
        }
    }

    public async Task<List<Region>> GetAllAsync()
        => await _context.Regions.ToListAsync();

    public async Task<Region?> GetFirstOrDefaultAsync(Expression<Func<Region, bool>> filter)
    {
        IQueryable<Region> query = _context.Regions;
        query = query.Where(filter);     

        return query.FirstOrDefault();
    }

    public async Task UpdateAsync(Guid id, UpdateRegionRequestDTO region)
    {
        var regionFromDb = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);

        if (regionFromDb != null)
        {
            regionFromDb.Code = region.Code;
            regionFromDb.Name = region.Name;
            regionFromDb.RegionImageUrl = region.RegionImageUrl;
            
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Region does not exist.");
        }
    }
}
