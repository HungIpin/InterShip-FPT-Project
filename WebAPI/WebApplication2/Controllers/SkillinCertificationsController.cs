using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillinCertificationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SkillinCertificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SkillinCertifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillinCertification>>> GetSkillinCertifications()
        {
            return await _context.SkillinCertifications.ToListAsync();
        }

        // GET: api/SkillinCertifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillinCertification>> GetSkillinCertification(string id)
        {
            var skillinCertification = await _context.SkillinCertifications.FindAsync(id);

            if (skillinCertification == null)
            {
                return NotFound();
            }

            return skillinCertification;
        }

        // PUT: api/SkillinCertifications/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkillinCertification(string id, [FromForm]SkillinCertification skillinCertification)
        {
            if (id != skillinCertification.SkillId)
            {
                return BadRequest();
            }

            _context.Entry(skillinCertification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillinCertificationExists(id))
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

        // POST: api/SkillinCertifications
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SkillinCertification>> PostSkillinCertification([FromForm]SkillinCertification skillinCertification)
        {
            _context.SkillinCertifications.Add(skillinCertification);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SkillinCertificationExists(skillinCertification.SkillId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSkillinCertification", new { id = skillinCertification.SkillId }, skillinCertification);
        }

        // DELETE: api/SkillinCertifications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkillinCertification>> DeleteSkillinCertification(string id)
        {
            var skillinCertification = await _context.SkillinCertifications.FindAsync(id);
            if (skillinCertification == null)
            {
                return NotFound();
            }

            _context.SkillinCertifications.Remove(skillinCertification);
            await _context.SaveChangesAsync();

            return skillinCertification;
        }

        private bool SkillinCertificationExists(string id)
        {
            return _context.SkillinCertifications.Any(e => e.SkillId == id);
        }
    }
}