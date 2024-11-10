using AutoMapper;
using KPZ_lab5.Data;
using KPZ_lab5.Models;
using KPZ_lab5.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KPZ_lab5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceCategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceCategoryController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceCategoryViewModel>>> GetInvoiceCategories()
        {
            var categories = await _context.InvoiceCategories.ToListAsync();
            return _mapper.Map<List<InvoiceCategoryViewModel>>(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceCategoryViewModel>> GetInvoiceCategory(int id)
        {
            var category = await _context.InvoiceCategories.FindAsync(id);
            if (category == null) return NotFound();
            return _mapper.Map<InvoiceCategoryViewModel>(category);
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceCategoryViewModel>> CreateInvoiceCategory(InvoiceCategoryViewModel categoryViewModel)
        {
            var category = _mapper.Map<InvoiceCategory>(categoryViewModel);
            _context.InvoiceCategories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetInvoiceCategory), new { id = category.InvoiceCategoryId }, _mapper.Map<InvoiceCategoryViewModel>(category));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoiceCategory(int id, InvoiceCategoryViewModel categoryViewModel)
        {
            if (id != categoryViewModel.Id) return BadRequest();
            var category = _mapper.Map<InvoiceCategory>(categoryViewModel);
            category.InvoiceCategoryId = id;
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceCategory(int id)
        {
            var category = await _context.InvoiceCategories.FindAsync(id);
            if (category == null) return NotFound();
            _context.InvoiceCategories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}