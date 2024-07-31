using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptiFuel.Data;
using OptiFuel.Models;

namespace OptiFuel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentreController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public CentreController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Centre>>> GetContacts()
        {
            return await _appDbContext.centres.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Centre>> GetCentre(Guid id)
        {
            var centre = await _appDbContext.centres.FindAsync(id);

            if (centre == null)
            {
                return NotFound();
            }

            return centre;
        }

        [HttpPost]
        public async Task<ActionResult<Centre>> PostCentre(Centre centre)
        {
            centre.Id = Guid.NewGuid();
            centre.e_created_on = DateTime.UtcNow;
            _appDbContext.centres.Add(centre);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCentre), new { id = centre.Id }, centre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCentre(Guid id, Centre centre)
        {
            if (id != centre.Id)
            {
                return BadRequest();
            }

            centre.e_updated_on = DateTime.UtcNow;
            _appDbContext.Entry(centre).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCentre(Guid id)
        {
            var centre = await _appDbContext.centres.FindAsync(id);
            if (centre == null)
            {
                return NotFound();
            }

            _appDbContext.centres.Remove(centre);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(Guid id)
        {
            return _appDbContext.centres.Any(e => e.Id == id);
        }
    }
}

