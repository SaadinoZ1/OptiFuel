using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptiFuel.Data;
using OptiFuel.Models;

namespace OptiFuel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BLController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public BLController(AppDbContext context)
        {
            _appDbContext = context;
        }


        [HttpPost("{planningId}/uploadBL")]
        public async Task<IActionResult> UploadBL(int planningId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Upload a valid file");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var bonDeLivraison = new BonDeLivraison
            {
                PlanningId = planningId,
                BLFile = memoryStream.ToArray()
            };

            _appDbContext.bonDeLivraisons.Add(bonDeLivraison);
            await _appDbContext.SaveChangesAsync();

            return Ok(new { bonDeLivraison.Id });
        }




    }
}
