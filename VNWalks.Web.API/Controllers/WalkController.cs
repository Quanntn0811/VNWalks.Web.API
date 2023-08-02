using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VNWalks.Application;
using VNWalks.Infrastructure.Repositories;
using VNWalks.Shared.EntityModels.SqlServer.DTO;
using VNWalks.Shared.EntityModels.SqlServer.EntityModels;
using VNWalks.Web.API.CustomeActionFilters;

namespace VNWalks.Web.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalkController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        // GET: /walk?filterOn=Name&filterQuery=Track
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, int pageNumber = 1)
        {
            var walks = await walkRepository.GetAllAsync(includeProperties: "Region,Difficulty", 
                filterOn: filterOn, filterQuery: filterQuery,
                sortBy: sortBy, isAscending: isAscending ?? true, pageNumber: pageNumber);    
            
            if (!walks.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<WalkDTO>>(walks));
        }

        [HttpGet]
        [Route("{id:Guid?}")]
        public async Task<IActionResult> GetById([FromRoute] Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walk = await walkRepository.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "Region,Difficulty");

            if (walk == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDTO>(walk));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] CreateWalkRequestDTO createWalkRequestDTO)
        {
            var walk = mapper.Map<Walk>(createWalkRequestDTO);
            await walkRepository.CreateAsync(walk);

            return Ok(mapper.Map<WalkDTO>(walk));

        }

        [HttpPut]
        [Route("api/{id:Guid?}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDTO updateWalkRequestDTO)
        {

            try
            {
                if (id == null) return NotFound();

                await walkRepository.UpdateAsync(id, updateWalkRequestDTO);

                return Ok();

            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{id:Guid?}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await walkRepository.DeleteAsync(id);

                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
