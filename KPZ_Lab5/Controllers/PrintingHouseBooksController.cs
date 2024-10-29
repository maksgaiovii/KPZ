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
    public class PrintingHouseBooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrintingHouseBooksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PrintingHouseBooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrintingHouseBook>>> GetPrintingHouseBooks()
        {
            return await _context.PrintingHouseBooks.ToListAsync();
        }

        // GET: api/PrintingHouseBooks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrintingHouseBook>> GetPrintingHouseBook(int id)
        {
            var printingHouseBook = await _context.PrintingHouseBooks.FindAsync(id);

            if (printingHouseBook == null)
            {
                return NotFound();
            }

            return printingHouseBook;
        }

        // PUT: api/PrintingHouseBooks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrintingHouseBook(int id, PrintingHouseBook printingHouseBook)
        {
            if (id != printingHouseBook.BookId)
            {
                return BadRequest();
            }

            _context.Entry(printingHouseBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrintingHouseBookExists(id))
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

        // POST: api/PrintingHouseBooks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PrintingHouseBook>> PostPrintingHouseBook(PrintingHouseBook printingHouseBook)
        {
            _context.PrintingHouseBooks.Add(printingHouseBook);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PrintingHouseBookExists(printingHouseBook.BookId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPrintingHouseBook", new { id = printingHouseBook.BookId }, printingHouseBook);
        }

        // DELETE: api/PrintingHouseBooks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrintingHouseBook(int id)
        {
            var printingHouseBook = await _context.PrintingHouseBooks.FindAsync(id);
            if (printingHouseBook == null)
            {
                return NotFound();
            }

            _context.PrintingHouseBooks.Remove(printingHouseBook);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrintingHouseBookExists(int id)
        {
            return _context.PrintingHouseBooks.Any(e => e.BookId == id);
        }
    }
}
