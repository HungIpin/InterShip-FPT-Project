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
    public class QuestionTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuestionTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/QuestionTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionType>>> GetQuestionTypes()
        {
            return await _context.QuestionTypes.ToListAsync();
        }

        // GET: api/QuestionTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionType>> GetQuestionType(int id)
        {
            var questionType = await _context.QuestionTypes.FindAsync(id);

            if (questionType == null)
            {
                return NotFound();
            }

            return questionType;
        }

        // PUT: api/QuestionTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionType(int id, QuestionType questionType)
        {
            if (id != questionType.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetQuestionType", new { id = questionType.Id }, questionType);
        }

        // POST: api/QuestionTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<QuestionType>> PostQuestionType(QuestionType questionType)
        {
            _context.QuestionTypes.Add(questionType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestionType", new { id = questionType.Id }, questionType);
        }

        // DELETE: api/QuestionTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuestionType>> DeleteQuestionType(int id)
        {
            var questionType = await _context.QuestionTypes.FindAsync(id);
            if (questionType == null)
            {
                return NotFound();
            }

            _context.QuestionTypes.Remove(questionType);
            await _context.SaveChangesAsync();

            return questionType;
        }

        private bool QuestionTypeExists(int id)
        {
            return _context.QuestionTypes.Any(e => e.Id == id);
        }
    }
}
