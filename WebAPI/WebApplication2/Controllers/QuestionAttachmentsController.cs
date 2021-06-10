using System;
using System.Collections.Generic;
using System.IO;
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
    public class QuestionAttachmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuestionAttachmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/QuestionAttachments
        [HttpGet]
        [ActionName("GetQuestionAttachments")]
        public async Task<ActionResult<IEnumerable<QuestionAttachment>>> GetQuestionAttachments()
        {
            return await _context.QuestionAttachment.Include(e => e.Question).ToListAsync();
        }
        [HttpGet]
        [ActionName("GetQuestionAttachmentsAsync")]
        [Route("Admin")]
        public async Task<ActionResult<QuestionAttachmentViewModel>> GetQuestionAttachmentsAsync()
        {
            QuestionAttachmentViewModel questionAttachmentViewModels = new QuestionAttachmentViewModel();
            questionAttachmentViewModels.QuestionAttachments = _context.QuestionAttachment.Include(e => e.Question).ToList();
            questionAttachmentViewModels.Questions = _context.Questions.Select(c => new Question { Id = c.Id}).ToList();
            return questionAttachmentViewModels;
        }


        // GET: api/QuestionAttachments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionAttachment>> GetQuestionAttachment(int id)
        {
            var questionAttachment = await _context.QuestionAttachment.FindAsync(id);

            if (questionAttachment == null)
            {
                return NotFound();
            }

            return questionAttachment;
        }

        // PUT: api/QuestionAttachments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionAttachment(int id,[FromForm] QuestionAttachment questionAttachment)
        {
            if (id != questionAttachment.Id)
            {
                return BadRequest();
            }
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var file = HttpContext.Request.Form.Files[0];

                byte[] fileData = null;

                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    fileData = binaryReader.ReadBytes((int)file.Length);
                }

                questionAttachment.Attachment = fileData;
            }
            _context.Entry(questionAttachment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionAttachmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return RedirectToAction("GetQuestionAttachments", "QuestionAttachments");
                }
            }

            return RedirectToAction("GetQuestionAttachments", "QuestionAttachments");
        }

        // POST: api/QuestionAttachments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult> PostQuestionAttachment([FromForm] QuestionAttachment questionAttachment)
        {
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var file = HttpContext.Request.Form.Files[0];

                byte[] fileData = null;

                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    fileData = binaryReader.ReadBytes((int)file.Length);
                }

                questionAttachment.Attachment = fileData;
            }
            _context.QuestionAttachment.Add(questionAttachment);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (QuestionAttachmentExists(questionAttachment.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }


            return RedirectToAction("GetQuestionAttachments", "QuestionAttachments");
            //return CreatedAtAction("GetQuestionAttachment", new { id = questionAttachment.Id }, questionAttachment);
        }

        // DELETE: api/QuestionAttachments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuestionAttachment>> DeleteQuestionAttachment(int id)
        {
            var questionAttachment = await _context.QuestionAttachment.FindAsync(id);
            if (questionAttachment == null)
            {
                return NotFound();
            }

            _context.QuestionAttachment.Remove(questionAttachment);
            await _context.SaveChangesAsync();

            return questionAttachment;
        }

        private bool QuestionAttachmentExists(int id)
        {
            return _context.QuestionAttachment.Any(e => e.Id == id);
        }
    }
}
