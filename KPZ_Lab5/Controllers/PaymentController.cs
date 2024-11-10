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
    public class PaymentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PaymentController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentViewModel>>> GetPayments()
        {
            var payments = await _context.Payments.ToListAsync();
            return _mapper.Map<List<PaymentViewModel>>(payments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentViewModel>> GetPayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return NotFound();
            return _mapper.Map<PaymentViewModel>(payment);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentViewModel>> CreatePayment(PaymentViewModel paymentViewModel)
        {
            var payment = _mapper.Map<Payment>(paymentViewModel);
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPayment), new { id = payment.PaymentId }, _mapper.Map<PaymentViewModel>(payment));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, PaymentViewModel paymentViewModel)
        {
            if (id != paymentViewModel.Id) return BadRequest();
            var payment = _mapper.Map<Payment>(paymentViewModel);
            payment.PaymentId = id;
            _context.Entry(payment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return NotFound();
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
