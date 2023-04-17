using Microsoft.AspNetCore.Mvc;
using Challenge.Alura.Adopet.API.Data;
using Challenge.Alura.Adopet.API.Dominio;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.JsonPatch;

namespace Challenge.Alura.Adopet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly AdoPetContext _context;

        public PetsController(AdoPetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetPets()
        {
            var pets = await this._context.Pets.Include(x=>x.Tutor).ToArrayAsync();

            if (pets == null)
            {
                return this.NotFound();
            }

            return pets;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetPet(int id)
        {
            var _pet = await this._context.Pets.FirstAsync(a => a.Id == id);

            if (_pet == null)
            {
                return this.NotFound();
            }

            return _pet;
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> PostPet(Pet Pet)
        {
            try
            {
                await this._context.Pets.AddAsync(Pet);
                await this._context.SaveChangesAsync();

                return this.CreatedAtAction("GetPet", new { id = Pet.Id }, Pet);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Pet>> DeletePet(int id)
        {
            var _pet = await this._context.Pets.FirstOrDefaultAsync(a => a.Id == id);
            if (_pet is null)
            {
                return this.NotFound("Pet não encontrado na base de dados.");
            }
            try
            {
                this._context.Pets.Remove(_pet);
                await this._context.SaveChangesAsync();

            }
            catch (Exception ex) { return BadRequest(ex.Message); }
            return Ok(_pet);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Pet>> PutPet(int id, Pet pet)
        {
            var _pet = await this._context.Pets.FirstOrDefaultAsync(a => a.Id == id);
            if (_pet is null)
            {
                return this.NotFound("Pet não encontrado na base de dados para atualização.");
            }

            try
            {
                //Coloco o objeto recuperado em modo de `modificação`.
                _context.Entry(_pet).State = EntityState.Modified;

                // Atualizo os campos do objeto com as novas informações.
                _pet.Nome = pet.Nome;
                _pet.Descricao = pet.Descricao;
                _pet.Porte = pet.Porte;
                _pet.TutorId = pet.TutorId;
                _pet.Tutor=pet.Tutor;


                //Atualizo o objeto no contexto.
                this._context.Pets.Update(_pet);

                //Salvo as alterações.
                await this._context.SaveChangesAsync();
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
