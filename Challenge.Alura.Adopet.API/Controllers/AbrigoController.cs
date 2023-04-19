using Microsoft.AspNetCore.Mvc;
using Challenge.Alura.Adopet.API.Data;
using Challenge.Alura.Adopet.API.Dominio;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.JsonPatch;
using Challenge.Alura.Adopet.API.Service.Interface;
using Challenge.Alura.Adopet.API.Service;
using Challenge.Alura.Adopet.API.DTO;

namespace Challenge.Alura.Adopet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbrigosController : ControllerBase
    {
        private readonly AdoPetContext _context;
        private readonly IAbrigoService _abrigoService;

        public AbrigosController(AdoPetContext context, IAbrigoService abrigoService)
        {
            _context = context;
            _abrigoService = abrigoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AbrigoDTO>>> GetAbrigos()
        {

            return await _abrigoService.BuscaTodosAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AbrigoDTO>> GetAbrigo(int id)
        {

            return await _abrigoService.BuscaPorIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<AbrigoDTO>> PostAbrigo(AbrigoDTO abrigo)
        {
            try
            {
                await _abrigoService.CriarAsync(abrigo);

                return this.CreatedAtAction("GetAbrigo", new { id = abrigo.Id }, abrigo);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AbrigoDTO>> DeleteAbrigo(int id)
        {
            var _abrigo = _abrigoService.BuscaPorIdAsync(id);
            if (_abrigo is null)
            {
                return this.NotFound("Abrigo não encontrado na base de dados.");
            }


            try
            {
                var result = await _abrigoService.DeletaAsync(_abrigo.Result);

            }
            catch (Exception ex) { return BadRequest(ex.Message); }
            return Ok(_abrigo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AbrigoDTO>> PutAbrigo(int id, AbrigoDTO abrigo)
        {
            var _abrigo = await _abrigoService.BuscaPorIdAsync(id);
            if (_abrigo is null)
            {
                return this.NotFound("Abrigo não encontrado na base de dados para atualização.");
            }

            try
            {
                //Coloco o objeto recuperado em modo de `modificação`.
                await _abrigoService.AlteraAsync(abrigo);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok(_abrigo);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Abrigo>> PatchAbrigo(int id, JsonPatchDocument<Abrigo> patch)
        {
            var _abrigo = await this._context.Abrigos.FirstOrDefaultAsync(a => a.Id == id);
            if (_abrigo is null)
            {
                return this.NotFound("Abrigo não encontrado na base de dados para atualização.");
            }

            patch.ApplyTo(_abrigo, ModelState);
            if (!TryValidateModel(_abrigo))
            {
                return ValidationProblem(ModelState);
            }
            _context.SaveChanges();
            return Ok(_abrigo);

        }
    }
}
