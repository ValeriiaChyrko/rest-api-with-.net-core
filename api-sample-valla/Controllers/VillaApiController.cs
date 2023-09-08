using api_sample_valla.Data;
using api_sample_valla.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace api_sample_valla.Controllers;

[Route("api/VillaApi")]
[ApiController]
public class VillaApiController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ICollection<VillaDto>> GetVillas()
    {
        return Ok(VillaStore.villasList);
    }

    [HttpGet("{id:int}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<VillaDto> GetVilla(int id)
    {
        if (id < 0)
            return StatusCode(StatusCodes.Status400BadRequest, "Invalid object identification.");

        VillaDto? searchVilla = VillaStore.villasList.FirstOrDefault(v => v.Id == id);
        if (searchVilla == null)
            return StatusCode(StatusCodes.Status404NotFound, "There is not such villa in DataBase.");

        return StatusCode(StatusCodes.Status200OK, searchVilla);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto? villaDto)
    {
        if (villaDto == null)
            return StatusCode(StatusCodes.Status400BadRequest, villaDto);

        if (villaDto.Id < 0)
            return StatusCode(StatusCodes.Status500InternalServerError, "Invalid object identification.");

        if (VillaStore.villasList.FirstOrDefault(v=>v.Id==villaDto.Id) != null && villaDto.Id != 0)
        {
            ModelState.AddModelError("IdValidationUniqueError", "Villa with such identification already exists.");
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        
        villaDto.Id = VillaStore.villasList.Last().Id + 1;
        VillaStore.villasList.Add(villaDto);
        
        return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
    }
    
    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteVilla(int id)
    {
        if (id < 0)
            return StatusCode(StatusCodes.Status400BadRequest, "Invalid object identification.");

        VillaDto? villaDto = VillaStore.villasList.FirstOrDefault(v => v.Id == id);
        if (villaDto == null)
            return StatusCode(StatusCodes.Status404NotFound, "There is not such villa in DataBase.");

        VillaStore.villasList.Remove(villaDto);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}