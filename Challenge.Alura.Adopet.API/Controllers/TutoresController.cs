using Microsoft.AspNetCore.Mvc;
using Challenge.Alura.Adopet.API.Data;
using Challenge.Alura.Adopet.API.Dominio;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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


    }
}
