using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExamsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Counts")]
        public async Task<ActionResult> GetNumOfExams()
        {
            var list = await _context.Exams.ToListAsync();
            return Content(list.Count.ToString());
        }
        [HttpGet]
        [Route("Recent")]
        public async Task<ActionResult<IEnumerable<Exam>>> GetRecentExams()
        {
            var list = await _context.Exams.OrderByDescending(e => e.CreatedDate).Take(4).ToListAsync();
            for (int i = 0; i < list.Count; i++)
                list[i].Account = _context.Accounts.Where(a => a.Id == list[i].AccountId).Select(a => new Account() { Username = a.Username }).First();
            return list;
        }
        [HttpGet]
        [Route("Top")]
        public async Task<ActionResult<IEnumerable<Exam>>> GetTop5Exams()
        {
            return await _context.Exams.OrderByDescending(e => e.Rating).Take(5).ToListAsync();
        }

        [HttpGet]
        [Route("New")]
        public async Task<ActionResult<IEnumerable<Exam>>> GetNew5Exams()
        {
            return await _context.Exams.OrderByDescending(e => e.CreatedDate).Take(5).ToListAsync();
        }

        // GET: api/Exams
        [HttpGet]
        [ActionName("GetExams")]
        public async Task<ActionResult<IEnumerable<Exam>>> GetExams()
        {
            return await _context.Exams.Include(e => e.Certification).ToListAsync();  //Quỳnh sửa
            //return await _context.Exams.Include(e => e.Certification).Include(e => e.FeedbackType).Include(e => e.FeedbackLevel).Include(e => e.Account).Include(e => e.ScoreRecording).ToListAsync();
        }
        [HttpGet]
        [ActionName("GetExamsAsync")]
        [Route("Admin")]
        public async Task<ActionResult<ExamViewModel>> GetExamsAsyn()
        {
            ExamViewModel exams = new ExamViewModel();
            exams.Exams = _context.Exams.Include(e => e.Certification).Include(e => e.FeedbackType).Include(e => e.FeedbackLevel).Include(e => e.Account).Include(e => e.ScoreRecording).ToList();
            exams.Certifications = _context.Certifications.Select(c => new Certification { Id = c.Id, Name = c.Name }).ToList();
            exams.FeedbackTypes = _context.FeedbackTypes.Select(c => new FeedbackType { Id = c.Id }).ToList();
            exams.FeedbackLevels = _context.FeedbackLevels.Select(c => new FeedbackLevel { Id = c.Id }).ToList();
            exams.Accounts = _context.Accounts.Select(c => new Account { Id = c.Id }).ToList();
            exams.ScoreRecordings = _context.ScoreRecordings.Select(c => new ScoreRecording { Id = c.Id }).ToList();
            return exams;
        }

        // GET: api/Exams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetExam(string id)
        {
            var exam = await _context.Exams.FindAsync(id);

            if (exam == null)
            {
                return NotFound();
            }

            return exam;
        }

        // PUT: api/Exams/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExam(string id, Exam exam)
        {
            if (id != exam.Id)
            {
                return BadRequest();
            }

            _context.Entry(exam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return RedirectToAction("GetExams", "Exams");
                }
            }

            return RedirectToAction("GetExams", "Exams");
        }

        // POST: api/Exams
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Exam>> PostExam(Exam exam)
        {
            _context.Exams.Add(exam);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExamExists(exam.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("GetExams", "Exams");
        }

        // DELETE: api/Exams/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Exam>> DeleteExam(string id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();

            return exam;
        }

        // GET: api/ExamSettings/examid
        [HttpGet("{examid}")]
        public async Task<ActionResult<ExamSetting>> GetExamSettingByExamId(string examid)
        {
            var examSetting = await _context.ExamSettings.FirstAsync(a => a.ExamId == examid);

            if (examSetting == null)
            {
                return Content("false");
            }

            return Content("true");
        }
        private bool ExamExists(string id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }

        // GET: api/Exams/GetExamTest/
        [HttpGet]
        [Route("GetExamTest/{id}")]
        public async Task<ActionResult<Object>> GetExamTest(string id)
        {
            //var question = await _context.Questions.Include(q => q.QuestionPool).ToListAsync();
            //var questionPools = await _context.ExamParts.Include(q => q.QuestionPool).Include(e => e.Exam).Where(q => q.ExamId == id).FirstOrDefaultAsync();

            //var exam = await _context.Exams.Where(q => q.Id == id).FirstOrDefaultAsync();
            //var questioninpart = await _context.QuestionsInParts.Include(q => q.ExamPart).Include(a => a.Question).ToListAsync();

            //var questiona = await _context.Questions.Include(a => a.QuestionsInParts).Include(a => a.QuestionChoices).Include(a => a.QuestionSetting).Include(a => a.QuestionType).ToListAsync();
            //var exama = await _context.Exams.Include(a => a.ExamParts)
            //                                .ThenInclude(a => a.QuestionsInParts.Select(c => new QuestionsInPart { Id = c.Id, Question = questiona }))
            //                                .FirstAsync(c => c.Id == id);

            var question = _context.Questions.Include(m => m.QuestionChoices).Include(m => m.QuestionSetting).ToList();

            for (int i = 0; i < question.Count; i++)
            {
                for (int j = 0; j < question[i].QuestionChoices.Count; j++)
                    question[i].QuestionChoices.ElementAt(j).IsCorrect = false;
            }
            var examquestion = _context.ExamParts
                .Include(m => m.QuestionsInParts)
                .ThenInclude(m => question)
                .ToList();

            var exam = _context.Exams.Include(a => examquestion).Where(q => q.Id == id).FirstOrDefault();
            
            return exam;
        }
        [HttpGet]
        [Route("Ratings")]
        public async Task<ActionResult<IEnumerable<ExamRating>>> GetExamRatings()
        {
            List<ExamRating> examRatings = new List<ExamRating>();
            for (int i = 1; i <= 5; i++)
            {
                ExamRating exam = new ExamRating() { Rating = i, Total = await _context.Exams.Where(e => e.Rating == i).CountAsync() };
                examRatings.Add(exam);
            }
            return examRatings;
        }
        [HttpGet]

        [Route("Getexamincertificate/{id}")]
        public async Task<ActionResult<IEnumerable<Exam>>> Getexamincertificate(string id) //goi ra list
        {
            var exam = await _context.Exams.Where(a => a.CertificationId == id).ToListAsync();

            if (exam == null)
            {
                return NotFound();
            }

            return exam;
        }
        [HttpGet]
        [Route("GetExamTestAdmin/{id}")]
        public async Task<ActionResult<Object>> GetExamTestAdmin(string id)
        {
            //var question = await _context.Questions.Include(q => q.QuestionPool).ToListAsync();
            //var questionPools = await _context.ExamParts.Include(q => q.QuestionPool).Include(e => e.Exam).Where(q => q.ExamId == id).FirstOrDefaultAsync();

            //var exam = await _context.Exams.Where(q => q.Id == id).FirstOrDefaultAsync();
            //var questioninpart = await _context.QuestionsInParts.Include(q => q.ExamPart).Include(a => a.Question).ToListAsync();

            //var questiona = await _context.Questions.Include(a => a.QuestionsInParts).Include(a => a.QuestionChoices).Include(a => a.QuestionSetting).Include(a => a.QuestionType).ToListAsync();
            //var exama = await _context.Exams.Include(a => a.ExamParts)
            //                                .ThenInclude(a => a.QuestionsInParts.Select(c => new QuestionsInPart { Id = c.Id, Question = questiona }))
            //                                .FirstAsync(c => c.Id == id);

            var question = _context.Questions.Include(m => m.QuestionChoices).Include(m => m.QuestionSetting).ToList();
            var examquestion = _context.ExamParts
                .Include(m => m.QuestionsInParts)
                .ThenInclude(m => question)
                .ToList();

            var exam = _context.Exams.Include(a => examquestion).Where(q => q.Id == id).FirstOrDefault();

            return exam;
        }
    }
}