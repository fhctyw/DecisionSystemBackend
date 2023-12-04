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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductManagersController : ControllerBase
    {
        private readonly DecisionSystemDbContext _context;

        public ProductManagersController(DecisionSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductManagers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductManager>>> GetProductManager()
        {
          if (_context.ProductManager == null)
          {
              return NotFound();
          }
            return await _context.ProductManager.ToListAsync();
        }

        // GET: api/ProductManagers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductManager>> GetProductManager(int id)
        {
          if (_context.ProductManager == null)
          {
              return NotFound();
          }
            var productManager = await _context.ProductManager.FindAsync(id);

            if (productManager == null)
            {
                return NotFound();
            }

            return productManager;
        }

        // PUT: api/ProductManagers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductManager(int id, ProductManager productManager)
        {
            if (id != productManager.Id)
            {
                return BadRequest();
            }

            _context.Entry(productManager).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductManagerExists(id))
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

        // POST: api/ProductManagers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductManager>> PostProductManager(ProductManager productManager)
        {
          if (_context.ProductManager == null)
          {
              return Problem("Entity set 'DecisionSystemDbContext.ProductManager'  is null.");
          }
            _context.ProductManager.Add(productManager);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductManager", new { id = productManager.Id }, productManager);
        }

        // DELETE: api/ProductManagers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductManager(int id)
        {
            if (_context.ProductManager == null)
            {
                return NotFound();
            }
            var productManager = await _context.ProductManager.FindAsync(id);
            if (productManager == null)
            {
                return NotFound();
            }

            _context.ProductManager.Remove(productManager);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductManagerExists(int id)
        {
            return (_context.ProductManager?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
