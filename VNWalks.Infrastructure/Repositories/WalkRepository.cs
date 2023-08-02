using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VNWalks.Application;
using VNWalks.Infrastructure.Data;
using VNWalks.Shared.EntityModels.SqlServer.DTO;
using VNWalks.Shared.EntityModels.SqlServer.EntityModels;

namespace VNWalks.Infrastructure.Repositories;

public class WalkRepository : IWalkRepository
{
    private readonly VNWalksDbContext _context;
    public static int PAGE_SIZE { get; set; } = 5;

    public WalkRepository(VNWalksDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Walk walk)
    {
        await _context.Walks.AddAsync(walk);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var walkFromDb = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);

        if (walkFromDb != null)
        {
            _context.Walks.Remove(walkFromDb);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Walk does not exist.");
        }
    }

    public async Task<List<Walk>> GetAllAsync(Expression<Func<Walk, bool>>? filter = null, string? includeProperties = null,
        string? filterOn = null, string? filterQuery = null,
        string? sortBy = null, bool isAscending = true, int pageNumber = 1)
    {
        IQueryable<Walk> query = _context.Walks;

        // Filter by properties
        if (filter != null)
        {
            query = query.Where(filter);
        }

        // Including
        if (includeProperties != null)
        {
            foreach (var property in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property).AsQueryable();
            }
        }

        // Filtering
        if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
        {
            if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(x => x.Name.Contains(filterQuery));
            }
        }

        // Sorting
        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                query = isAscending ? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name);
            }
            else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
            {
                query = isAscending ? query.OrderBy(x => x.Length) : query.OrderByDescending(x => x.Length);
            }
        }

        // Pagination
        //int pageNumber = _context.Walks.Count() / 5;
        //var skipResults = (pageNumber - 1) * PAGE_SIZE;
        // return await query.Skip(skipResults).Take(PAGE_SIZE).ToListAsync();

        var result = PaginatedList<Walk>.Create(query, pageNumber, PAGE_SIZE);

        return result.ToList();
    }
        

    public async Task<Walk?> GetFirstOrDefaultAsync(Expression<Func<Walk, bool>> filter, string? includeProperties = null)
    {
        IQueryable<Walk> query = _context.Walks;
        query = query.Where(filter);

        if (includeProperties != null)
        {
            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }

        return query.FirstOrDefault();
    }

    public async Task UpdateAsync(Guid id, UpdateWalkRequestDTO region)
    {
        var walkFromDb = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);

        if (walkFromDb != null)
        {
            walkFromDb.Description = region.Description;
            walkFromDb.Name = region.Name;
            walkFromDb.Length = region.Length;
            walkFromDb.RegionId = region.RegionId;
            walkFromDb.DifficultyId = region.DifficultyId;
            walkFromDb.WalkImageUrl = region.WalkImageUrl;

            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Walk does not exist.");
        }
    }
}
