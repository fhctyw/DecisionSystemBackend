using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DecisionSystem.Data;
using DecisionSystem.Models;

namespace DecisionSystem.Controllers
{
    [Route("api/product-analys")]
    [ApiController]
    public class ProductAnalysController : ControllerBase
    {
        private readonly DecisionSystemDbContext _context;

        public ProductAnalysController(DecisionSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductAnalys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductAnalys>>> GetProductAnalys()
        {
            if (_context.ProductAnalys == null)
            {
                return NotFound();
            }
            return await _context.ProductAnalys.ToListAsync();
        }

        // GET: api/ProductAnalys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductAnalys>> GetProductAnalys(int id)
        {
            if (_context.ProductAnalys == null)
            {
                return NotFound();
            }
            var productAnalys = await _context.ProductAnalys.FindAsync(id);

            if (productAnalys == null)
            {
                return NotFound();
            }

            return productAnalys;
        }

        // PUT: api/ProductAnalys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductAnalys(int id, ProductAnalys productAnalys)
        {
            if (id != productAnalys.Id)
            {
                return BadRequest();
            }

            _context.Entry(productAnalys).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductAnalysExists(id))
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

        // POST: api/ProductAnalys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductAnalys>> PostProductAnalys(ProductAnalys productAnalys)
        {
            if (_context.ProductAnalys == null)
            {
                return Problem("Entity set 'DecisionSystemDbContext.ProductAnalys'  is null.");
            }
            _context.ProductAnalys.Add(productAnalys);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductAnalys", new { id = productAnalys.Id }, productAnalys);
        }

        // DELETE: api/ProductAnalys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAnalys(int id)
        {
            if (_context.ProductAnalys == null)
            {
                return NotFound();
            }
            var productAnalys = await _context.ProductAnalys.FindAsync(id);
            if (productAnalys == null)
            {
                return NotFound();
            }

            _context.ProductAnalys.Remove(productAnalys);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductAnalysExists(int id)
        {
            return (_context.ProductAnalys?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
