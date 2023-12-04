using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DecisionSystem.Data;
using DecisionSystem.Models;
using DecisionSystem.DTOs;

namespace DecisionSystem.Controllers
{
    [Route("api/analys-factor")]
    [ApiController]
    public class AnalysFactorsController : ControllerBase
    {
        private readonly DecisionSystemDbContext _context;

        public AnalysFactorsController(DecisionSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/AnalysFactors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnalysFactor>>> GetAnalysFactor()
        {
          if (_context.AnalysFactor == null)
          {
              return NotFound();
          }
            return await _context.AnalysFactor.ToListAsync();
        }

        // GET: api/AnalysFactors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnalysFactor>> GetAnalysFactor(int id)
        {
          if (_context.AnalysFactor == null)
          {
              return NotFound();
          }
            var analysFactor = await _context.AnalysFactor.FindAsync(id);

            if (analysFactor == null)
            {
                return NotFound();
            }

            return analysFactor;
        }

        // PUT: api/AnalysFactors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnalysFactor(int id, AnalysFactor analysFactor)
        {
            if (id != analysFactor.Id)
            {
                return BadRequest();
            }

            _context.Entry(analysFactor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnalysFactorExists(id))
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

        // PUT: api/weight
        [HttpPut("weight")]
        public async Task<IActionResult> PutAnalysFactorWeight([FromBody] AnalysWeightDTO dto)
        {
            var factor = await _context.AnalysFactor.FirstOrDefaultAsync(af => af.Id == dto.Id);
            if (factor == null)
            {
                return BadRequest();
            }
            factor.Weight = dto.Weight;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/AnalysFactors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AnalysFactor>> PostAnalysFactor(AnalysFactor analysFactor)
        {
          if (_context.AnalysFactor == null)
          {
              return Problem("Entity set 'DecisionSystemDbContext.AnalysFactor'  is null.");
          }
            _context.AnalysFactor.Add(analysFactor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnalysFactor", new { id = analysFactor.Id }, analysFactor);
        }

        // DELETE: api/AnalysFactors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnalysFactor(int id)
        {
            if (_context.AnalysFactor == null)
            {
                return NotFound();
            }
            var analysFactor = await _context.AnalysFactor.FindAsync(id);
            if (analysFactor == null)
            {
                return NotFound();
            }

            _context.AnalysFactor.Remove(analysFactor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnalysFactorExists(int id)
        {
            return (_context.AnalysFactor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
