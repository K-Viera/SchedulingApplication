using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftScheduling.Database;

namespace ShiftScheduleWebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class ShiftsController : ControllerBase
{
    private readonly IShiftRepository _shiftService;

    public ShiftsController(IShiftRepository shiftService)
    {
        _shiftService = shiftService;
    }

    [HttpGet]
    [Authorize(Roles="Admin")]
    public async Task<ActionResult<IEnumerable<ShiftResponse>>> GetAll()
    {
        var shifts = await _shiftService.GetAll();
        return Ok(shifts);
    }

    [HttpPost]
    [Authorize(Roles="Admin")]
    public async Task<ActionResult<Shift>> Create(ShiftRequest shift)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            int shiftId = await _shiftService.AddShift(shift);
            return CreatedAtAction(nameof(Create), new { id = shiftId }, shift);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }

    }

    [HttpDelete("{id}")]
    [Authorize(Roles="Admin")]
    public async Task<ActionResult> Delete(int id)
    {
        if (!await _shiftService.DeleteShiftById(id))
        {
            return NotFound();
        }
        return NoContent();
    }
}
