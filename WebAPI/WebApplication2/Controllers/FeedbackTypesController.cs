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
    public class FeedbackTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FeedbackTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FeedbackTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedbackType>>> GetFeedbackTypes()
        {
            return await _context.FeedbackTypes.ToListAsync();
        }

        // GET: api/FeedbackTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackType>> GetFeedbackType(int id)
        {
            var feedbackType = await _context.FeedbackTypes.FindAsync(id);

            if (feedbackType == null)
            {
                return NotFound();
            }

            return feedbackType;
        }

        // PUT: api/FeedbackTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedbackType(int id, FeedbackType feedbackType)
        {
            if (id != feedbackType.Id)
            {
                return BadRequest();
            }

            _context.Entry(feedbackType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFeedbackType", new { id = feedbackType.Id }, feedbackType);
        }

        // POST: api/FeedbackTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<FeedbackType>> PostFeedbackType(FeedbackType feedbackType)
        {
            _context.FeedbackTypes.Add(feedbackType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedbackType", new { id = feedbackType.Id }, feedbackType);
        }

        // DELETE: api/FeedbackTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FeedbackType>> DeleteFeedbackType(int id)
        {
            var feedbackType = await _context.FeedbackTypes.FindAsync(id);
            if (feedbackType == null)
            {
                return NotFound();
            }

            _context.FeedbackTypes.Remove(feedbackType);
            await _context.SaveChangesAsync();

            return feedbackType;
        }

        private bool FeedbackTypeExists(int id)
        {
            return _context.FeedbackTypes.Any(e => e.Id == id);
        }
    }
}
