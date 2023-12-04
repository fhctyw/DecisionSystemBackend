using DecisionSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DecisionSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly DecisionSystemDbContext _context;

        public TestController(DecisionSystemDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            var analysts = await _context.Analyst.ToListAsync();
            var customers = await _context.Customer.ToListAsync();
            var products = await _context.Product.ToListAsync();
            var carts = await _context.Cart.ToListAsync();
            var cartItems = await _context.CartItem.ToListAsync();
            var categories = await _context.Category.ToListAsync();
            var productsCategories = await _context.ProductsCategories.ToListAsync();
            var productRatings = await _context.ProductRating.ToListAsync();
            var purchases = await _context.Purchase.ToListAsync();
            var purchaseItems = await _context.PurchaseItem.ToListAsync();
            var productManagers = await _context.ProductManager.ToListAsync();
            var productAnalysis = await _context.ProductAnalys.ToListAsync();
            var archiveProductAnalysis = await _context.ArchiveProductsAnalys.ToListAsync();
            var analysisReports = await _context.AnalysReport.ToListAsync();

            return Ok(new
            {
                Analysts = analysts,
                Customers = customers,
                Products = products,
                Carts = carts,
                CartItems = cartItems,
                Categories = categories,
                ProductsCategories = productsCategories,
                ProductRatings = productRatings,
                Purchases = purchases,
                PurchaseItems = purchaseItems,
                ProductManagers = productManagers,
                ProductAnalysis = productAnalysis,
                ArchiveProductAnalysis = archiveProductAnalysis,
                AnalysisReports = analysisReports
            });
        }
    }

}
