using HPSpells.DomainLayer.DTOs;
using HPSpells.DomainLayer.Services.BusinessLayer;
using Microsoft.AspNetCore.Mvc;

namespace HPSpells.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SpellController : ControllerBase
    {
        private readonly ISpellService _spellService;

        public SpellController(ISpellService spellService)
        {
            _spellService = spellService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetSpells()
        {
            try
            {
                var spells =  _spellService.GetAllSpells();
                return Ok(spells);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Upsert([FromBody] SpellRequest request)
        {
            try
            {
                await _spellService.UpsertAsync(request);
                return Ok();
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
