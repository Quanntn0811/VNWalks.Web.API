using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using VNWalks.Application;
using VNWalks.Infrastructure.Data;
using VNWalks.Shared.EntityModels.SqlServer.DTO;
using VNWalks.Shared.EntityModels.SqlServer.EntityModels;
using VNWalks.Web.API.CustomeActionFilters;

namespace VNWalks.Web.API.Controllers
{
    // https//localhost:portnumber/region
    [Route("[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionController> logger;

        public RegionController(IRegionRepository regionRepository, IMapper mapper,
            ILogger<RegionController> logger)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET ALL REGIONS
        // https//localhost:portnumber/region
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("Get all regions Action method was invoked");

            // Get data from database - Domain models
            var regions = await regionRepository.GetAllAsync();

            logger.LogInformation($"Finished get all regions request with data: {JsonSerializer.Serialize(regions)}");

            // Map Domain Models to DTOs and return DTO
            return Ok(mapper.Map<List<RegionDTO>>(regions));
        }

        // GET SINGLE REGION
        [HttpGet]
        [Route("{id:Guid?}")]
        public async Task<IActionResult> GetById([FromRoute] Guid? id)
        {

            if (id == null)
            {
                return RedirectToAction("GetAll");
            }

            //var region = _context.Regions.Find(id);
            var region = await regionRepository.GetFirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
            {
                return RedirectToAction("GetAll");
                // return NotFound();
            }

            //return DTO back to client
            return Ok(mapper.Map<RegionDTO>(region));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] CreateRegionRequestDTO createRegionRequestDTO)
        {
            var region = mapper.Map<Region>(createRegionRequestDTO);

            await regionRepository.CreateAsync(region);

            return CreatedAtAction(nameof(GetById), new { id = region.Id }, mapper.Map<RegionDTO>(region));
        }

        [HttpPut]
        [Route("{id:Guid?}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequest)
        {
            try
            {
                await regionRepository.UpdateAsync(id, updateRegionRequest);

                //return RedirectToAction(nameof(GetAll));
                return Ok();
            }
            catch
            {
                //return RedirectToAction(nameof(GetAll));
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{id:Guid?}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await regionRepository.DeleteAsync(id);

                //return RedirectToAction(nameof(GetAll));
                return Ok();
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(GetAll));
            }
        }
    }
}
