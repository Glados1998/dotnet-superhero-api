using dotnetAPI.Data;
using dotnetAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuperHero>>> GetSuperHeroes()
        {
            return await _context.SuperHeroes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSuperHero(int id)
        {
            var superHero = await _context.SuperHeroes.FindAsync(id);

            if (superHero == null)
            {
                return NotFound();
            }

            return superHero;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuperHero(int id, SuperHero superHero)
        {
            if (id != superHero.Id)
            {
                return BadRequest();
            }

            _context.Entry(superHero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!SuperHeroExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<SuperHero>> PostSuperHero(SuperHero superHero)
        {
            _context.SuperHeroes.Add(superHero);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSuperHero", new { id = superHero.Id }, superHero);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuperHero(int id)
        {
            var superHero = await _context.SuperHeroes.FindAsync(id);
            if (superHero == null)
            {
                return NotFound();
            }

            _context.SuperHeroes.Remove(superHero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SuperHeroExists(int id)
        {
            return _context.SuperHeroes.Any(e => e.Id == id);
        }
    }
}