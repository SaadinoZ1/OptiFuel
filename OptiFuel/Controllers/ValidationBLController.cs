using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptiFuel.Data;
using OptiFuel.Dtos;
using OptiFuel.Models;
using System.Text.Json;

namespace OptiFuel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidationBLController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public ValidationBLController(AppDbContext context)
        {
            _appDbContext = context;
        }



        // GET: api/ValidationBL
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ValidationBL>>> GetValidationBLs()
        {
            return await _appDbContext.validationBLs
                .Include(v => v.Commissions)
                .ThenInclude(c => c.Contact)
                .ToListAsync();
        }

        // GET: api/ValidationBL/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ValidationBL>> GetValidationBL(Guid id)
        {
            var validationBL = await _appDbContext.validationBLs
                .Include(v => v.Commissions)
                .ThenInclude(c => c.Contact)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (validationBL == null)
            {
                return NotFound();
            }

            return validationBL;
        }

        // POST: api/ValidationBL
        [HttpPost("AddValidationBL")]
        public async Task<ActionResult<ValidationBL>> AddValidationBL([FromBody] ValidationBLDto validationBLDto)
        {
            if (validationBLDto == null)
                return BadRequest("Invalid validation BL data.");

            var commission = new Commission
            {
                Id = Guid.NewGuid(),
                CodeG = validationBLDto.Commission.FirstOrDefault()?.CodeG,
                CodeS = validationBLDto.Commission.FirstOrDefault()?.CodeS,
                e_created_on = DateTime.UtcNow,
                Contact = validationBLDto.Commission.Select(c => new Contact
                {
                    Id = c.ContactId,
                    // You can populate other Contact properties here if needed
                }).ToList()
            };

            var validationBL = new ValidationBL
            {
                Id = Guid.NewGuid(),
                PlanningId = validationBLDto.PlanningId,
                BLFile = validationBLDto.BLFile,
                CertificatJumelageFile = validationBLDto.CertificatJumelageFile,
                QuantitésBL = validationBLDto.QuantitésBL,
                StartTime = validationBLDto.StartTime,
                EndTime = validationBLDto.EndTime,
                e_created_on = DateTime.UtcNow,
                Commissions = commission
            };

            _appDbContext.validationBLs.Add(validationBL);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetValidationBL), new { id = validationBL.Id }, validationBL);

        }


        // POST: api/ValidationBL/UploadBL
        [HttpPost("UploadBL")]
        public async Task<IActionResult> UploadBL(Guid validationBLId, IFormFile blFile)
        {

            var validationBL = await _appDbContext.validationBLs.FindAsync(validationBLId);
            if (validationBL == null)
            {
                return NotFound();
            }

            using (var memoryStream = new MemoryStream())
            {
                await blFile.CopyToAsync(memoryStream);
                validationBL.BLFile = memoryStream.ToArray();
            }

            _appDbContext.Entry(validationBL).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/ValidationBL/UploadCertificat
        [HttpPost("UploadCertificat")]
        public async Task<IActionResult> UploadCertificat(Guid validationBLId, IFormFile certificatFile)
        {
            var validationBL = await _appDbContext.validationBLs.FindAsync(validationBLId);
            if (validationBL == null)
            {
                return NotFound();
            }

            using (var memoryStream = new MemoryStream())
            {
                await certificatFile.CopyToAsync(memoryStream);
                validationBL.CertificatJumelageFile = memoryStream.ToArray();
            }

            _appDbContext.Entry(validationBL).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();

            return NoContent();

        }

        // PUT: api/ValidationBL/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutValidationBL(Guid id, ValidationBL validationBL)
        {

            if (id != validationBL.Id)
            {
                return BadRequest();
            }

            _appDbContext.Entry(validationBL).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValidationBLExists(id))
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

        // DELETE: api/ValidationBL/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteValidationBL(Guid id)
        {
            var validationBL = await _appDbContext.validationBLs.FindAsync(id);
            if (validationBL == null)
            {
                return NotFound();
            }

            _appDbContext.validationBLs.Remove(validationBL);
            await _appDbContext.SaveChangesAsync();

            return NoContent();

        }

        // GET: api/ValidationBL/5/Commissions
        [HttpGet("{id}/Commissions")]
        public async Task<ActionResult<IEnumerable<Commission>>> GetCommissionsByValidationBL(Guid id)
        {
            var validationBL = await _appDbContext.validationBLs
                .Include(v => v.Commissions)
                .ThenInclude(c => c.Contact)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (validationBL == null)
            {
                return NotFound();
            }

            return Ok(validationBL.Commissions);

        }

        // POST: api/ValidationBL/5/Commissions
        [HttpPost("{id}/Commissions")]
        public async Task<ActionResult<Commission>> PostCommission(Guid id, Commission commission)
        {
            var validationBL = await _appDbContext.validationBLs.FindAsync(id);
            if (validationBL == null)
            {
                return NotFound();
            }

            commission.ValidationBLId = id;
            _appDbContext.Commissions.Add(commission);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetCommission", new { id = commission.Id }, commission);
        }

        private bool ValidationBLExists(Guid id)
        {
            return _appDbContext.validationBLs.Any(e => e.Id == id);
        }





    }
}
