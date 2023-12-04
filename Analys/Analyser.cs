using DecisionSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DecisionSystem.Analys
{
    public class Analyser : IAnalyser
    {
        private readonly DecisionSystemDbContext _context;

        public Analyser(DecisionSystemDbContext context)
        {
            _context = context;
        }

        public decimal AnalyseProduct(int productId)
        {
            // Отримати метрики для конкретного товару
            var reviewScore = CalculateReviewScore(productId);
            var ratingScore = CalculateRatingScore(productId);
            var salesScore = CalculateSalesScore(productId);
            var cartDataScore = CalculateCartDataScore(productId);
            var categoryPopularityScore = CalculateCategoryPopularityScore(productId);

            // Скомбінувати метрики для отримання загального показника популярності
            return (reviewScore + ratingScore + salesScore + cartDataScore + categoryPopularityScore) / 5;
        }

        private decimal CalculateReviewScore(int productId)
        {
            // Приклад розрахунку кількості оцінок
            var reviews = _context.ProductRating.Where(r => r.Id == productId).ToList();
            return Normalize(reviews.Count, _context.ProductRating.Where(r => r.ProductId == productId).Count());
        }

        private decimal CalculateRatingScore(int productId)
        {
            // Вилучення рейтингів для продукту в пам'ять програми
            var ratings = _context.ProductRating
                          .Where(r => r.ProductId == productId)
                          .Select(r => r.Rate)
                          .Cast<int>()
                          .ToList();

            // Перевірка, чи є рейтинги, та обчислення середнього значення
            var averageRating = ratings.Any() ? ratings.Average() : 0;

            return Normalize((decimal)averageRating, 5);
        }


        private decimal CalculateSalesScore(int productId)
        {
            // Приклад розрахунку кількості продажів
            var salesCount = _context.PurchaseItem
                .Where(pi => pi.ProductId == productId)
                .Join(
                    _context.Purchase,
                    pi => pi.PurchaseId,
                    p => p.Id,
                    (pi, p) => pi
                )
                .Count();

            return Normalize(salesCount, _context.PurchaseItem.Count());
        }


        private decimal CalculateCartDataScore(int productId)
        {
            // Кількість додавань вказаного товару в кошик
            var cartAdds = _context.CartItem.Count(ci => ci.ProductId == productId);

            // Максимальна кількість додавань будь-якого товару
            var maxCartAdds = _context.CartItem
                .AsEnumerable()
                .GroupBy(ci => ci.ProductId)
                .Max(g => g.Count());

            // Нормалізація
            return Normalize(cartAdds, maxCartAdds);
        }

        private decimal CalculateCategoryPopularityScore(int productId)
        {
            // Отримання категорій для продукту
            var productCategories = _context.ProductsCategories.Where(pc => pc.ProductId == productId).ToList();

            // Максимальна кількість продуктів у будь-якій категорії
            var maxCategoryPopularity = _context.ProductsCategories
                .AsEnumerable() 
                .GroupBy(c => c.CategoryId)
                .Max(g => g.Count());

            // Підрахунок популярності кожної категорії
            var categoryScores = new List<decimal>();
            foreach (var pc in productCategories)
            {
                var categoryPopularity = _context.ProductsCategories.Count(c => c.CategoryId == pc.CategoryId);
                categoryScores.Add(Normalize(categoryPopularity, maxCategoryPopularity));
            }

            // Повернення середнього значення нормалізованих оцінок
            return categoryScores.Any() ? categoryScores.Average() : 0;
        }


        // Метод для нормалізації значення
        private decimal Normalize(decimal value, decimal max)
        {
            return (max > 0) ? value / max : 0;
        }
    }
}
