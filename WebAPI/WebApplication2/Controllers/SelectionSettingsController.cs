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
    public class SelectionSettingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SelectionSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SelectionSettings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SelectionSetting>>> GetSelectionSettings()
        {
            return await _context.SelectionSettings.ToListAsync();
        }

        // GET: api/SelectionSettings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SelectionSetting>> GetSelectionSetting(int id)
        {
            var selectionSetting = await _context.SelectionSettings.FindAsync(id);

            if (selectionSetting == null)
            {
                return NotFound();
            }

            return selectionSetting;
        }

        // PUT: api/SelectionSettings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSelectionSetting(int id, SelectionSetting selectionSetting)
        {
            if (id != selectionSetting.Id)
            {
                return BadRequest();
            }

            _context.Entry(selectionSetting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SelectionSettingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSelectionSetting", new { id = selectionSetting.Id }, selectionSetting);
        }

        // POST: api/SelectionSettings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SelectionSetting>> PostSelectionSetting(SelectionSetting selectionSetting)
        {
            _context.SelectionSettings.Add(selectionSetting);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSelectionSetting", new { id = selectionSetting.Id }, selectionSetting);
        }

        // DELETE: api/SelectionSettings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SelectionSetting>> DeleteSelectionSetting(int id)
        {
            var selectionSetting = await _context.SelectionSettings.FindAsync(id);
            if (selectionSetting == null)
            {
                return NotFound();
            }

            _context.SelectionSettings.Remove(selectionSetting);
            await _context.SaveChangesAsync();

            return selectionSetting;
        }

        private bool SelectionSettingExists(int id)
        {
            return _context.SelectionSettings.Any(e => e.Id == id);
        }
    }
}
