using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptiFuel.Data;
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
            return await _appDbContext.Planings.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Planning>> GetPlaning(int id)
        {
            var planning = await _appDbContext.Planings.FindAsync(id);

            if (planning == null)
            {
                return NotFound();
            }

            return planning;
        }

        [HttpPost]
        public async Task<ActionResult<Planning>> PostPlaning([FromBody]Planning planning)
        {
            _appDbContext.Planings.Add(planning);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetPlanning", new { id = planning.Id }, planning);
        }

        [HttpPost("{id}/uploadBL")]
        public async Task<IActionResult> UploadBL(int id, IFormFile file)
        {
            var planning = await _appDbContext.Planings.FindAsync(id);
            if (planning == null)
            {
                return NotFound();
            }
            if (planning.BonDeLivraison!=null)
            {
                return BadRequest(" Bon de Livraison already exists fr this planning.");
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                planning.BonDeLivraison = new BonDeLivraison
                {
                    BLFile = memoryStream.ToArray(),
                    DateValidation = DateTime.Now,
                    PlanningId = id
                };
            }
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpPost("{id}/uploadCertificat")]
        public async Task<IActionResult> UploadCertificat(int id, IFormFile file)
        {
            var planning = await _appDbContext.Planings.FindAsync(id);
            if (planning == null)
            {
                return NotFound();
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                planning.Certificat = new Certificat
                {
                    CertificatFile = memoryStream.ToArray(),
                    DateUpload = DateTime.Now
                };
            }

            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }



    }
}
