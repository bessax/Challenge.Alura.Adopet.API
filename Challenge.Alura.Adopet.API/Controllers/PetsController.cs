using Challenge.Alura.Adopet.API.Data;
using Challenge.Alura.Adopet.API.Dominio;
using Challenge.Alura.Adopet.API.DTO;
using Challenge.Alura.Adopet.API.Service.Interface;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Challenge.Alura.Adopet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly AdoPetContext _context;
        private readonly IPetService _petService;

        public PetsController(AdoPetContext context, IPetService tutorService)
        {
            _context = context;
            _petService = tutorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetDTO>>> GetPets()
        {

            return await _petService.BuscaTodosAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PetDTO>> GetPet(int id)
        {

            return await _petService.BuscaPorIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<PetDTO>> PostPet(PetDTO pet)
        {
            try
            {
                await _petService.CriarAsync(pet);

                return this.CreatedAtAction("GetPet", new { id = pet.Id }, pet);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PetDTO>> DeletePet(int id)
        {
            var _pet = _petService.BuscaPorIdAsync(id);
            if (_pet is null)
            {
                return this.NotFound("Pet não encontrado na base de dados.");
            }

            try
            {
                var result = await _petService.DeletaAsync(_pet.Result);

            }
            catch (Exception ex) { return BadRequest(ex.Message); }
            return Ok(_pet);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PetDTO>> PutPet(int id, PetDTO pet)
        {
            var _pet = await _petService.BuscaPorIdAsync(id);
            if (_pet is null)
            {
                return this.NotFound("Pet não encontrado na base de dados para atualização.");
            }

            try
            {
                //Coloco o objeto recuperado em modo de `modificação`.
                await _petService.AlteraAsync(pet);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok(_pet);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Pet>> PatchPet(int id, JsonPatchDocument<Pet> patch)
        {
            var _pet = await this._context.Pets.FirstOrDefaultAsync(a => a.Id == id);
            if (_pet is null)
            {
                return this.NotFound("Pet não encontrado na base de dados para atualização.");
            }

            patch.ApplyTo(_pet, ModelState);
            if (!TryValidateModel(_pet))
            {
                return ValidationProblem(ModelState);
            }
            _context.SaveChanges();
            return Ok(_pet);

        }
    }
}
