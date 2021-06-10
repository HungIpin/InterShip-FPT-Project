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
    public class ExamHistoryDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExamHistoryDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ExamHistoryDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamHistoryDetail>>> GetExamHistoryDetails()
        {
            return await _context.ExamHistoryDetails.ToListAsync();
        }

        // GET: api/ExamHistoryDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamHistoryDetail>> GetExamHistoryDetail(int id)
        {
            var examHistoryDetail = await _context.ExamHistoryDetails.FindAsync(id);

            if (examHistoryDetail == null)
            {
                return NotFound();
            }

            return examHistoryDetail;
        }

        // PUT: api/ExamHistoryDetails/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamHistoryDetail(int id, ExamHistoryDetail examHistoryDetail)
        {
            if (id != examHistoryDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(examHistoryDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamHistoryDetailExists(id))
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

        // POST: api/ExamHistoryDetails
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ExamHistoryDetail>> PostExamHistoryDetail(ExamHistoryDetail examHistoryDetail)
        {
            _context.ExamHistoryDetails.Add(examHistoryDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExamHistoryDetail", new { id = examHistoryDetail.Id }, examHistoryDetail);
        }

        // DELETE: api/ExamHistoryDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExamHistoryDetail>> DeleteExamHistoryDetail(int id)
        {
            var examHistoryDetail = await _context.ExamHistoryDetails.FindAsync(id);
            if (examHistoryDetail == null)
            {
                return NotFound();
            }

            _context.ExamHistoryDetails.Remove(examHistoryDetail);
            await _context.SaveChangesAsync();

            return examHistoryDetail;
        }

        private bool ExamHistoryDetailExists(int id)
        {
            return _context.ExamHistoryDetails.Any(e => e.Id == id);
        }
    }
 }