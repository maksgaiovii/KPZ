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
    public class TextBooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TextBooksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TextBooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TextBook>>> GetTextBooks()
        {
            return await _context.TextBooks.ToListAsync();
        }

        // GET: api/TextBooks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TextBook>> GetTextBook(int id)
        {
            var textBook = await _context.TextBooks.FindAsync(id);

            if (textBook == null)
            {
                return NotFound();
            }

            return textBook;
        }

        // PUT: api/TextBooks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTextBook(int id, TextBook textBook)
        {
            if (id != textBook.BookId)
            {
                return BadRequest();
            }

            _context.Entry(textBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TextBookExists(id))
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

        // POST: api/TextBooks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TextBook>> PostTextBook(TextBook textBook)
        {
            _context.TextBooks.Add(textBook);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TextBookExists(textBook.BookId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTextBook", new { id = textBook.BookId }, textBook);
        }

        // DELETE: api/TextBooks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTextBook(int id)
        {
            var textBook = await _context.TextBooks.FindAsync(id);
            if (textBook == null)
            {
                return NotFound();
            }

            _context.TextBooks.Remove(textBook);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TextBookExists(int id)
        {
            return _context.TextBooks.Any(e => e.BookId == id);
        }
    }
}
