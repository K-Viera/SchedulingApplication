using Microsoft.AspNetCore.Mvc;
using ShiftScheduling.Database;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Authorization;

namespace ShiftScheduleWebApi.Controllers;

[ApiController]
[Route("[controller]")]
//admin autorization
[Authorize(Roles = "Admin")]
public class ShiftTypesController : ControllerBase
{
    private readonly IShiftTypeRepository _shiftTypeService;

    public ShiftTypesController(IShiftTypeRepository shiftTypeService)
    {
        _shiftTypeService = shiftTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShiftTypeResponse>>> GetAll()
    {
        var shiftTypes = await _shiftTypeService.GetAll();
        return Ok(shiftTypes);
    }

    //[HttpGet("{id}")]
    //public async Task<ActionResult<ShiftTypeResponse>> GetShiftType(int id)
    //{
    //    var shiftType = await _shiftTypeService.GetShiftType(id);
    //    if (shiftType == null)
    //    {
    //        return NotFound();
    //    }
    //    return Ok(shiftType);
    //}

    [HttpPost]
    public async Task<ActionResult<ShiftType>> Create(ShiftType shiftType)
    {
        int shiftTypeId = await _shiftTypeService.AddShiftType(shiftType);
        return CreatedAtAction(nameof(Create), new { id = shiftTypeId }, shiftType);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, ShiftType shiftType)
    {
        if (!await _shiftTypeService.UpdateShiftType(id, shiftType))
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (!await _shiftTypeService.DeleteById(id))
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (ReferenceConstraintException ex)
        {
            return BadRequest("You cannot delete a shift type that is in use.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
