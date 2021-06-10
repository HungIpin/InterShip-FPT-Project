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
    public class QuestionChoicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuestionChoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/QuestionChoices
        [HttpGet]
        [ActionName("GetQuestionChoices")]
        public async Task<ActionResult<IEnumerable<QuestionChoice>>> GetQuestionChoices()
        {
            return await _context.QuestionChoices.Include(e => e.Question).ToListAsync();
        }
        [HttpGet]
        [ActionName("GetQuestionChoicesAsync")]
        [Route("Admin")]
        public async Task<ActionResult<QuestionChoiceViewModel>> GetQuestionChoicesAsync()
        {
            QuestionChoiceViewModel questionChoiceViewModel = new QuestionChoiceViewModel();
            questionChoiceViewModel.QuestionChoices = _context.QuestionChoices.Include(e => e.Question).ToList();
            questionChoiceViewModel.Questions = _context.Questions.Select(c => new Question { Id = c.Id}).ToList();
            return questionChoiceViewModel;
        }

        // GET: api/QuestionChoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionChoice>> GetQuestionChoice(int id)
        {
            var questionChoice = await _context.QuestionChoices.FindAsync(id);

            if (questionChoice == null)
            {
                return NotFound();
            }

            return questionChoice;
        }

        // PUT: api/QuestionChoices/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionChoice(int id, QuestionChoice questionChoice)
        {
            if (id != questionChoice.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionChoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionChoiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return RedirectToAction("GetQuestionChoices", "QuestionChoices");
                }
            }

            return RedirectToAction("GetQuestionChoices", "QuestionChoices");
        }

        // POST: api/QuestionChoices
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult> PostQuestionChoice(QuestionChoice questionChoice)
        {
            _context.QuestionChoices.Add(questionChoice);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (QuestionChoiceExists(questionChoice.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtAction("GetExam", new { id = exam.Id }, exam);
            return RedirectToAction("GetQuestionChoices", "QuestionChoices");
            //return CreatedAtAction("GetQuestionChoice", new { id = questionChoice.Id }, questionChoice);
        }

        // DELETE: api/QuestionChoices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuestionChoice>> DeleteQuestionChoice(int id)
        {
            var questionChoice = await _context.QuestionChoices.FindAsync(id);
            if (questionChoice == null)
            {
                return NotFound();
            }

            _context.QuestionChoices.Remove(questionChoice);
            await _context.SaveChangesAsync();

            return questionChoice;
        }

        private bool QuestionChoiceExists(int id)
        {
            return _context.QuestionChoices.Any(e => e.Id == id);
        }
    }
}
