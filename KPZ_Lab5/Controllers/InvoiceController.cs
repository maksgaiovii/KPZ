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
    public class InvoiceController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceViewModel>>> GetInvoices()
        {
            var invoices = await _context.Invoices.ToListAsync();
            return _mapper.Map<List<InvoiceViewModel>>(invoices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceViewModel>> GetInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null) return NotFound();
            return _mapper.Map<InvoiceViewModel>(invoice);
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceViewModel>> CreateInvoice(InvoiceViewModel invoiceViewModel)
        {
            var invoice = _mapper.Map<Invoice>(invoiceViewModel);
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetInvoice), new { id = invoice.InvoiceId }, _mapper.Map<InvoiceViewModel>(invoice));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, InvoiceViewModel invoiceViewModel)
        {
            if (id != invoiceViewModel.Id) return BadRequest();
            var invoice = _mapper.Map<Invoice>(invoiceViewModel);
            invoice.InvoiceId = id;
            _context.Entry(invoice).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null) return NotFound();
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
