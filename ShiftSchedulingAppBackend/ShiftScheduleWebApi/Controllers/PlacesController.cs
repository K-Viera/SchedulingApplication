﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShiftScheduling.Database;

namespace ShiftScheduleWebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class PlacesController : ControllerBase
{
    private readonly IPlaceRepository _placeService;

    public PlacesController(IPlaceRepository placeService)
    {
        _placeService = placeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlaceResponse>>> GetAll()
    {
        var places = await _placeService.GetAll();
        if (places == null)
        {
            return NotFound();
        }
        return Ok(places);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Place>> GetPlace(int id)
    {
        var place = await _placeService.GetPlace(id);
        if (place == null)
        {
            return NotFound();
        }
        return Ok(place);
    }

    [HttpPost]
    public async Task<ActionResult<Place>> Create(Place place)
    {
        int placeId = await _placeService.AddPlace(place);
        return CreatedAtAction(nameof(Create), new { id = placeId }, place);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, Place place)
    {
        if (!await _placeService.UpdatePlace(id, place))
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("name/{name}")]
    public async Task<ActionResult> Delete(string name)
    {
        if (!await _placeService.DeletePlace(name))
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
            if (!await _placeService.DeleteById(id))
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (ReferenceConstraintException ex)
        {
            return BadRequest("You cannot delete a place that is in use.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
