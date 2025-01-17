using GestionBibliothequeAPI.Data;
using GestionBibliothequeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionBibliothequeAPI.Controlleurs
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpruntsControlleur : ControllerBase
    {
        private readonly BibliothequeContext _context;

        public EmpruntsControlleur(BibliothequeContext context)
        {
            _context = context;
        }

        // GET: api/EmpruntsControlleur
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emprunt>>> GetEmprunts()
        {
            return await _context.Emprunts.Include(e => e.Livre).ToListAsync();
        }

        // GET: api/EmpruntsControlleur/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Emprunt>> GetEmprunt(int id)
        {
            var emprunt = await _context.Emprunts.Include(e => e.Livre)
                                                 .FirstOrDefaultAsync(e => e.Id == id);

            if (emprunt == null)
            {
                return NotFound();
            }

            return emprunt;
        }

        // POST: api/EmpruntsControlleur
        [HttpPost]
        public async Task<ActionResult<Emprunt>> PostEmprunt(Emprunt emprunt)
        {
            _context.Emprunts.Add(emprunt);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmprunt), new { id = emprunt.Id }, emprunt);
        }

        // PUT: api/EmpruntsControlleur/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmprunt(int id, Emprunt emprunt)
        {
            if (id != emprunt.Id)
            {
                return BadRequest();
            }

            _context.Entry(emprunt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Emprunts.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/EmpruntsControlleur/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmprunt(int id)
        {
            var emprunt = await _context.Emprunts.FindAsync(id);
            if (emprunt == null)
            {
                return NotFound();
            }

            _context.Emprunts.Remove(emprunt);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
