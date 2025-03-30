using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransferServiceAPI.Models;
using TransferServiceAPI.Services;

namespace TransferServiceAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LocationCategoriesController : ControllerBase
    {
        private readonly ILocationCategoryService _locationCategoryService;

        public LocationCategoriesController(ILocationCategoryService locationCategoryService)
        {
            _locationCategoryService = locationCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _locationCategoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _locationCategoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LocationCategory locationCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            locationCategory.Id = 0;  // Ensure ID is not explicitly set
            var createdCategory = await _locationCategoryService.CreateAsync(locationCategory);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LocationCategory locationCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != locationCategory.Id)
            {
                return BadRequest("ID in URL must match ID in body");
            }

            var updatedCategory = await _locationCategoryService.UpdateAsync(id, locationCategory);
            if (updatedCategory == null)
            {
                return NotFound();
            }

            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _locationCategoryService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }



}