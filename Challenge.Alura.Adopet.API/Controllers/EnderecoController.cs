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
    public class EnderecosController : ControllerBase
    {
        private readonly AdoPetContext _context;
        private readonly IEnderecoService _enderecoService;

        public EnderecosController(AdoPetContext context, IEnderecoService abrigoService)
        {
            _context = context;
            _enderecoService = abrigoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnderecoDTO>>> GetEnderecos()
        {

            return await _enderecoService.BuscaTodosAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EnderecoDTO>> GetEndereco(int id)
        {

            return await _enderecoService.BuscaPorIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Endereco>> PostEndereco(EnderecoDTO abrigo)
        {
            try
            {
                await _enderecoService.CriarAsync(abrigo);

                return this.CreatedAtAction("GetEndereco", new { id = abrigo.Id }, abrigo);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EnderecoDTO>> DeleteEndereco(int id)
        {
            var _endereco = _enderecoService.BuscaPorIdAsync(id);
            if (_endereco is null)
            {
                return this.NotFound("Endereco não encontrado na base de dados.");
            }


            try
            {
                var result = await _enderecoService.DeletaAsync(_endereco.Result);

            }
            catch (Exception ex) { return BadRequest(ex.Message); }
            return Ok(_endereco);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EnderecoDTO>> PutEndereco(int id, EnderecoDTO abrigo)
        {
            var _endereco = await _enderecoService.BuscaPorIdAsync(id);
            if (_endereco is null)
            {
                return this.NotFound("Endereco não encontrado na base de dados para atualização.");
            }

            try
            {
                //Coloco o objeto recuperado em modo de `modificação`.
                await _enderecoService.AlteraAsync(abrigo);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok(_endereco);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Endereco>> PatchEndereco(int id, JsonPatchDocument<Endereco> patch)
        {
            var _endereco = await this._context.Enderecos.FirstOrDefaultAsync(a => a.Id == id);
            if (_endereco is null)
            {
                return this.NotFound("Endereco não encontrado na base de dados para atualização.");
            }

            patch.ApplyTo(_endereco, ModelState);
            if (!TryValidateModel(_endereco))
            {
                return ValidationProblem(ModelState);
            }
            _context.SaveChanges();
            return Ok(_endereco);

        }
    }
}
