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
    public class CounterpartyController(DbLabsContext context, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CounterpartyViewModel>>> GetCounterparties()
        {
            var counterparties = await context.Counterparties.ToListAsync();
            return mapper.Map<List<CounterpartyViewModel>>(counterparties);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CounterpartyViewModel>> GetCounterparty(string id)
        {
            var counterparty = await context.Counterparties.FindAsync(id);
            if (counterparty == null) return NotFound();
            return mapper.Map<CounterpartyViewModel>(counterparty);
        }

        [HttpPost]
        public async Task<ActionResult<CounterpartyViewModel>> CreateCounterparty(CounterpartyViewModel counterpartyViewModel)
        {
            var counterparty = mapper.Map<Counterparty>(counterpartyViewModel);
            context.Counterparties.Add(counterparty);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCounterparty), new { id = counterparty.TaxId }, mapper.Map<CounterpartyViewModel>(counterparty));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCounterparty(string id, CounterpartyViewModel counterpartyViewModel)
        {
            if (id != counterpartyViewModel.Id) return BadRequest();
            var counterparty = mapper.Map<Counterparty>(counterpartyViewModel);
            counterparty.TaxId = id;
            context.Entry(counterparty).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCounterparty(string id)
        {
            var counterparty = await context.Counterparties.FindAsync(id);
            if (counterparty == null) return NotFound();
            context.Counterparties.Remove(counterparty);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
