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
    public class FeedbackLevelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FeedbackLevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FeedbackLevels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedbackLevel>>> GetFeedbackLevels()
        {
            return await _context.FeedbackLevels.ToListAsync();
        }

        // GET: api/FeedbackLevels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackLevel>> GetFeedbackLevel(int id)
        {
            var feedbackLevel = await _context.FeedbackLevels.FindAsync(id);

            if (feedbackLevel == null)
            {
                return NotFound();
            }

            return feedbackLevel;
        }

        // PUT: api/FeedbackLevels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedbackLevel(int id, FeedbackLevel feedbackLevel)
        {
            if (id != feedbackLevel.Id)
            {
                return BadRequest();
            }

            _context.Entry(feedbackLevel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackLevelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFeedbackLevel", new { id = feedbackLevel.Id }, feedbackLevel);
        }

        // POST: api/FeedbackLevels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<FeedbackLevel>> PostFeedbackLevel(FeedbackLevel feedbackLevel)
        {
            _context.FeedbackLevels.Add(feedbackLevel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedbackLevel", new { id = feedbackLevel.Id }, feedbackLevel);
        }

        // DELETE: api/FeedbackLevels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FeedbackLevel>> DeleteFeedbackLevel(int id)
        {
            var feedbackLevel = await _context.FeedbackLevels.FindAsync(id);
            if (feedbackLevel == null)
            {
                return NotFound();
            }

            _context.FeedbackLevels.Remove(feedbackLevel);
            await _context.SaveChangesAsync();

            return feedbackLevel;
        }

        private bool FeedbackLevelExists(int id)
        {
            return _context.FeedbackLevels.Any(e => e.Id == id);
        }
    }
}
