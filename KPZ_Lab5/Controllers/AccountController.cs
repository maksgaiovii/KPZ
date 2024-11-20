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
    public class AccountController : ControllerBase
    {
        private readonly DbLabsContext _context;
        private readonly IMapper _mapper;

        public AccountController(DbLabsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountViewModel>>> GetAccounts()
        {
            var accounts = await _context.Accounts.ToListAsync();
            return _mapper.Map<List<AccountViewModel>>(accounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountViewModel>> GetAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return NotFound();
            return _mapper.Map<AccountViewModel>(account);
        }

        [HttpPost]
        public async Task<ActionResult<AccountViewModel>> CreateAccount(AccountViewModel accountViewModel)
        {
            return BadRequest("{}");
            var account = _mapper.Map<Account>(accountViewModel);
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAccount), new { id = account.AccountId }, _mapper.Map<AccountViewModel>(account));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, AccountViewModel accountViewModel)
        {
            if (id != accountViewModel.Id) return BadRequest();
            var account = _mapper.Map<Account>(accountViewModel);
            account.AccountId = id;
            _context.Entry(account).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return NotFound();
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
