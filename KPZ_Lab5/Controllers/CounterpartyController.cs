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
    public class CounterpartyController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CounterpartyController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CounterpartyViewModel>>> GetCounterparties()
        {
            var counterparties = await _context.Counterparties.ToListAsync();
            return _mapper.Map<List<CounterpartyViewModel>>(counterparties);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CounterpartyViewModel>> GetCounterparty(string id)
        {
            var counterparty = await _context.Counterparties.FindAsync(id);
            if (counterparty == null) return NotFound();
            return _mapper.Map<CounterpartyViewModel>(counterparty);
        }

        [HttpPost]
        public async Task<ActionResult<CounterpartyViewModel>> CreateCounterparty(CounterpartyViewModel counterpartyViewModel)
        {
            var counterparty = _mapper.Map<Counterparty>(counterpartyViewModel);
            _context.Counterparties.Add(counterparty);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCounterparty), new { id = counterparty.TaxId }, _mapper.Map<CounterpartyViewModel>(counterparty));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCounterparty(string id, CounterpartyViewModel counterpartyViewModel)
        {
            if (id != counterpartyViewModel.Id) return BadRequest();
            var counterparty = _mapper.Map<Counterparty>(counterpartyViewModel);
            counterparty.TaxId = id;
            _context.Entry(counterparty).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCounterparty(string id)
        {
            var counterparty = await _context.Counterparties.FindAsync(id);
            if (counterparty == null) return NotFound();
            _context.Counterparties.Remove(counterparty);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
