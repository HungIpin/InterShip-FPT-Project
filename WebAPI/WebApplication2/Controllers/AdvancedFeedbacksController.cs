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
    public class AdvancedFeedbacksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdvancedFeedbacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AdvancedFeedbacks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdvancedFeedback>>> GetAdvancedFeedbacks()
        {
            return await _context.AdvancedFeedbacks.ToListAsync();
        }

        // GET: api/AdvancedFeedbacks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdvancedFeedback>> GetAdvancedFeedback(int id)
        {
            var advancedFeedback = await _context.AdvancedFeedbacks.FindAsync(id);

            if (advancedFeedback == null)
            {
                return NotFound();
            }

            return advancedFeedback;
        }

        // PUT: api/AdvancedFeedbacks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdvancedFeedback(int id, AdvancedFeedback advancedFeedback)
        {
            if (id != advancedFeedback.Id)
            {
                return BadRequest();
            }

            _context.Entry(advancedFeedback).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvancedFeedbackExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAdvancedFeedback", new { id = advancedFeedback.Id }, advancedFeedback);
        }

        // POST: api/AdvancedFeedbacks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AdvancedFeedback>> PostAdvancedFeedback(AdvancedFeedback advancedFeedback)
        {
            _context.AdvancedFeedbacks.Add(advancedFeedback);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdvancedFeedback", new { id = advancedFeedback.Id }, advancedFeedback);
        }

        // DELETE: api/AdvancedFeedbacks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AdvancedFeedback>> DeleteAdvancedFeedback(int id)
        {
            var advancedFeedback = await _context.AdvancedFeedbacks.FindAsync(id);
            if (advancedFeedback == null)
            {
                return NotFound();
            }

            _context.AdvancedFeedbacks.Remove(advancedFeedback);
            await _context.SaveChangesAsync();

            return advancedFeedback;
        }

        private bool AdvancedFeedbackExists(int id)
        {
            return _context.AdvancedFeedbacks.Any(e => e.Id == id);
        }
    }
}
