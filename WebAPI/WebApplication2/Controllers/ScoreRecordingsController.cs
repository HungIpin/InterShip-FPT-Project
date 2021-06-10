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
    public class ScoreRecordingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ScoreRecordingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ScoreRecordings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScoreRecording>>> GetScoreRecordings()
        {
            return await _context.ScoreRecordings.ToListAsync();
        }

        // GET: api/ScoreRecordings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScoreRecording>> GetScoreRecording(int id)
        {
            var scoreRecording = await _context.ScoreRecordings.FindAsync(id);

            if (scoreRecording == null)
            {
                return NotFound();
            }

            return scoreRecording;
        }

        // PUT: api/ScoreRecordings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScoreRecording(int id, ScoreRecording scoreRecording)
        {
            if (id != scoreRecording.Id)
            {
                return BadRequest();
            }

            _context.Entry(scoreRecording).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreRecordingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetScoreRecording", new { id = scoreRecording.Id }, scoreRecording);
        }

        // POST: api/ScoreRecordings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ScoreRecording>> PostScoreRecording(ScoreRecording scoreRecording)
        {
            _context.ScoreRecordings.Add(scoreRecording);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScoreRecording", new { id = scoreRecording.Id }, scoreRecording);
        }

        // DELETE: api/ScoreRecordings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ScoreRecording>> DeleteScoreRecording(int id)
        {
            var scoreRecording = await _context.ScoreRecordings.FindAsync(id);
            if (scoreRecording == null)
            {
                return NotFound();
            }

            _context.ScoreRecordings.Remove(scoreRecording);
            await _context.SaveChangesAsync();

            return scoreRecording;
        }

        private bool ScoreRecordingExists(int id)
        {
            return _context.ScoreRecordings.Any(e => e.Id == id);
        }
    }
}
