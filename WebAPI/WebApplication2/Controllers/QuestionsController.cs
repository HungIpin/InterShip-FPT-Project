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
    public class QuestionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Counts")]
        public async Task<ActionResult> GetNumOfQuestions()
        {
            var list = await _context.Questions.ToListAsync();
            return Content(list.Count.ToString());
        }

        // GET: api/Questions
        //[HttpGet]
        //[ActionName("GetQuestions")]
        //public async Task<ActionResult<QuestionContainerViewModel>> GetQuestions()
        //{
        //    QuestionContainerViewModel container = new QuestionContainerViewModel();

        //    // container.Questions =  await _context.Questions.Include(e => e.QuestionType).Include(e => e.QuestionPool).Include(e => e.SelectionSetting).ToListAsync();
        //    container.Questions = await _context.Questions.Include(e => e.QuestionChoices).ToListAsync();
        //    container.QuestionSettings = await _context.QuestionSetting.Include(e => e.Question).ToListAsync();

        //    return container;
        //}
        [HttpGet]
        [ActionName("GetQuestions")]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            return await _context.Questions.Include(e => e.QuestionType).Include(e => e.QuestionPool).Include(e => e.SelectionSetting).Include(e => e.QuestionSetting).Include(e => e.QuestionChoices).ToListAsync();
        }
        [HttpGet]
        [ActionName("GetQuestionsAsync")]
        [Route("Admin")]
        public async Task<ActionResult<QuestionViewModel>> GetQuestionsAsync()
        {
            QuestionViewModel questionViewModel = new QuestionViewModel();
            questionViewModel.Questions = _context.Questions.Include(e => e.QuestionType).Include(e => e.QuestionPool).Include(e => e.SelectionSetting).ToList();
            questionViewModel.QuestionTypes = _context.QuestionTypes.Select(c => new QuestionType { Id = c.Id, Name = c.Name }).ToList();
            questionViewModel.QuestionPools = _context.QuestionPools.Select(c => new QuestionPool { Id = c.Id, Name = c.Name }).ToList();
            questionViewModel.SelectionSettings = _context.SelectionSettings.Select(c => new SelectionSetting { Id = c.Id, Name = c.Name }).ToList();
            questionViewModel.QuestionSettings = _context.QuestionSetting.ToList();
            return questionViewModel;
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, Question question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }

            _context.Entry(question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return RedirectToAction("GetQuestions", "Questions");
                }
            }

            _context.Entry(question.QuestionSetting).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            for (int i = 0; i < question.QuestionChoices.Count; i++)
                _context.Entry(question.QuestionChoices.ElementAt(i)).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction("GetQuestions", "Questions");
        }

        // POST: api/Questions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            int id = question.Id;

            question.QuestionSetting.CreatedDate = DateTime.Now;
            question.QuestionSetting.QuestionId = id;
            _context.QuestionSetting.Add(question.QuestionSetting);

            for (int i = 0; i < question.QuestionChoices.Count; i++)
                question.QuestionChoices.ElementAt(i).QuestionId = id;
            _context.QuestionChoices.AddRange(question.QuestionChoices);

            for (int i = 0; i < question.QuestionAttachments.Count; i++)
                question.QuestionAttachments.ElementAt(i).QuestionId = id;
            _context.QuestionAttachment.AddRange(question.QuestionAttachments);

            await _context.SaveChangesAsync();

            return RedirectToAction("GetQuestions", "Questions");
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Question>> DeleteQuestion(int id)
        {
            var choices = await _context.QuestionChoices.Where(m => m.QuestionId == id).ToListAsync();
            _context.QuestionChoices.RemoveRange(choices);

            var setting = await _context.QuestionSetting.SingleOrDefaultAsync(m => m.QuestionId == id);
            _context.QuestionSetting.Remove(setting);

            var attachments = await _context.QuestionAttachment.Where(m => m.QuestionId == id).ToListAsync();
            _context.QuestionAttachment.RemoveRange(attachments);

            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return question;
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
