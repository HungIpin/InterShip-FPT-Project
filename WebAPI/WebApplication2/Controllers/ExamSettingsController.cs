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
    public class ExamSettingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExamSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ExamSettings
        [HttpGet]
        [ActionName("GetExamSettingsControllers")]
        public async Task<ActionResult<IEnumerable<ExamSetting>>> GetExamSettingsControllers()
        {
           
            return await _context.ExamSettings.Include(e => e.Exam).ToListAsync();
        }
        [HttpGet]
        [ActionName("GetExamSettingsAsync")]
        [Route("Admin")]
        public async Task<ActionResult<ExamSettingViewModel>> GetExamSettingsAsync()
        {
            ExamSettingViewModel examSettingViewModels = new ExamSettingViewModel();
            examSettingViewModels.ExamSettings = _context.ExamSettings.Include(e => e.Exam).ToList();
            examSettingViewModels.Exams = _context.Exams.Select(c => new Exam { Id = c.Id }).ToList();
            return examSettingViewModels;
        }

        // GET: api/ExamSettings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamSetting>> GetExamSetting(int id)
        {
            var examSetting = await _context.ExamSettings.FindAsync(id);

            if (examSetting == null)
            {
                return NotFound();
            }

            return examSetting;
        }

        // PUT: api/ExamSettings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamSetting(int id, ExamSetting examSetting)
        {
            if (id != examSetting.Id)
            {
                return BadRequest();
            }

            _context.Entry(examSetting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamSettingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return RedirectToAction("GetExamSettings", "ExamSettings");
                }
            }

            return RedirectToAction("GetExamSettings", "ExamSettings");
        }

        // POST: api/ExamSettings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ExamSetting>> PostExamSetting(ExamSetting examSetting)
        {
            _context.ExamSettings.Add(examSetting);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExamSettingExists(examSetting.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("GetExamSettings", "ExamSettings");
        }

        // DELETE: api/ExamSettings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExamSetting>> DeleteExamSetting(int id)
        {
            var examSetting = await _context.ExamSettings.FindAsync(id);
            if (examSetting == null)
            {
                return NotFound();
            }

            _context.ExamSettings.Remove(examSetting);
            await _context.SaveChangesAsync();

            return examSetting;
        }

        private bool ExamSettingExists(int id)
        {
            return _context.ExamSettings.Any(e => e.Id == id);
        }
    }
}