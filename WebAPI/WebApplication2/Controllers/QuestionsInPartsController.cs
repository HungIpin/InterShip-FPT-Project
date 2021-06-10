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
    public class QuestionsInPartsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuestionsInPartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/QuestionsInParts
        [HttpGet]
        [ActionName("GetQuestionsInParts")]
        public async Task<ActionResult<IEnumerable<QuestionsInPart>>> GetQuestionsInParts()
        {
            return await _context.QuestionsInParts.Include(e => e.ExamPart).Include(e => e.Question).ToListAsync();
        }
        [HttpGet]
        [ActionName("GetQuestionsInPartsAsync")]
        [Route("Admin")]
        public async Task<ActionResult<QuestionInPartViewModel>> GetQuestionsInPartsAsync()
        {
            QuestionInPartViewModel questionInPartViewModel = new QuestionInPartViewModel();
            questionInPartViewModel.QuestionInParts = _context.QuestionsInParts.Include(e => e.ExamPart).Include(e => e.Question).ToList();
            questionInPartViewModel.ExamParts = _context.ExamParts.Select(c => new ExamPart { Id = c.Id, Name = c.Name }).ToList();
            questionInPartViewModel.Questions = _context.Questions.Select(c => new Question { Id = c.Id }).ToList();
            return questionInPartViewModel;
        }

        // GET: api/QuestionsInParts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionsInPart>> GetQuestionsInPart(int id)
        {
            var questionsInPart = await _context.QuestionsInParts.FindAsync(id);

            if (questionsInPart == null)
            {
                return NotFound();
            }

            return questionsInPart;
        }

        // PUT: api/QuestionsInParts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionsInPart(int id, QuestionsInPart questionsInPart)
        {
            if (id != questionsInPart.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionsInPart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionsInPartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return RedirectToAction("GetQuestionsInParts", "QuestionsInParts");
                }
            }

            return RedirectToAction("GetQuestionsInParts", "QuestionsInParts");
        }

        // POST: api/QuestionsInParts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<QuestionsInPart>> PostQuestionsInPart(QuestionsInPart questionsInPart)
        {
            _context.QuestionsInParts.Add(questionsInPart);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (QuestionsInPartExists(questionsInPart.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtAction("GetExam", new { id = exam.Id }, exam);
            return RedirectToAction("GetQuestionsInParts", "QuestionsInParts");

            //return CreatedAtAction("GetQuestionsInPart", new { id = questionsInPart.Id }, questionsInPart);
        }

        // DELETE: api/QuestionsInParts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuestionsInPart>> DeleteQuestionsInPart(int id)
        {
            var questionsInPart = await _context.QuestionsInParts.FindAsync(id);
            if (questionsInPart == null)
            {
                return NotFound();
            }

            _context.QuestionsInParts.Remove(questionsInPart);
            await _context.SaveChangesAsync();

            return questionsInPart;
        }

        private bool QuestionsInPartExists(int id)
        {
            return _context.QuestionsInParts.Any(e => e.Id == id);
        }

        
    }
}
