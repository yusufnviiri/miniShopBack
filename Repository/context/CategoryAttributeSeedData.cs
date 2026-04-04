using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repository.context
{
         public static class CategoryAttributeSeedData
    {
        private class AttributeSeedWrapper
        {
    
            public List<CategoryAttributeDto>? CategoryAttributes { get; set; }



        }











        public static void Seed(ModelBuilder modelBuilder)
        {
            var jsonPath = Path.Combine(AppContext.BaseDirectory, "CategoryAttributeSeedData.json");
            if (!File.Exists(jsonPath))
                throw new FileNotFoundException($"Seed file not found at: {jsonPath}");

            string json = File.ReadAllText(jsonPath);

            var seedWrapper = JsonSerializer.Deserialize<AttributeSeedWrapper>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (seedWrapper?.CategoryAttributes == null) return;

            foreach (var item in seedWrapper.CategoryAttributes)
            {
                // Seed Hymn
                modelBuilder.Entity<CategoryAttribute>().HasData(new CategoryAttribute
                {
                    CategoryId = item.CategoryId,
                    CategoryAttributeId = item.CategoryAttributeId,
                    AttributeDataTypeId = item.AttributeDataTypeId,
                    AttributeName = item.AttributeName,               
               
                });

            }

           

        }

    }

}

