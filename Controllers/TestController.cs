using DecisionSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DecisionSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        /*[HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }*/
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
            //var productPlacements = await _context.ProductPlacementsToListAsync();
            var carts = await _context.Cart.ToListAsync();
            //var recommendations = await _context.Recommendations.ToListAsync();
            var productManagers = await _context.ProductManager.ToListAsync();

            return Ok(new
            {
                Analysts = analysts,
                Customers = customers,
                Products = products,
                //ProductPlacements = productPlacements,
                Carts = carts,
                //Recommendations = recommendations,
                ProductManagers = productManagers
            });
        }
    }
}
