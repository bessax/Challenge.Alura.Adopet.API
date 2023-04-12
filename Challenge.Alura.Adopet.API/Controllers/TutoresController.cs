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
    public class TutoresController : ControllerBase
    {
        private readonly AdoPetContext _context;

        public TutoresController(AdoPetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tutor>>> GetTutores()
        {
            var turores = await this._context.Tutores.ToArrayAsync();

            if (turores == null)
            {
                return this.NotFound();
            }

            return turores;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tutor>> GetTutor(int id)
        {
            var _tutor = await this._context.Tutores.FirstAsync(a => a.Id == id);

            if (_tutor == null)
            {
                return this.NotFound();
            }

            return _tutor;
        }

        [HttpPost]
        public async Task<ActionResult<Tutor>> PostTutor(Tutor tutor)
        {
            try
            {
                await this._context.Tutores.AddAsync(tutor);
                await this._context.SaveChangesAsync();

                return this.CreatedAtAction("GetTutor", new { id = tutor.Id }, tutor);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Tutor>> DeleteTutor(int id)
        { 
            var _tutor = await this._context.Tutores.FirstOrDefaultAsync(a => a.Id == id);
            if(_tutor is null)
            {
                return this.NotFound("Tutor não encontrado na base de dados.");
            }
            try
            {
                this._context.Tutores.Remove(_tutor);
                await this._context.SaveChangesAsync(); 

            }
            catch (Exception ex) {return BadRequest(ex.Message);}   
            return Ok(_tutor);        
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Tutor>> PutTutor(int id,Tutor tutor)
        {
            var _tutor = await this._context.Tutores.FirstOrDefaultAsync(a => a.Id == id);
            if (_tutor is null)
            {
                return this.NotFound("Tutor não encontrado na base de dados para atualização.");
            }

            try
            {
                //Coloco o objeto recuperado em modo de `modificação`.
                _context.Entry(_tutor).State = EntityState.Modified;
                
                // Atualizo os campos do objeto com as novas informações.
                _tutor.Nome= tutor.Nome;
                _tutor.Email= tutor.Email;
                _tutor.Senha= tutor.Senha;
                _tutor.Imagem= tutor.Imagem;   
                _tutor.NomeDoAnimal= tutor.NomeDoAnimal;

                //Atualizo o objeto no contexto.
                this._context.Tutores.Update(_tutor);

                //Salvo as alterações.
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message); 
            }
            return Ok(_tutor);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Tutor>> PatchTutor(int id, JsonPatchDocument<Tutor> patch)
        {
            var _tutor = await this._context.Tutores.FirstOrDefaultAsync(a => a.Id == id);
            if (_tutor is null)
            {
                return this.NotFound("Tutor não encontrado na base de dados para atualização.");
            }

            patch.ApplyTo(_tutor, ModelState);
            if (!TryValidateModel(_tutor))
            {
                return ValidationProblem(ModelState);
            }
            _context.SaveChanges();
            return Ok(_tutor);

        }
    }
}
