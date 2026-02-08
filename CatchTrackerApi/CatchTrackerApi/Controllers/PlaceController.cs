using CatchTrackerApi.DTOs.PlaceDTOs;
using CatchTrackerApi.Interfaces.ServiceInterfaces;
using CatchTrackerApi.Mappers;
using CatchTrackerApi.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CatchTrackerApi.Controllers
{
    [Route("api/Places")]

    [ApiController]
    //[Authorize]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceService _placeService;

        public PlaceController(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetAll([FromQuery]QueryForPlace query)
        {
            try
            {
                var places = await _placeService.GetAllAsync(query);
                var dtoPlaces = places.Select(p => p.ToPlaceDTO()).ToList();
                return Ok(dtoPlaces);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var place = await _placeService.GetByIdAsync(id);
                return Ok(place.ToPlaceDTO());
            }
            catch (KeyNotFoundException ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreatePlaceDTO createPlaceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var place = createPlaceDto.ToPlaceFromCreateDTO();
                var createdPlace = await _placeService.CreateAsync(place);
                return CreatedAtAction(nameof(GetById), new { id = createdPlace.Id }, createdPlace.ToPlaceDTO());
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] UpdatePlaceDTO updatePlaceDto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var place = updatePlaceDto.ToPlaceFromUpdateDTO();
                var updatedPlace = await _placeService.UpdateAsync(place, id);
                if (updatedPlace == null)
                {
                    throw new KeyNotFoundException($"Place with ID {id} not found.");
                }
                return Ok(updatedPlace.ToPlaceDTO());
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var deletedPlace = await _placeService.DeleteAsync(id);
                if (deletedPlace == null)
                {
                    throw new KeyNotFoundException($"Place with ID {id} not found.");
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
