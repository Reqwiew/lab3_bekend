using lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace lab1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting("fixedWindow")]
    public class EntranceController : Controller
    {

        private DataContext _dataContext;
        public EntranceController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


       
        [HttpGet("report")]
        public async Task<IActionResult> GetProductReport()
        {
           
            var entrances = await _dataContext.entrances.ToListAsync();

           
            var report = entrances.Select(e => new
            {
                e.ProductName,
                e.BarchSize,  
                e.BarchPrice,
                TotalCost = e.BarchSize * e.BarchPrice 
            }).ToList();

            return Ok(report);
        }

       
        [HttpGet("check/{productName}")]
        public async Task<IActionResult> CheckProductAvailability(string productName, int quantity)
        {
            var product = await _dataContext.entrances
                .FirstOrDefaultAsync(p => p.ProductName.Equals(productName, StringComparison.OrdinalIgnoreCase));

            if (product == null)
            {
                return NotFound(new { message = "Товар не найден" });
            }

            if (product.BarchSize < quantity)
            {
                return Ok(new { message = $"Недостаточно товара. В наличии: {product.BarchSize} единиц." });
            }

            return Ok(new { message = $"В наличии достаточно товара. В наличии: {product.BarchSize} единиц." });
        }


        [HttpGet]
        public IAsyncEnumerable<Entrance> GetEntrance()
        {
            return _dataContext.entrances.AsAsyncEnumerable();

        }

        [HttpGet("{id}")]
        [DisableRateLimiting]
        public async Task<IActionResult> GetEntrance(long id)
        {
            Entrance ent = await _dataContext.entrances.FindAsync(id);
            if (ent == null) { return NotFound(); }
            return Ok(ent);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(long id, [FromBody] Entrance ent)
        {
            var existingEntrance = await _dataContext.entrances.FindAsync(id);
            if (existingEntrance == null)
            {
                return NotFound();
            }

            try
            {
                existingEntrance.ProductName = ent.ProductName;
                existingEntrance.BarchPrice = ent.BarchPrice;
                existingEntrance.BarchDate = ent.BarchDate;
                existingEntrance.BarchSize = ent.BarchSize;
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "The record was updated or deleted by another user." });
            }
            return Ok(existingEntrance);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(long id) 
        {
            var ent = await _dataContext.entrances.FindAsync(id);
            if (ent == null) return NotFound();

            _dataContext.entrances.RemoveRange(ent);
            await _dataContext.SaveChangesAsync();
            return NoContent();
            
        
        
        
        }








    }
}
