using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptiFuel.Data;
using OptiFuel.Dtos;
using OptiFuel.Models;

namespace OptiFuel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanningController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public PlanningController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Planning>>> GetPlanings()
        {
            return await _appDbContext.Plannings.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Planning>> GetPlanning(int id)
        {
            var planning = await _appDbContext.Plannings.FindAsync(id);

            if (planning == null)
            {
                return NotFound();
            }

            return planning;
        }

        [HttpPost]
        public async Task<ActionResult<Planning>> PostPlanning([FromBody]PlanningDto planningDto)
        {
            var planning = planningDto.Adapt<Planning>(); 
            _appDbContext.Plannings.Add(planning);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetPlanning", new { id = planning.Id }, planning);
        }



    }
}
