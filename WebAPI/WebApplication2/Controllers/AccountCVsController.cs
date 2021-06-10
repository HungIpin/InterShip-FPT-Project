using System;
using System.Collections.Generic;
using System.IO;
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
    public class AccountCVsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountCVsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AccountCVs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountCV>>> GetAccountCVs()
        {
            return await _context.AccountCVs.ToListAsync();
        }

        // GET: api/AccountCVs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountCV>> GetAccountCV(int id)
        {
            var accountCV = await _context.AccountCVs.FindAsync(id);

            if (accountCV == null)
            {
                return NotFound();
            }

            return accountCV;
        }

        // PUT: api/AccountCVs/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountCV(int id, [FromForm] AccountCV accountCV)
        {
            if (id != accountCV.AccountId)
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

                accountCV.Attachment = fileData;
            }
            _context.Entry(accountCV).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountCVExists(id))
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

        // POST: api/AccountCVs
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AccountCV>> PostAccountCV([FromForm]AccountCV accountCV)
        {
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var file = HttpContext.Request.Form.Files[0];

                byte[] fileData = null;

                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    fileData = binaryReader.ReadBytes((int)file.Length);
                }

                accountCV.Attachment = fileData;
            }
            _context.AccountCVs.Add(accountCV);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountCVExists(accountCV.AccountId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccountCV", new { id = accountCV.AccountId }, accountCV);
        }

        // DELETE: api/AccountCVs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccountCV>> DeleteAccountCV(int id)
        {
            var accountCV = await _context.AccountCVs.FindAsync(id);
            if (accountCV == null)
            {
                return NotFound();
            }

            _context.AccountCVs.Remove(accountCV);
            await _context.SaveChangesAsync();

            return accountCV;
        }

        private bool AccountCVExists(int id)
        {
            return _context.AccountCVs.Any(e => e.AccountId == id);
        }
    }
}