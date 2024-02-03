using dotnetAPI.Data;
using dotnetAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SideKickController : ControllerBase
    {
        private readonly DataContext _context;

        public SideKickController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SideKick>>> GetSideKicks()
        {
            return await _context.SideKicks.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SideKick>> GetSideKick(int id)
        {
            var sideKick = await _context.SideKicks.FindAsync(id);

            if (sideKick == null)
            {
                return NotFound();
            }

            return sideKick;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSideKick(int id, SideKick sideKick)
        {
            if (id != sideKick.SidekickId)
            {
                return BadRequest();
            }

            _context.Entry(sideKick).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!SideKickExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<SideKick>> PostSideKick(SideKick sideKick)
        {
            _context.SideKicks.Add(sideKick);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSideKick", new { id = sideKick.SidekickId }, sideKick);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSideKick(int id)
        {
            var sideKick = await _context.SideKicks.FindAsync(id);
            if (sideKick == null)
            {
                return NotFound();
            }

            _context.SideKicks.Remove(sideKick);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SideKickExists(int id)
        {
            return _context.SideKicks.Any(e => e.SidekickId == id);
        }
    }
}