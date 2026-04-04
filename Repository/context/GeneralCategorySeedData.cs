using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.context
{
    public class GeneralCategorySeedData : IEntityTypeConfiguration<GeneralCategory>
    {

        public void Configure(EntityTypeBuilder<GeneralCategory> builder)
        {
            builder.HasData(
                new GeneralCategory { GeneralCategoryId = 1, GeneralCategoryName = "Phones and Gadgets" },
                new GeneralCategory { GeneralCategoryId = 2, GeneralCategoryName = "Computers" },
                new GeneralCategory { GeneralCategoryId = 3, GeneralCategoryName = "Men's wear" },
                new GeneralCategory { GeneralCategoryId = 4, GeneralCategoryName = "Women's wear" },
                new GeneralCategory { GeneralCategoryId = 5, GeneralCategoryName = "Kids fashion" },
                new GeneralCategory { GeneralCategoryId = 6, GeneralCategoryName = "Brands" },
                new GeneralCategory { GeneralCategoryId = 7, GeneralCategoryName = "Home" },
                new GeneralCategory { GeneralCategoryId = 8, GeneralCategoryName = "Bags" },
                new GeneralCategory { GeneralCategoryId = 9, GeneralCategoryName = "Appliances" },
                new GeneralCategory { GeneralCategoryId = 10, GeneralCategoryName = "Others" },

                new GeneralCategory { GeneralCategoryId = 11, GeneralCategoryName = "Health and Beauty" },
                new GeneralCategory { GeneralCategoryId = 12, GeneralCategoryName = "Trades" },

               new GeneralCategory { GeneralCategoryId = 13, GeneralCategoryName = "Software" },
               new GeneralCategory { GeneralCategoryId = 14, GeneralCategoryName = "Food & Agriculture" },
                new GeneralCategory { GeneralCategoryId = 15, GeneralCategoryName = "Property" },
               new GeneralCategory { GeneralCategoryId = 16, GeneralCategoryName = "Fashion & Apparel" },
              new GeneralCategory { GeneralCategoryId = 17, GeneralCategoryName = "Electronics & Home Entertainment" },
               new GeneralCategory { GeneralCategoryId = 18, GeneralCategoryName = "Food & Beverages" }



);       }
    }
}
