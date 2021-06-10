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
    public class ExamPartsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExamPartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ExamParts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamPart>>> GetExamParts()
        {
            return await _context.ExamParts.ToListAsync();
        }

        // GET: api/ExamParts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamPart>> GetExamPart(int id)
        {
            var examPart = await _context.ExamParts.FindAsync(id);

            if (examPart == null)
            {
                return NotFound();
            }

            return examPart;
        }

        // PUT: api/ExamParts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamPart(int id, ExamPart examPart)
        {
            if (id != examPart.Id)
            {
                return BadRequest();
            }

            _context.Entry(examPart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamPartExists(id))
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

        // POST: api/ExamParts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ExamPart>> PostExamPart(ExamPart examPart)
        {
            _context.ExamParts.Add(examPart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExamPart", new { id = examPart.Id }, examPart);
        }

        // DELETE: api/ExamParts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExamPart>> DeleteExamPart(int id)
        {
            var examPart = await _context.ExamParts.FindAsync(id);
            if (examPart == null)
            {
                return NotFound();
            }

            _context.ExamParts.Remove(examPart);
            await _context.SaveChangesAsync();

            return examPart;
        }

        private bool ExamPartExists(int id)
        {
            return _context.ExamParts.Any(e => e.Id == id);
        }
    }
}