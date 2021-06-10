using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CertificationsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Top")]
        public async Task<ActionResult<IEnumerable<Certification>>> GetTop5Certifications()
        {
            return await _context.Certifications.OrderByDescending(e => e.TakenTimes).Take(5).ToListAsync();
        }

        [HttpGet]
        [Route("CountExams")]
        public async Task<ActionResult<IEnumerable<ExamsInCertificationVM>>> GetExamsinCertifications()
        {
            var listCertifications = await _context.Certifications.Include(m => m.Exams).OrderByDescending(e => e.TakenTimes).Take(5).ToListAsync();
            List<ExamsInCertificationVM> examsInCertifications = new List<ExamsInCertificationVM>();
            foreach (Certification certification in listCertifications)
                examsInCertifications.Add(new ExamsInCertificationVM() { Id = certification.Id, Name = certification.Name, NumOfExams = certification.Exams.Count });
            return examsInCertifications;
        }
        [HttpGet]
        [Route("Counts")]
        public async Task<ActionResult> GetNumOfCertifications()
        {
            var list = await _context.Certifications.ToListAsync();
            return Content(list.Count.ToString());
        }

        // GET: api/Certifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Certification>>> GetCertifications()
        {
            return await _context.Certifications.Include(m => m.SkillinCertifications).ThenInclude(m => m.Skill).ToListAsync();
        }

        // GET: api/Certifications/5
        [HttpGet("{id}")]

        public async Task<ActionResult<Certification>> GetCertification(string id)
        {
            var certification = await _context.Certifications
                .Include(m => m.SkillinCertifications)
                .ThenInclude(m => m.Skill)
                .FirstAsync(c => c.Id == id);

            if (certification == null)
            {
                return NotFound();
            }

            return certification;
        }

        // GET: api/Certifications/GetCertification/name
        [HttpGet]
        [Route("GetCertification/{name}")]

        public async Task<ActionResult<IEnumerable<Certification>>> GetCertificationSearch(string name)
        {
            var certification = await _context.Certifications
                .Include(m => m.SkillinCertifications)
                .ThenInclude(m => m.Skill)
                .Where((a => a.Name.ToLower().Contains(name.ToLower()))).ToListAsync();

            if (certification == null)
            {
                return NotFound();
            }

            return certification;
        }

        // PUT: api/Certifications/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCertification(string id, [FromForm] Certification certification)
        {

            if (id != certification.Id)
            {
                return BadRequest();
            }
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var file = HttpContext.Request.Form.Files[0];

                byte[] fileData = null;

                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    fileData = binaryReader.ReadBytes((int)file.Length);
                }

                certification.Image = fileData;
            }
            _context.Entry(certification).State = EntityState.Modified;

            var skillListInDb = await _context.SkillinCertifications.Where(s => s.CertificationId == certification.Id).ToListAsync();
            _context.SkillinCertifications.RemoveRange(skillListInDb);
            var skillList = JsonConvert.DeserializeObject<List<SkillinCertification>>(HttpContext.Request.Form["SkillinCertifications"]);
            _context.SkillinCertifications.AddRange(skillList);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var certificate = await _context.Certifications
                .Include(m => m.SkillinCertifications)
                .ThenInclude(m => m.Skill)
                .FirstAsync(c => c.Id == certification.Id);
            return CreatedAtAction("GetCertification", new { id = certification.Id }, certificate);
        }

        // POST: api/Certifications
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Certification>> PostCertification([FromForm] Certification certification)
        {
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var file = HttpContext.Request.Form.Files[0];

                byte[] fileData = null;

                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    fileData = binaryReader.ReadBytes((int)file.Length);
                }

                certification.Image = fileData;
            }
            _context.Certifications.Add(certification);
            var skillList = JsonConvert.DeserializeObject<List<SkillinCertification>>(HttpContext.Request.Form["SkillinCertifications"]);
            _context.SkillinCertifications.AddRange(skillList);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CertificationExists(certification.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            var certificate = await _context.Certifications
                .Include(m => m.SkillinCertifications)
                .ThenInclude(m => m.Skill)
                .FirstAsync(c => c.Id == certification.Id);
            return certificate;
        }

        // DELETE: api/Certifications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Certification>> DeleteCertification(string id)
        {
            var certification = await _context.Certifications.FindAsync(id);

            if (certification == null)
            {
                return NotFound();
            }
            var skillListInDb = await _context.SkillinCertifications.Where(s => s.CertificationId == certification.Id).ToListAsync();
            _context.SkillinCertifications.RemoveRange(skillListInDb);

            _context.Certifications.Remove(certification);
            await _context.SaveChangesAsync();

            return certification;
        }

        private bool CertificationExists(string id)
        {
            return _context.Certifications.Any(e => e.Id == id);
        }
    }
}
