using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
   [Route("api/[controller]")]
    [ApiController]
    public class AdvancedFeedbacksInExamsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdvancedFeedbacksInExamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AdvancedFeedbacksInExams
        [HttpGet]
        [ActionName("GetAdvancedFeedbacksInExams")]
        public async Task<ActionResult<IEnumerable<AdvancedFeedbacksInExam>>> GetAdvancedFeedbacksInExams()
        {
            return await _context.AdvancedFeedbacksInExams.Include(e => e.AdvancedFeedback).Include(e => e.Exam).ToListAsync();
        }
        [HttpGet]
        [ActionName("GetAdvancedFeedbacksInExamsAsync")]
        [Route("Admin")]
        public async Task<ActionResult<AdvancedFeedbacksInExamViewModel>> GetAdvancedFeedbacksInExamsAsync()
        {
            AdvancedFeedbacksInExamViewModel advancedFeedbacksInExams = new AdvancedFeedbacksInExamViewModel();
            advancedFeedbacksInExams.AdvancedFeedbacksInExams = _context.AdvancedFeedbacksInExams.Include(e => e.Exam).Include(e => e.AdvancedFeedback).ToList();
            advancedFeedbacksInExams.Exams = _context.Exams.Select(c => new Exam { Id = c.Id, Name = c.Name }).ToList();
            advancedFeedbacksInExams.AdvancedFeedbacks = _context.AdvancedFeedbacks.Select(c => new AdvancedFeedback { Id = c.Id }).ToList();
            return advancedFeedbacksInExams;
        }

        // GET: api/AdvancedFeedbacksInExams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdvancedFeedbacksInExam>> GetAdvancedFeedbacksInExam(string id)
        {
            var advancedFeedbacksInExam = await _context.AdvancedFeedbacksInExams.FindAsync(id);

            if (advancedFeedbacksInExam == null)
            {
                return NotFound();
            }

            return advancedFeedbacksInExam;
        }

        // PUT: api/AdvancedFeedbacksInExams/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdvancedFeedbacksInExam(string id, AdvancedFeedbacksInExam advancedFeedbacksInExam)
        {
            if (id != advancedFeedbacksInExam.ExamId)
            {
                return BadRequest();
            }

            _context.Entry(advancedFeedbacksInExam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvancedFeedbacksInExamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return RedirectToAction("GetAdvancedFeedbacksInExams", "AdvancedFeedbacksInExams");
                }
            }

            return RedirectToAction("GetAdvancedFeedbacksInExams", "AdvancedFeedbacksInExams");
        }

        // POST: api/AdvancedFeedbacksInExams
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AdvancedFeedbacksInExam>> PostAdvancedFeedbacksInExam(AdvancedFeedbacksInExam advancedFeedbacksInExam)
        {
            _context.AdvancedFeedbacksInExams.Add(advancedFeedbacksInExam);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdvancedFeedbacksInExamExists(advancedFeedbacksInExam.ExamId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("GetAdvancedFeedbacksInExams", "AdvancedFeedbacksInExams");
        }

        // DELETE: api/AdvancedFeedbacksInExams/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AdvancedFeedbacksInExam>> DeleteAdvancedFeedbacksInExam(string id)
        {
            var advancedFeedbacksInExam = await _context.AdvancedFeedbacksInExams.FindAsync(id);
            if (advancedFeedbacksInExam == null)
            {
                return NotFound();
            }

            _context.AdvancedFeedbacksInExams.Remove(advancedFeedbacksInExam);
            await _context.SaveChangesAsync();

            return advancedFeedbacksInExam;
        }

        private bool AdvancedFeedbacksInExamExists(string id)
        {
            return _context.AdvancedFeedbacksInExams.Any(e => e.ExamId == id);
        }
    }
}