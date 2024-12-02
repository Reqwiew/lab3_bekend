using lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace lab1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting("fixedWindow")]
    public class TarrifsController : Controller
    {
        private DataContext _dataContext;

        public TarrifsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public IAsyncEnumerable<Tariffs> GetTariffs()
        {
            return _dataContext.Tariffses.AsAsyncEnumerable();
        }

        [HttpGet("{id}")]
        [DisableRateLimiting]
        public async Task<IActionResult> GetTariffs(long id) 
        {
            Tariffs? tar = await _dataContext.Tariffses.FindAsync(id);
            if(tar == null) return NotFound();
            return Ok(tar);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(long id, [FromBody] Tariffs tar)
        { 
            var existingTariffs = await _dataContext.Tariffses.FindAsync(id);
            if (existingTariffs == null)
            { 
                return NotFound();
            }
            try
            {
                existingTariffs.ProductName = existingTariffs.ProductName;
                existingTariffs.CostTariff = existingTariffs.CostTariff;
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            {
                return Conflict(new { message = "The record was updated or deleted by another user." });
            }
            return Ok(existingTariffs);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(long id)
        { 
            var tar = await _dataContext.Tariffses.FindAsync(id);
            if (tar ==null) return NotFound();

            _dataContext.Tariffses.Remove(tar);
            await _dataContext.SaveChangesAsync();

            return NoContent();



        }

    }
}
