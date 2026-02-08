using CatchTrackerApi.DTOs.FishingLogDTOs;
using CatchTrackerApi.Interfaces.ServiceInterfaces;
using CatchTrackerApi.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CatchTrackerApi.Controllers
{
    [ApiController]
    [Route("api/FishingLogs")]
    [Authorize]
    public class FishingLogController : BaseAuthController
    {
        private readonly IFishingLogService _fishingLogService;
        public FishingLogController(IFishingLogService fishingLogService)
        {
            _fishingLogService = fishingLogService;
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var fishingLogs = await _fishingLogService.GetAllFishingLogsAsync();
                var fishingLogsDto = fishingLogs.Select(f=>f.ToFishingLogDTO()).ToList();
                return Ok(fishingLogsDto);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = "Сталася внутрішня помилка сервера.", details = ex.Message }) { StatusCode = 500 };
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try 
            {
                var fishingLog = await _fishingLogService.GetFishingLogByIdAsync(id);
                if (fishingLog == null)
                {
                    return NotFound(new { message = $"Fishing log with ID {id} not found." });
                }
                return Ok(fishingLog.ToFishingLogDTO());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("userLogs")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetByUserId()
        {
            try
            {
                var fishingLogs = await _fishingLogService.GetFishingLogsByUserIdAsync(CurrentUserId);
                var fishingLogsDto = fishingLogs.Select(f => f.ToFishingLogDTO()).ToList();
                return Ok(fishingLogsDto);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = "Сталася внутрішня помилка сервера.", details = ex.Message }) { StatusCode = 500 };
            }
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([FromBody] CreateFishingLogDTO createDto)
        {
            try
            {
                var FishingLog = createDto.ToFishingLogFromCreateDTO(CurrentUserId);
                var createdFishingLog = await _fishingLogService.CreateFishingLogAsync(FishingLog);
                return CreatedAtAction(nameof(GetById), new { id = createdFishingLog.Id }, createdFishingLog.ToFishingLogDTO());
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = "Сталася внутрішня помилка сервера.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Update([FromBody] UpdateFishingLogDTO updatedDTO, [FromRoute] int id)
        {
            var existingLog = await _fishingLogService.GetFishingLogByIdAsync(id);
            ValidateOwnership(existingLog.UserId);

            var updatedFishingLog = updatedDTO.ToFishingLogFromUpdateDTO();
            
            try
            {
                var fishingLog = await _fishingLogService.UpdateFishingLogAsync(updatedFishingLog, id);
                return Ok(fishingLog.ToFishingLogDTO());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Внутрішня помилка", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var existingLog = await _fishingLogService.GetFishingLogByIdAsync(id);
                ValidateOwnership(existingLog.UserId);
                await _fishingLogService.DeleteFishingLogAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

    }
}
