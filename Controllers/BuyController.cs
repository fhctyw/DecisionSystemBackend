using DecisionSystem.Data;
using DecisionSystem.DTOs;
using DecisionSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DecisionSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuyController : Controller
    {
        private readonly DecisionSystemDbContext _context;

        public BuyController(DecisionSystemDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Buy([FromBody] BuyDTO buyDTO)
        {
            // Отримання ID кошика для даного клієнта
            var cart = await _context.Cart.Where(c => c.CustomerId == buyDTO.CustomerId).FirstOrDefaultAsync();

            if (cart == null)
            {
                // Кошик не знайдено
                return NotFound("Not found cart");
            }

            var productId = await _context.Product.Where(p => p.Id == buyDTO.ProductId).FirstOrDefaultAsync();

            if (productId == null)
            {
                return NotFound("Not found product");
            }

            // Створення нового запису про покупку
            var purchase = new Purchase
            {
                CustomerId = buyDTO.CustomerId,
                Date = DateTime.Now
                // TotalPrice буде оновлено після розрахунку
            };

            _context.Purchase.Add(purchase);
            await _context.SaveChangesAsync();

            // Отримання товарів з кошика
            var cartItems = await _context.CartItem
                                .Where(ci => ci.CartId == cart.Id)
                                .ToListAsync();

            decimal totalPrice = 0;

            // Копіювання товарів з кошика в покупки
            foreach (var item in cartItems)
            {
                var product = await _context.Product
                                .Where(p => p.Id == item.ProductId)
                                .FirstOrDefaultAsync();

                if (product == null)
                {
                    // Продукт не знайдено
                    continue; // або повернути помилку
                }

                var purchaseItem = new PurchaseItem
                {
                    PurchaseId = purchase.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                totalPrice += item.Quantity * product.Price; // Ціна товару

                _context.PurchaseItem.Add(purchaseItem);
            }

            // Оновлення загальної ціни покупки
            purchase.TotalPrice = totalPrice;
            _context.Purchase.Update(purchase);

            // Видалення товарів з кошика
            _context.CartItem.RemoveRange(cartItems);

            await _context.SaveChangesAsync();

            return Ok(buyDTO);

        }

    }
}
