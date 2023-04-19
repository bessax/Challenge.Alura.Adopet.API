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
    public class TutoresController : ControllerBase
    {
        private readonly AdoPetContext _context;
        private readonly ITutorService _tutorService;

        public TutoresController(AdoPetContext context, ITutorService tutorService)
        {
            _context = context;
           _tutorService = tutorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TutorDTO>>> GetTutores()        {  

            return await _tutorService.BuscaTodosAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TutorDTO>> GetTutor(int id)
        {           

            return await _tutorService.BuscaPorIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Tutor>> PostTutor(TutorDTO tutor)
        {
            try
            {
                await _tutorService.CriarAsync(tutor);              

                return this.CreatedAtAction("GetTutor", new { id = tutor.Id }, tutor);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TutorDTO>> DeleteTutor(int id)
        {
            var _tutor = _tutorService.BuscaPorIdAsync(id);
            if (_tutor is null)
            {
                return this.NotFound("Tutor não encontrado na base de dados.");
            }
          
            
            try
            {
                var result = await _tutorService.DeletaAsync(_tutor.Result);

            }
            catch (Exception ex) {return BadRequest(ex.Message);}   
            return Ok(_tutor);        
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Tutor>> PutTutor(int id,Tutor tutor)
        {
            var _tutor = await _tutorService.BuscaPorIdAsync(id);
            if (_tutor is null)
            {
                return this.NotFound("Tutor não encontrado na base de dados para atualização.");
            }

            try
            {
                //Coloco o objeto recuperado em modo de `modificação`.
                await _tutorService.AlteraAsync(_tutor);
      
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
