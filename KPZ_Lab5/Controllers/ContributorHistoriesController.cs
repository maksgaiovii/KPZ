using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KPZ_lab5.Data;
using KPZ_lab5.Models;

namespace KPZ_lab5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContributorHistoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContributorHistoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ContributorHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContributorHistory>>> GetContributorHistories()
        {
            return await _context.ContributorHistories.ToListAsync();
        }

        // GET: api/ContributorHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContributorHistory>> GetContributorHistory(int id)
        {
            var contributorHistory = await _context.ContributorHistories.FindAsync(id);

            if (contributorHistory == null)
            {
                return NotFound();
            }

            return contributorHistory;
        }

        // PUT: api/ContributorHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContributorHistory(int id, ContributorHistory contributorHistory)
        {
            if (id != contributorHistory.Id)
            {
                return BadRequest();
            }

            _context.Entry(contributorHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContributorHistoryExists(id))
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

        // POST: api/ContributorHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContributorHistory>> PostContributorHistory(ContributorHistory contributorHistory)
        {
            _context.ContributorHistories.Add(contributorHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContributorHistory", new { id = contributorHistory.Id }, contributorHistory);
        }

        // DELETE: api/ContributorHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContributorHistory(int id)
        {
            var contributorHistory = await _context.ContributorHistories.FindAsync(id);
            if (contributorHistory == null)
            {
                return NotFound();
            }

            _context.ContributorHistories.Remove(contributorHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContributorHistoryExists(int id)
        {
            return _context.ContributorHistories.Any(e => e.Id == id);
        }
    }
}
