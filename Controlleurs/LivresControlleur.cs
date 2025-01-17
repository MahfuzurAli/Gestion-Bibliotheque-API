using GestionBibliothequeAPI.Data;
using GestionBibliothequeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionBibliothequeAPI.Controlleurs
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivresControlleur : ControllerBase
    {
        private readonly BibliothequeContext _context;

        public LivresControlleur(BibliothequeContext context)
        {
            _context = context;
        }

        // GET: api/LivresControlleur
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livre>>> GetLivres()
        {
            return await _context.Livres.Include(l => l.Auteur).ToListAsync();
        }

        // GET: api/LivresControlleur/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Livre>> GetLivre(int id)
        {
            var livre = await _context.Livres.Include(l => l.Auteur).FirstOrDefaultAsync(l => l.Id == id);

            if (livre == null)
            {
                return NotFound();
            }

            return livre;
        }

        // POST: api/LivresControlleur
        [HttpPost]
        public async Task<ActionResult<Livre>> PostLivre(Livre livre)
        {
            _context.Livres.Add(livre);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLivre), new { id = livre.Id }, livre);
        }

        // PUT: api/LivresControlleur/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivre(int id, Livre livre)
        {
            if (id != livre.Id)
            {
                return BadRequest();
            }

            _context.Entry(livre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Livres.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/LivresControlleur/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivre(int id)
        {
            var livre = await _context.Livres.FindAsync(id);
            if (livre == null)
            {
                return NotFound();
            }

            _context.Livres.Remove(livre);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
