using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Repository.context
{
    public static class CategoryRuntimeSeeder
    {
        private class CategorySeedWrapper
        {
            public List<Category>? Categories { get; set; }
            public List<SubCategory>? SubCategories { get; set; }
            public List<SubCategoryCategory>? SubCategoryCategories { get; set; }
        }

        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // Prevent duplicate seeding
            if (await context.Categories.AnyAsync())
                return;

            var jsonPath = Path.Combine(
                AppContext.BaseDirectory,
                "CategoryDataSeed.json");

            if (!File.Exists(jsonPath))
                throw new FileNotFoundException($"Seed file not found at: {jsonPath}");

            var json = await File.ReadAllTextAsync(jsonPath);

            var seedData = JsonSerializer.Deserialize<CategorySeedWrapper>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (seedData == null) return;

            if (seedData.Categories != null)
                await context.Categories.AddRangeAsync(seedData.Categories);

            if (seedData.SubCategories != null)
                await context.SubCategories.AddRangeAsync(seedData.SubCategories);

            if (seedData.SubCategoryCategories != null)
                await context.SubCategoryCategories.AddRangeAsync(seedData.SubCategoryCategories);

            await context.SaveChangesAsync();
        }
    }
}