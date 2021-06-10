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
    public class AccountCertificationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountCertificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AccountCertifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountCertification>>> GetAccountCertifications()
        {
            return await _context.AccountCertifications.ToListAsync();
        }

        // GET: api/AccountCertifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountCertification>> GetAccountCertification(int id)
        {
            var accountCertification = await _context.AccountCertifications.FindAsync(id);

            if (accountCertification == null)
            {
                return NotFound();
            }

            return accountCertification;
        }

        // PUT: api/AccountCertifications/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountCertification(int id, AccountCertification accountCertification)
        {
            if (id != accountCertification.AccountId)
            {
                return BadRequest();
            }

            _context.Entry(accountCertification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountCertificationExists(id))
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

        // POST: api/AccountCertifications
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AccountCertification>> PostAccountCertification(AccountCertification accountCertification)
        {
            _context.AccountCertifications.Add(accountCertification);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountCertificationExists(accountCertification.AccountId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccountCertification", new { id = accountCertification.AccountId }, accountCertification);
        }

        // DELETE: api/AccountCertifications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccountCertification>> DeleteAccountCertification(int id)
        {
            var accountCertification = await _context.AccountCertifications.FindAsync(id);
            if (accountCertification == null)
            {
                return NotFound();
            }

            _context.AccountCertifications.Remove(accountCertification);
            await _context.SaveChangesAsync();

            return accountCertification;
        }

        private bool AccountCertificationExists(int id)
        {
            return _context.AccountCertifications.Any(e => e.AccountId == id);
        }
    }
}