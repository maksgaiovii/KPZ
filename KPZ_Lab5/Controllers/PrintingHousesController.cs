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
    public class PrintingHousesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrintingHousesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PrintingHouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrintingHouse>>> GetPrintingHouses()
        {
            return await _context.PrintingHouses.ToListAsync();
        }

        // GET: api/PrintingHouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrintingHouse>> GetPrintingHouse(int id)
        {
            var printingHouse = await _context.PrintingHouses.FindAsync(id);

            if (printingHouse == null)
            {
                return NotFound();
            }

            return printingHouse;
        }

        // PUT: api/PrintingHouses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrintingHouse(int id, PrintingHouse printingHouse)
        {
            if (id != printingHouse.PrintingHouseId)
            {
                return BadRequest();
            }

            _context.Entry(printingHouse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrintingHouseExists(id))
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

        // POST: api/PrintingHouses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PrintingHouse>> PostPrintingHouse(PrintingHouse printingHouse)
        {
            _context.PrintingHouses.Add(printingHouse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrintingHouse", new { id = printingHouse.PrintingHouseId }, printingHouse);
        }

        // DELETE: api/PrintingHouses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrintingHouse(int id)
        {
            var printingHouse = await _context.PrintingHouses.FindAsync(id);
            if (printingHouse == null)
            {
                return NotFound();
            }

            _context.PrintingHouses.Remove(printingHouse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrintingHouseExists(int id)
        {
            return _context.PrintingHouses.Any(e => e.PrintingHouseId == id);
        }
    }
}
