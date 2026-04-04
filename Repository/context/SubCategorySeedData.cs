using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.context
{

    public class SubCategorySeedData : IEntityTypeConfiguration<SubCategory>
    {

        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasData(
                //Fashion & Lifestyle
                new SubCategory { SubCategoryId = 1, CategoryId = 1, SubCategoryName = "Men's Fashion" },
                new SubCategory { SubCategoryId = 2, CategoryId = 1, SubCategoryName = "Women's Fashion" },
                new SubCategory { SubCategoryId = 3, CategoryId = 1, SubCategoryName = "Watches and Jewelry" },
                new SubCategory { SubCategoryId = 4, CategoryId = 1, SubCategoryName = "Wedding Fashions" },
                new SubCategory { SubCategoryId = 5, CategoryId = 1, SubCategoryName = "Kid's Fashion" },
                new SubCategory { SubCategoryId = 6, CategoryId = 1, SubCategoryName = "Bags and Luggage" },
                new SubCategory { SubCategoryId = 7, CategoryId = 1, SubCategoryName = "School Uniforms" },
            //Electronics
            new SubCategory { SubCategoryId = 8, CategoryId = 2, SubCategoryName = "Television " },
             new SubCategory { SubCategoryId = 9, CategoryId = 2, SubCategoryName = "Camera and Photos" },
             new SubCategory { SubCategoryId = 10, CategoryId = 2, SubCategoryName = "Home Audio" },
             new SubCategory { SubCategoryId = 11, CategoryId = 2, SubCategoryName = "Car and Vehicle Electronics" }, 
             new SubCategory { SubCategoryId = 12, CategoryId = 2, SubCategoryName = "Audio" },
             new SubCategory { SubCategoryId = 13, CategoryId = 2, SubCategoryName = "Cables" }, 
             new SubCategory { SubCategoryId = 14, CategoryId = 2, SubCategoryName = "Audio Gadgets" },
                  //Appliances
             new SubCategory { SubCategoryId = 15, CategoryId = 3, SubCategoryName = "Bulk Appliances" },
            new SubCategory { SubCategoryId = 16, CategoryId = 3, SubCategoryName = "Small Appliances" },
            new SubCategory { SubCategoryId = 17, CategoryId = 3, SubCategoryName = "Brands" },
            new SubCategory { SubCategoryId = 18, CategoryId = 3, SubCategoryName = "Cooking Appliances" },
            //Home
            new SubCategory { SubCategoryId = 19, CategoryId = 4, SubCategoryName = "Bath" },
            new SubCategory { SubCategoryId = 20, CategoryId = 4, SubCategoryName = "Bedding" },
            new SubCategory { SubCategoryId = 21, CategoryId = 4, SubCategoryName = "Kitchen and Dining" },
            //Health and Beauty
           new SubCategory { SubCategoryId = 22, CategoryId = 5, SubCategoryName = "Makeup" },
           new SubCategory { SubCategoryId = 23, CategoryId = 5, SubCategoryName = "Perfume" },
           new SubCategory { SubCategoryId = 24, CategoryId = 5, SubCategoryName = "Skin Care" },
           new SubCategory { SubCategoryId = 25, CategoryId = 5, SubCategoryName = "Hair Care" },
           //Computing
           new SubCategory { SubCategoryId = 26, CategoryId = 6, SubCategoryName = "Laptops" },
           new SubCategory { SubCategoryId = 27, CategoryId = 6, SubCategoryName = "Computer Accessories" },
           new SubCategory { SubCategoryId = 28, CategoryId = 6, SubCategoryName = "Computer Storage" },
           new SubCategory { SubCategoryId = 29, CategoryId = 6, SubCategoryName = "Printersmand Scanners" },
           new SubCategory { SubCategoryId = 30, CategoryId = 6, SubCategoryName = "Monitors" },
           new SubCategory { SubCategoryId = 31, CategoryId = 6, SubCategoryName = "Desktops and CPU" },

           //Baby Products
           new SubCategory { SubCategoryId = 32, CategoryId = 7, SubCategoryName = "Baby Care" },
           new SubCategory { SubCategoryId = 33, CategoryId = 7, SubCategoryName = "Toys & Games" },
           new SubCategory { SubCategoryId = 34, CategoryId = 7, SubCategoryName = "Diapers" },
           new SubCategory { SubCategoryId = 35, CategoryId = 7, SubCategoryName = "Baby Skin Care" },
           new SubCategory { SubCategoryId = 36, CategoryId = 7, SubCategoryName = "Feeding" },
          new SubCategory { SubCategoryId = 37, CategoryId = 7, SubCategoryName = "Clothing" },
           new SubCategory { SubCategoryId = 38, CategoryId = 8, SubCategoryName = "Fresh Food" },
           new SubCategory { SubCategoryId = 39, CategoryId =8 , SubCategoryName = "Stored Food" },
           new SubCategory { SubCategoryId = 40, CategoryId =8 , SubCategoryName = "Resturant And TakeAway" },
           new SubCategory { SubCategoryId = 41, CategoryId = 8, SubCategoryName = "Snacks" },
           new SubCategory { SubCategoryId = 42, CategoryId =8 , SubCategoryName = "Cakes and Bakery" },
           new SubCategory { SubCategoryId = 43, CategoryId = 8, SubCategoryName = "Others" },
           //Phones and Tablets
           new SubCategory { SubCategoryId = 44, CategoryId = 9, SubCategoryName = "Mobile Phones" },
           new SubCategory { SubCategoryId = 45, CategoryId = 9, SubCategoryName = "Phone Accessories" },
           new SubCategory { SubCategoryId = 46, CategoryId = 9, SubCategoryName = "Tablets" },
           new SubCategory { SubCategoryId = 47, CategoryId = 9, SubCategoryName = "Brands" },
           //Transport Services
           new SubCategory { SubCategoryId = 48, CategoryId = 10, SubCategoryName = "Trucks and Pickups" },
           new SubCategory { SubCategoryId = 49, CategoryId = 10, SubCategoryName = "Family Cars" },
           new SubCategory { SubCategoryId = 50, CategoryId =10 , SubCategoryName = "Driver" },
           //Sports, Fitness & Outdoor
           new SubCategory { SubCategoryId = 51, CategoryId = 11, SubCategoryName = "Gym Equipment" },
           new SubCategory { SubCategoryId = 52, CategoryId = 11, SubCategoryName = "Sportswear" },
           new SubCategory { SubCategoryId = 53, CategoryId = 11, SubCategoryName = "Outdoor Gear" },
           //Repair & Maintenance
           new SubCategory { SubCategoryId = 54, CategoryId = 12, SubCategoryName = "Computer Repair" },
           new SubCategory { SubCategoryId = 55, CategoryId = 12, SubCategoryName = "Computer Installation & Setup" },
           new SubCategory { SubCategoryId = 56, CategoryId =12 , SubCategoryName = "Phone Repair" },
           new SubCategory { SubCategoryId = 57, CategoryId = 12, SubCategoryName = "Car Repair" },
           // Digital Goods
           new SubCategory { SubCategoryId = 58, CategoryId = 13, SubCategoryName = "Software Licenses " },
           new SubCategory { SubCategoryId = 59, CategoryId = 13, SubCategoryName = "Online Courses" },
           new SubCategory { SubCategoryId = 69, CategoryId = 13, SubCategoryName = "SaaS Subscriptions" },
           new SubCategory { SubCategoryId = 70, CategoryId = 13, SubCategoryName = "Graphics Design" },
           //Land
           new SubCategory { SubCategoryId = 71, CategoryId = 14, SubCategoryName = "Land With Title" },
           new SubCategory { SubCategoryId = 72, CategoryId = 14, SubCategoryName = "Land With Agreement" },
           //Wedding Planner
           new SubCategory { SubCategoryId = 73, CategoryId = 15, SubCategoryName = "Wedding Planner" },
           new SubCategory { SubCategoryId = 74, CategoryId = 15, SubCategoryName = "Event Planner" },
           new SubCategory { SubCategoryId = 75, CategoryId = 15, SubCategoryName = "MC" },
           new SubCategory { SubCategoryId = 76, CategoryId = 15, SubCategoryName = "DJ" },
           //Agricultural Products
           new SubCategory { SubCategoryId = 77, CategoryId =16, SubCategoryName = "Food Crops" },
           new SubCategory { SubCategoryId = 78, CategoryId = 16, SubCategoryName = "Oil Crops" },
           new SubCategory { SubCategoryId = 79, CategoryId = 16, SubCategoryName = "Industrial Crops" }
          
          





                );
        }
    }
}
