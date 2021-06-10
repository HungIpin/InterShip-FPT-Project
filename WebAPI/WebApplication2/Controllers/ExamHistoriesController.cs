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
    public class ExamHistoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private object questionsInDb;

        public ExamHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ExamHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamHistory>>> GetExamHistories()
        {
            return await _context.ExamHistories.ToListAsync();
        }

        // GET: api/ExamHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamHistory>> GetExamHistory(int id)
        {
            var examHistory = await _context.ExamHistories.FindAsync(id);

            if (examHistory == null)
            {
                return NotFound();
            }

            return examHistory;
        }

        // PUT: api/ExamHistories/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamHistory(int id, ExamHistory examHistory)
        {
            if (id != examHistory.Id)
            {
                return BadRequest();
            }

            _context.Entry(examHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamHistoryExists(id))
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

        // POST: api/ExamHistories
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ExamHistory>> PostExamHistory(ExamHistory examHistory)
        {
            _context.ExamHistories.Add(examHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExamHistory", new { id = examHistory.Id }, examHistory);
        }
        [HttpPost]
        [Route("{accountId}")]
        public async Task<ActionResult> Submit(int accountId, List<QuestionsInPart> questionsInParts)
        {
            float Total = 0;

            for (int i = 0; i < questionsInParts.Count; i++)
            {
                ExamHistory history = new ExamHistory() { AccountId = accountId, QuestionsInPartId = questionsInParts[i].Id, Points = 0 };
                List<ExamHistoryDetail> examHistoryDetails = new List<ExamHistoryDetail>();

                var questionInDb = _context.Questions.Include(q => q.QuestionChoices).Include(q => q.QuestionSetting).Where(q => q.Id == questionsInParts[i].QuestionId).FirstOrDefault();
                var questionPartInDb = _context.QuestionsInParts.Include(q => q.ExamPart).Where(q => q.QuestionId == questionsInParts[i].QuestionId).FirstOrDefault();

                int count = 0;
                for (int j = 0; j < questionsInParts[i].Question.QuestionChoices.Count; j++)
                {
                    if (questionsInParts[i].Question.QuestionChoices.ElementAt(j).IsCorrect)
                    {
                        examHistoryDetails.Add(new ExamHistoryDetail() { Choice = questionsInParts[i].Question.QuestionChoices.ElementAt(j).Choice, ExamHistoryId = 0 }) ;
                        if (questionsInParts[i].Question.QuestionChoices.ElementAt(j).IsCorrect == questionInDb.QuestionChoices.ElementAt(j).IsCorrect)
                            count++;
                    }
                   
                }
                float S = 0;
                if (questionInDb.SelectionSettingId == 1 && count == 1)
                    S = questionPartInDb.ExamPart.QuestionPoints == 0 ? questionInDb.QuestionSetting.PointValue : questionPartInDb.ExamPart.QuestionPoints;
                else if (questionInDb.SelectionSettingId == 2 && count == 1)
                    S = questionPartInDb.ExamPart.QuestionPoints == 0 ? questionInDb.QuestionSetting.PointValue : questionPartInDb.ExamPart.QuestionPoints;
                else if (questionInDb.SelectionSettingId == 3 && count >= 1)
                {
                    float point = questionPartInDb.ExamPart.QuestionPoints == 0 ? questionInDb.QuestionSetting.PointValue : questionPartInDb.ExamPart.QuestionPoints;
                    float rights = questionInDb.QuestionChoices.Where(c => c.IsCorrect).Count();
                    S = (point / rights) * count;
                }
                history.Points = S;
                Total += S;

                _context.ExamHistories.Add(history);
                _context.SaveChanges();


                for (int j = 0; j < examHistoryDetails.Count; j++)
                {
                    examHistoryDetails.ElementAt(j).ExamHistoryId = history.Id;
                    examHistoryDetails.ElementAt(j).Id = 0;
                }

                _context.ExamHistoryDetails.AddRange(examHistoryDetails);

            }
            var part = _context.QuestionsInParts.Include(q => q.ExamPart).Where(q => q.QuestionId == questionsInParts[0].QuestionId).FirstOrDefault();
            ExamScore score = new ExamScore() { Id = 0, AccountId = accountId, ExamId = part.ExamPart.ExamId, Points = Total, OpenedTime = DateTime.Now, ClosedTime = DateTime.Now };
            _context.ExamScores.Add(score);
            _context.SaveChanges();

            return Content(Total.ToString());
        }

        // DELETE: api/ExamHistories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExamHistory>> DeleteExamHistory(int id)
        {
            var examHistory = await _context.ExamHistories.FindAsync(id);
            if (examHistory == null)
            {
                return NotFound();
            }

            _context.ExamHistories.Remove(examHistory);
            await _context.SaveChangesAsync();

            return examHistory;
        }

        private bool ExamHistoryExists(int id)
        {
            return _context.ExamHistories.Any(e => e.Id == id);
        }
    }
}