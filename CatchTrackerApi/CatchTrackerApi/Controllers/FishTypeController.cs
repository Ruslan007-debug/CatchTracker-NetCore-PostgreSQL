using CatchTrackerApi.Interfaces.ServiceInterfaces;
using CatchTrackerApi.Mappers;
using Microsoft.AspNetCore.Mvc;
using CatchTrackerApi.DTOs.FishTypeDTOs;

namespace CatchTrackerApi.Controllers
{
    [Route("api/FishTypes")]
    [ApiController]
    public class FishTypeController: ControllerBase
    {
        private readonly IFishTypeService _fishTypeService;

        public FishTypeController(IFishTypeService fishTypeService)
        {
            _fishTypeService = fishTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var types = await _fishTypeService.GetAllAsync();
                var dtoTypes = types.Select(t => t.ToFishTypeDTO()).ToList();
                return Ok(dtoTypes);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var type = await _fishTypeService.GetByIdAsync(id);
                return Ok(type.ToFishTypeDTO());
            }
            catch (KeyNotFoundException ex)
            {
                return Ok(new { message = ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFishTypeDTO createFishTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fishType = createFishTypeDto.ToFishTypeFromCreateDto();
            try
            {
                var createdType = await _fishTypeService.CreateAsync(fishType);
                return CreatedAtAction(nameof(GetById), new { id = createdType.Id }, createdType.ToFishTypeDTO());
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateFishTypeDTO updatedTypeDto, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fishType = updatedTypeDto.ToFishTypeFromUpdateDto();
            try 
            {
                var updatedType = await _fishTypeService.UpdateAsync(id, fishType);
                if (updatedType == null)
                {
                    throw new KeyNotFoundException($"FishType with Id: {id} is not found.");
                }
                return Ok(updatedType.ToFishTypeDTO());
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }

            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var deleted = await _fishTypeService.DeleteAsync(id);
                if (deleted == null)
                {
                    throw new KeyNotFoundException($"FishType with Id: {id} is not found.");
                }
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
