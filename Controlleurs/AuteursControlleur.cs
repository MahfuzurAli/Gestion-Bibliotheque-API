using GestionBibliothequeAPI.Data;
using GestionBibliothequeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionBibliothequeAPI.Controlleurs
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuteursControlleur : ControllerBase
    {
        private readonly BibliothequeContext _context;

        public AuteursControlleur(BibliothequeContext context)
        {
            _context = context;
        }

        // GET: api/AuteursControlleur
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auteur>>> GetAuteurs()
        {
            return await _context.Auteurs.ToListAsync();
        }

        // GET: api/AuteursControlleur/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Auteur>> GetAuteur(int id)
        {
            var auteur = await _context.Auteurs.FindAsync(id);

            if (auteur == null)
            {
                return NotFound();
            }

            return auteur;
        }

        // POST: api/AuteursControlleur
        [HttpPost]
        public async Task<ActionResult<Auteur>> PostAuteur(Auteur auteur)
        {
            auteur.DateNaissanceUTC(auteur.DateNaissance);
            _context.Auteurs.Add(auteur);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuteur), new { id = auteur.Id }, auteur);
        }

        // PUT: api/AuteursControlleur/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuteur(int id, Auteur auteur)
        {
            if (id != auteur.Id)
            {
                return BadRequest();
            }

            _context.Entry(auteur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Auteurs.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/AuteursControlleur/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuteur(int id)
        {
            var auteur = await _context.Auteurs.FindAsync(id);
            if (auteur == null)
            {
                return NotFound();
            }

            _context.Auteurs.Remove(auteur);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
