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
    public class QuestionSettingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuestionSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/QuestionAttachments
        [HttpGet]
        [ActionName("GetQuestionSettingsControllers")]
        public async Task<ActionResult<IEnumerable<QuestionSetting>>> GetQuestionSettings()
        {
            return await _context.QuestionSetting.Include(e => e.Question).ToListAsync();
        }
        [HttpGet]
        [ActionName("GetQuestionSettingsAsync")]
        [Route("Admin")]
        public async Task<ActionResult<QuestionSettingViewModel>> GetQuestionSettingsAsync()
        {
            QuestionSettingViewModel questionSettingViewModels = new QuestionSettingViewModel();
            questionSettingViewModels.QuestionSettings = _context.QuestionSetting.Include(e => e.Question).ToList();
            questionSettingViewModels.Questions = _context.Questions.Select(c => new Question { Id = c.Id }).ToList();
            return questionSettingViewModels;
        }

        // GET: api/QuestionSettings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionSetting>> GetQuestionSetting(int id)
        {
            var questionSetting = await _context.QuestionSetting.FindAsync(id);

            if (questionSetting == null)
            {
                return NotFound();
            }

            return questionSetting;
        }

        // PUT: api/QuestionSettings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionSetting(int id, QuestionSetting questionSetting)
        {
            if (id != questionSetting.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionSetting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionSettingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return RedirectToAction("GetQuestionSettings", "QuestionSettings");
                }
            }

            return RedirectToAction("GetQuestionSettings", "QuestionSettings");
        }

        // POST: api/QuestionSettings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<QuestionSetting>> PostQuestionSetting(QuestionSetting questionSetting)
        {
            _context.QuestionSetting.Add(questionSetting);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (QuestionSettingExists(questionSetting.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }


            return RedirectToAction("GetQuestionSetting", "QuestionSettings");
        }

        // DELETE: api/QuestionSettings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuestionSetting>> DeleteQuestionSetting(int id)
        {
            var questionSetting = await _context.QuestionSetting.FindAsync(id);
            if (questionSetting == null)
            {
                return NotFound();
            }

            _context.QuestionSetting.Remove(questionSetting);
            await _context.SaveChangesAsync();

            return questionSetting;
        }

        private bool QuestionSettingExists(int id)
        {
            return _context.QuestionSetting.Any(e => e.Id == id);
        }
    }
}