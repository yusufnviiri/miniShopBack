using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repository.context
    {
        public static class CategoryMigrationDataSeeder
        {
            private class CategorySeedWrapper
            {
            public List<Category>? Categories { get; set; }
                public List<SubCategory>? SubCategories { get; set; }

            public List<SubCategoryCategory>? SubCategoryCategories { get; set; } 

        }

        //public static async Task SeedAsync(ApplicationDbContext context)
        //    {
        //        // Prevent duplicate seeding
        //        if (await context.Categories.AnyAsync())
        //            return;

        //        var jsonPath = Path.Combine(
        //            AppContext.BaseDirectory,
        //            "SeedServicesData.json");

        //        if (!File.Exists(jsonPath))
        //            throw new FileNotFoundException($"Seed file not found at: {jsonPath}");

        //        var json = await File.ReadAllTextAsync(jsonPath);

        //        var seedData = JsonSerializer.Deserialize<CategorySeedWrapper>(
        //            json,
        //            new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true
        //            });

        //        if (seedData == null) return;

        //        if (seedData.Categories != null)
        //            await context.Categories.AddRangeAsync(seedData.Categories);

        //        if (seedData.SubCategories != null)
        //            await context.SubCategories.AddRangeAsync(seedData.SubCategories);

        //        if (seedData.SubCategoryCategories != null)
        //            await context.SubCategoryCategories.AddRangeAsync(seedData.SubCategoryCategories);

        //        await context.SaveChangesAsync();
        //    }










        public static void Seed(ModelBuilder modelBuilder)
        {
            var jsonPath = Path.Combine(AppContext.BaseDirectory, "CategoryDataSeed.json");
            if (!File.Exists(jsonPath))
                throw new FileNotFoundException($"Seed file not found at: {jsonPath}");

            string json = File.ReadAllText(jsonPath);

            var seedWrapper = JsonSerializer.Deserialize<CategorySeedWrapper>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (seedWrapper?.Categories == null) return;

            foreach (var item in seedWrapper.Categories)
            {
                // Seed Hymn
                modelBuilder.Entity<Category>().HasData(new Category
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    Type = item.Type,
                    GeneralCategoryId = item.GeneralCategoryId,
                                 });

            }

            if (seedWrapper?.SubCategories == null) return;

            foreach (var item in seedWrapper.SubCategories)
            {
                // Seed Hymn
                modelBuilder.Entity<SubCategory>().HasData(new SubCategory
                {
                    CategoryId = item.CategoryId,
                    SubCategoryName = item.SubCategoryName,
                    SubCategoryId = item.SubCategoryId,
                });

            }

            if (seedWrapper?.SubCategoryCategories == null) return;

            foreach (var item in seedWrapper.SubCategoryCategories)
            {
                // Seed Hymn
                modelBuilder.Entity<SubCategoryCategory>().HasData(new SubCategoryCategory
                {
                    SubCategoryCategoryId = item.SubCategoryCategoryId,
                    SubCategoryCategoryName = item.SubCategoryCategoryName,
                    SubCategoryId = item.SubCategoryId,
                });

            }

        }






    }
}