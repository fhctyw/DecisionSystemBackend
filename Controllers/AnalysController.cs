using DecisionSystem.Analys;
using DecisionSystem.Data;
using DecisionSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DecisionSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalysController : Controller
    {
        private readonly DecisionSystemDbContext _context;
        private readonly IAnalyser _analyser;

        public AnalysController(DecisionSystemDbContext context)
        {
            _context = context;
            _analyser = new Analyser(_context);
        }

        // GET: 
        [HttpGet("Products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAnalysedProducts()
        {
            if (_context.ProductAnalys == null || _context.Product == null)
            {
                return NotFound();
            }
            var analysedProducts = await (from pa in _context.ProductAnalys
                                          join p in _context.Product on pa.ProductId equals p.Id
                                          orderby pa.Popularity descending
                                          select p).ToListAsync();
            return Ok(analysedProducts);
        }

        [HttpPost("PerformAnalyse")]
        public async Task<IActionResult> PerformAnalyse()
        {
            var analysReport = new AnalysReport { Date = DateTime.Now };
            _context.AnalysReport.Add(analysReport);
            await _context.SaveChangesAsync();

            // Отримання всіх продуктів та їх популярності
            var productAnalyses = await _context.ProductAnalys.ToListAsync();

            // Архівування даних про популярність у ArchiveProductsAnalys
            foreach (var analysis in productAnalyses)
            {
                var archiveProductAnalysis = new ArchiveProductsAnalys
                {
                    AnalysReportId = analysReport.Id,
                    ProductId = analysis.ProductId,
                    Popularity = analysis.Popularity
                };
                _context.ArchiveProductsAnalys.Add(archiveProductAnalysis);
            }

            // Видалення старих даних з ProductAnalys
            var allProductAnalyses = _context.ProductAnalys.ToList();
            _context.ProductAnalys.RemoveRange(allProductAnalyses);
            await _context.SaveChangesAsync();

            // Отримання всіх продуктів для аналізу
            var products = await _context.Product.ToListAsync();

            // Аналіз кожного продукту та оновлення ProductAnalys
            foreach (var product in products)
            {
                var popularity = _analyser.AnalyseProduct(product.Id);
                var newProductAnalysis = new ProductAnalys
                {
                    ProductId = product.Id,
                    Popularity = popularity
                };
                _context.ProductAnalys.Add(newProductAnalysis);
            }

            // Збереження нових даних про популярність
            await _context.SaveChangesAsync();

            return Ok(await _context.ProductAnalys.ToListAsync());
        }
    }

}
