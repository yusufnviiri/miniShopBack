using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.context
{
    public class SubCategoryCategorySeedData : IEntityTypeConfiguration<SubCategoryCategory>
    {

        public void Configure(EntityTypeBuilder<SubCategoryCategory> builder)
        {
            builder.HasData(
                //Men's Fashion"
                new SubCategoryCategory { SubCategoryId = 1, SubCategoryCategoryId = 1, SubCategoryCategoryName = "Trousers" },
              new SubCategoryCategory { SubCategoryId = 1, SubCategoryCategoryId = 2, SubCategoryCategoryName = "Shirts" },

                new SubCategoryCategory { SubCategoryId = 1, SubCategoryCategoryId = 3, SubCategoryCategoryName = "T-Shirts" },
                new SubCategoryCategory { SubCategoryId = 1, SubCategoryCategoryId = 4, SubCategoryCategoryName = "Shoes" },
                new SubCategoryCategory { SubCategoryId = 1, SubCategoryCategoryId = 5, SubCategoryCategoryName = "Jewelry" },
                new SubCategoryCategory { SubCategoryId = 1, SubCategoryCategoryId = 6, SubCategoryCategoryName = "Watches" },

                //Women's Fashion
                new SubCategoryCategory { SubCategoryId = 2, SubCategoryCategoryId = 7, SubCategoryCategoryName = "Clothing" },
                new SubCategoryCategory { SubCategoryId = 2, SubCategoryCategoryId = 8, SubCategoryCategoryName = "Shoes" },
                new SubCategoryCategory { SubCategoryId = 2, SubCategoryCategoryId = 9, SubCategoryCategoryName = "Accessories" },
                new SubCategoryCategory { SubCategoryId = 2, SubCategoryCategoryId = 10, SubCategoryCategoryName = "Handbags" },
                new SubCategoryCategory { SubCategoryId = 2, SubCategoryCategoryId = 11, SubCategoryCategoryName = "Jewelry" },
                //Wedding Fashions
                new SubCategoryCategory { SubCategoryId = 4, SubCategoryCategoryId = 12, SubCategoryCategoryName = "Bridals" },
                new SubCategoryCategory { SubCategoryId = 4, SubCategoryCategoryId = 13, SubCategoryCategoryName = "Jewelry" },
                new SubCategoryCategory { SubCategoryId = 4, SubCategoryCategoryId = 14, SubCategoryCategoryName = "Decorations" },
                //Kid's Fashion
                new SubCategoryCategory { SubCategoryId = 5, SubCategoryCategoryId = 15, SubCategoryCategoryName = "Boys Clothes" },
                new SubCategoryCategory { SubCategoryId = 5, SubCategoryCategoryId = 16, SubCategoryCategoryName = "Girls Clothes" },
                new SubCategoryCategory { SubCategoryId = 5, SubCategoryCategoryId = 17, SubCategoryCategoryName = "Baby Beddings" },
                new SubCategoryCategory { SubCategoryId = 5, SubCategoryCategoryId = 18, SubCategoryCategoryName = "Baby Clothes" },
                new SubCategoryCategory { SubCategoryId = 5, SubCategoryCategoryId = 19, SubCategoryCategoryName = "Kid's Shoes" },
                new SubCategoryCategory { SubCategoryId = 5, SubCategoryCategoryId = 20, SubCategoryCategoryName = "Modern Baby" },
                   //Baby Care
                   new SubCategoryCategory { SubCategoryId = 32, SubCategoryCategoryId = 21, SubCategoryCategoryName = "Baby Jumpers" },
                new SubCategoryCategory { SubCategoryId = 32, SubCategoryCategoryId = 22, SubCategoryCategoryName = "Swings" },
                //Bags and Luggage
                new SubCategoryCategory { SubCategoryId = 6, SubCategoryCategoryId = 23, SubCategoryCategoryName = "Laptop Bags" },
                new SubCategoryCategory { SubCategoryId = 6, SubCategoryCategoryId = 24, SubCategoryCategoryName = "BackPacks" },
                   new SubCategoryCategory { SubCategoryId = 6, SubCategoryCategoryId = 25, SubCategoryCategoryName = "Luggage Bags" },
                new SubCategoryCategory { SubCategoryId = 6, SubCategoryCategoryId = 26, SubCategoryCategoryName = "Gymn Bags" },
                // School Uniforms

                new SubCategoryCategory { SubCategoryId = 7, SubCategoryCategoryId = 27, SubCategoryCategoryName = "School Sweaters" },
                new SubCategoryCategory { SubCategoryId = 7, SubCategoryCategoryId = 28, SubCategoryCategoryName = "Full School Uniform" },
                     new SubCategoryCategory { SubCategoryId = 7, SubCategoryCategoryId = 29, SubCategoryCategoryName = "School Stockings" },
            //Television
            new SubCategoryCategory { SubCategoryId = 8, SubCategoryCategoryId = 30, SubCategoryCategoryName = "Smart TVs" },            

            new SubCategoryCategory { SubCategoryId = 8, SubCategoryCategoryId = 31, SubCategoryCategoryName = "LED TVs" },
            new SubCategoryCategory { SubCategoryId =8 , SubCategoryCategoryId = 32, SubCategoryCategoryName = "DVD player" },
           new SubCategoryCategory { SubCategoryId = 8, SubCategoryCategoryId = 33, SubCategoryCategoryName = "Samsung TVs" },
           new SubCategoryCategory { SubCategoryId = 8, SubCategoryCategoryId =34 , SubCategoryCategoryName = "Hisense TVs" },
           new SubCategoryCategory { SubCategoryId =8 , SubCategoryCategoryId = 35, SubCategoryCategoryName = "Smart TV 32 inches" },
           new SubCategoryCategory { SubCategoryId = 8, SubCategoryCategoryId = 36, SubCategoryCategoryName = "Smart TV 43 inches" },
           new SubCategoryCategory { SubCategoryId = 8, SubCategoryCategoryId = 37, SubCategoryCategoryName = "Smart TV 55 inches" },
           new SubCategoryCategory { SubCategoryId = 8, SubCategoryCategoryId = 38, SubCategoryCategoryName = "TV Accessories" },

           //Camera and Photos
           new SubCategoryCategory { SubCategoryId = 9, SubCategoryCategoryId = 39, SubCategoryCategoryName = "Digital Cameras" },
           new SubCategoryCategory { SubCategoryId = 9, SubCategoryCategoryId = 40, SubCategoryCategoryName = "Projectors" },
           new SubCategoryCategory { SubCategoryId = 9, SubCategoryCategoryId = 41, SubCategoryCategoryName = "Binoculars and Scopes" },
          new SubCategoryCategory { SubCategoryId = 9, SubCategoryCategoryId = 42, SubCategoryCategoryName = "CamCoders" },
         new SubCategoryCategory { SubCategoryId = 9, SubCategoryCategoryId = 43, SubCategoryCategoryName = "Monitoring Cameras" },
         //Home Audio
         new SubCategoryCategory { SubCategoryId = 10, SubCategoryCategoryId = 44, SubCategoryCategoryName = "Home Theatre " },
         new SubCategoryCategory { SubCategoryId = 10, SubCategoryCategoryId = 45, SubCategoryCategoryName = "Bluetooth Speakers" },
        new SubCategoryCategory { SubCategoryId = 10, SubCategoryCategoryId = 46, SubCategoryCategoryName = "Recievers and Amplifiers" },
        new SubCategoryCategory { SubCategoryId = 10, SubCategoryCategoryId = 47, SubCategoryCategoryName = "Sound Bars" }










                );
        }
    }
}
