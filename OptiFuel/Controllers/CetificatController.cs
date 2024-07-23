using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptiFuel.Data;
using OptiFuel.Models;

namespace OptiFuel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CetificatController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CetificatController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        [HttpPost("{planningId}/uploadCertificat")]
        public async Task<IActionResult> UploadCertificat(int planningId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Upload a valid file");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var certificat = new Certificat
            {
                PlanningId = planningId,
                CertificatFile = memoryStream.ToArray()
            };

            _appDbContext.certificats.Add(certificat);
            await _appDbContext.SaveChangesAsync();

            return Ok(new { certificat.Id });
        }



    }
}
