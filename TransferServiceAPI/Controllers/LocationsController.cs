using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransferServiceAPI.Models;
using TransferServiceAPI.Services;

namespace TransferServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly ILocationCategoryService _locationCategoryService;

        public LocationsController(
            ILocationService locationService,
            ILocationCategoryService locationCategoryService)
        {
            _locationService = locationService;
            _locationCategoryService = locationCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var locations = await _locationService.GetAllAsync();
            return Ok(locations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var location = await _locationService.GetByIdAsync(id);
            if (location == null) return NotFound();
            return Ok(location);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _locationCategoryService.GetByIdAsync(location.LocationCategoryId);
            if (category == null)
            {
                return BadRequest("The specified LocationCategoryId does not exist.");
            }

            var createdLocation = await _locationService.CreateAsync(location);
            return CreatedAtAction(nameof(GetById), new { id = createdLocation.Id }, createdLocation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Location location)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != location.Id) return BadRequest("ID mismatch");

            var category = await _locationCategoryService.GetByIdAsync(location.LocationCategoryId);
            if (category == null)
            {
                return BadRequest("The specified LocationCategoryId does not exist.");
            }

            var updatedLocation = await _locationService.UpdateAsync(id, location);
            if (updatedLocation == null) return NotFound();
            return Ok(updatedLocation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _locationService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}