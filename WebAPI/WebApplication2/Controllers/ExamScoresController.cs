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
    public class ExamScoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExamScoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ExamScores
        [HttpGet]
        [ActionName("GetExamScores")]
        public async Task<ActionResult<IEnumerable<ExamScore>>> GetExamScores()
        {
            return await _context.ExamScores.Include(e => e.Account).Include(e => e.Exam).ToListAsync();
        }
        [HttpGet]
        [ActionName("GetExamScoresAsync")]
        [Route("Admin")]
        public async Task<ActionResult<ExamScoreViewModel>> GetExamScoresAsync()
        {
            ExamScoreViewModel examScoreViewModel = new ExamScoreViewModel();
            examScoreViewModel.ExamScores = _context.ExamScores.Include(e => e.Exam).Include(e => e.Account).ToList();
            examScoreViewModel.Exams = _context.Exams.Select(c => new Exam { Id = c.Id, Name = c.Name }).ToList();
            examScoreViewModel.Accounts = _context.Accounts.Select(c => new Account { Id = c.Id }).ToList();
            return examScoreViewModel;
        }

        // GET: api/ExamScores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamScore>> GetExamScore(string id)
        {
            var examScore = await _context.ExamScores.FindAsync(id);

            if (examScore == null)
            {
                return NotFound();
            }

            return examScore;
        }

        // PUT: api/ExamScores/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamScore(string id, ExamScore examScore)
        {
            if (id != examScore.ExamId)
            {
                return BadRequest();
            }

            _context.Entry(examScore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamScoreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return RedirectToAction("GetExamScores", "ExamScores");
                }
            }

            return RedirectToAction("GetExamScores", "ExamScores");
        }

        // POST: api/ExamScores
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ExamScore>> PostExamScore(ExamScore examScore)
        {
            _context.ExamScores.Add(examScore);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExamScoreExists(examScore.ExamId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("GetExamScores", "ExamScores");
        }

        // DELETE: api/ExamScores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExamScore>> DeleteExamScore(string id)
        {
            var examScore = await _context.ExamScores.FindAsync(id);
            if (examScore == null)
            {
                return NotFound();
            }

            _context.ExamScores.Remove(examScore);
            await _context.SaveChangesAsync();

            return examScore;
        }

        private bool ExamScoreExists(string id)
        {
            return _context.ExamScores.Any(e => e.ExamId == id);
        }

        [HttpGet]
        [Route("Counts/{id}")]
        public async Task<ActionResult> GetTakenTimes(int id)
        {
            var list = _context.ExamScores.Where(u => u.AccountId == id).ToList();
            return Content(list.Count.ToString());
        }

        [HttpGet]
        [Route("Top/{id}")]
        public async Task<ActionResult> GetHighestScore(int id)
        {
            var highest = _context.ExamScores.Where(u => u.AccountId == id).OrderByDescending(e => e.Points).FirstOrDefault();

            return Content(highest == null ? "0" : highest.Points.ToString());
        }
    }
}