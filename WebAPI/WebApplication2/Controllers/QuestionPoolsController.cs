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
    public class QuestionPoolsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuestionPoolsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Recent")]
        public async Task<ActionResult<IEnumerable<QuestionPool>>> GetRecentExams()
        {
            var list = await _context.QuestionPools.OrderByDescending(e => e.CreatedDate).Take(4).ToListAsync();
            for (int i = 0; i < list.Count; i++)
                list[i].Account = _context.Accounts.Where(a => a.Id == list[i].AccountId).Select(a => new Account() { Username = a.Username }).First();
            return list;
        }
        // GET: api/QuestionPools
        [HttpGet]
        [ActionName("GetQuestionPools")]
        public async Task<ActionResult<IEnumerable<QuestionPool>>> GetQuestionPools()
        {
            return await _context.QuestionPools.Include(e => e.Account).ToListAsync();
        }
        [HttpGet]
        [ActionName("GetQuestionPoolsAsync")]
        [Route("Admin")]
        public async Task<ActionResult<QuestionPoolViewModel>> GetQuestionPoolsAsync()
        {
            QuestionPoolViewModel questionPoolViewModel = new QuestionPoolViewModel();
            questionPoolViewModel.QuestionPools = _context.QuestionPools.Include(e => e.Account).ToList();
            questionPoolViewModel.Accounts = _context.Accounts.Select(c => new Account { Id = c.Id }).ToList();
            questionPoolViewModel.QuestionPoolParents = _context.QuestionPools.Select(c => new QuestionPool { Id = c.Id }).ToList();
            return questionPoolViewModel;
        }

        // GET: api/QuestionPools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionPool>> GetQuestionPool(int id)
        {
            var questionPool = await _context.QuestionPools.FindAsync(id);

            if (questionPool == null)
            {
                return NotFound();
            }

            return questionPool;
        }

        // PUT: api/QuestionPools/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionPool(int id, QuestionPool questionPool)
        {
            if (id != questionPool.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionPool).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionPoolExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return RedirectToAction("GetQuestionPools", "QuestionPools");
                }
            }

            return RedirectToAction("GetQuestionPools", "QuestionPools");
        }

        // POST: api/QuestionPools
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<QuestionPool>> PostQuestionPool(QuestionPool questionPool)
        {
            questionPool.CreatedDate = DateTime.Now;
            _context.QuestionPools.Add(questionPool);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (QuestionPoolExists(questionPool.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("GetQuestionPools", "QuestionPools");
            //return CreatedAtAction("GetQuestionPool", new { id = questionPool.Id }, questionPool);
        }

        // DELETE: api/QuestionPools/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuestionPool>> DeleteQuestionPool(int id)
        {
            var questionPool = await _context.QuestionPools.FindAsync(id);
            if (questionPool == null)
            {
                return NotFound();
            }

            _context.QuestionPools.Remove(questionPool);
            await _context.SaveChangesAsync();

            return questionPool;
        }

        private bool QuestionPoolExists(int id)
        {
            return _context.QuestionPools.Any(e => e.Id == id);
        }
    }
}
