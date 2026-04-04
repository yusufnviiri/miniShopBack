using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.context
{
    public class CategorySeedData : IEntityTypeConfiguration<Category>
    {
     
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { CategoryId = 1,GeneralCategoryId=1, CategoryName = "Fashion & Lifestyle" },
                new Category { CategoryId = 2, GeneralCategoryId =1, CategoryName = "Electronics " },
                new Category { CategoryId = 3, GeneralCategoryId =9, CategoryName = "Appliances" },
                new Category { CategoryId = 4, GeneralCategoryId =1, CategoryName = "Home" },
                new Category { CategoryId = 5, GeneralCategoryId =6, CategoryName = "Health and Beauty" },
                new Category { CategoryId = 6, GeneralCategoryId =2, CategoryName = "Computing" },
                new Category { CategoryId = 7, GeneralCategoryId =5, CategoryName = "Baby Products" },
                new Category { CategoryId = 8, GeneralCategoryId =7, CategoryName = "Food and Catering" },
                new Category { CategoryId = 9, GeneralCategoryId =6, CategoryName = "Phones and Tablets" },
                new Category { CategoryId = 10, GeneralCategoryId =8, CategoryName = "Transport Services" },
                new Category { CategoryId = 11, GeneralCategoryId =3, CategoryName = "Sports, Fitness & Outdoor" },
                new Category { CategoryId = 12, GeneralCategoryId =4, CategoryName = "Repair & Maintenance" },
                new Category { CategoryId = 13, GeneralCategoryId =7, CategoryName = "Digital Goods" },
                new Category { CategoryId = 14, GeneralCategoryId =10, CategoryName = "Land" },
                new Category { CategoryId = 15, GeneralCategoryId =10, CategoryName = "Function Planner" },
                new Category { CategoryId = 16, GeneralCategoryId =10, CategoryName = "Agricultural Products" }








             );
        }
    }
}
