using Marvel.Application.InputModel;
using Microsoft.AspNetCore.Mvc;
using Marvel.Application;

namespace Marvel.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterApplication _characterService;

        public CharactersController(ICharacterApplication characterService) 
        {
            _characterService = characterService;
        }

        /// <summary>
        /// Buscar heróis da Marvel de acordo com os filtros escolhidos
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCharacters([FromQuery] FiltersCharactersInputModel filters)
        {
            var result = await _characterService.GetCharacter(filters);
            return Ok(result);
        }

        /// <summary>
        /// Salvar personagem como favorito
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> SetFavorite([FromRoute] int id)
        {
            try
            {
                await _characterService.SetFavorite(id);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

           
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFavorite([FromRoute] int id)
        {
            try
            {
                await _characterService.RemoveFavorite(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("Favorites")]
        public async Task<IActionResult> GetFavorites()
        {
            var result = await _characterService.GetFavorite();

            return Ok(result);
        }
    }
}
