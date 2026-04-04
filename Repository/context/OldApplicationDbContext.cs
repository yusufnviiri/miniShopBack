//using Entities.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Repository.context
//{
//    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
//    {
//        public ApplicationDbContext(DbContextOptions options) : base(options) { }
//        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
//        {
//            configurationBuilder.Properties<decimal>()
//                .HavePrecision(18, 2);
//        }


//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {

//            base.OnModelCreating(modelBuilder);
//            //modelBuilder.ApplyConfiguration(new CategorySeedData());
//            //modelBuilder.ApplyConfiguration(new SubCategorySeedData());
//            modelBuilder.ApplyConfiguration(new GroupRoleSeedData());
//            modelBuilder.ApplyConfiguration(new GroupTypeDataSeed());
//            modelBuilder.ApplyConfiguration(new MemberStatusSeedData());
//            //modelBuilder.ApplyConfiguration(new SubCategoryCategorySeedData());
//            modelBuilder.ApplyConfiguration(new CommodityClassSeedData());
//            modelBuilder.ApplyConfiguration(new UserStatusSeedData());
//            modelBuilder.ApplyConfiguration(new SellerPolicySeedData());
//            modelBuilder.ApplyConfiguration(new GroupCategorySeedData());
//            modelBuilder.ApplyConfiguration(new SellerTierSeedData());
//            modelBuilder.ApplyConfiguration(new BuyerTierSeedData());
//            modelBuilder.ApplyConfiguration(new SellerTypeSeedData());
//            modelBuilder.ApplyConfiguration(new SellerOfferingSeedData());
//            modelBuilder.ApplyConfiguration(new AttributeDataTypeSeedData());
//            modelBuilder.ApplyConfiguration(new GeneralCategorySeedData());
//            modelBuilder.ApplyConfiguration(new BookingStatusSeedData());
//            CategoryMigrationDataSeeder.Seed(modelBuilder);
//            CategoryAttributeSeedData.Seed(modelBuilder);





//            string appUserAId = "42cd3af3-f319-4118-a604-4442d487b923";
//            string appUserBId = "5f2b8a40-d899-4345-aa3e-7b98712bc112";
//            string appUserCId = "bd363936-63d0-4ace-bc46-a2f1348cb61e";

//            string ROLE1_ID = "25153dfe-8a7a-48f3-a2f9-c314232dd6a3";
//            string ROLE2_ID = "bb69367c-0380-470b-8337-35644e861587";
//            string ROLE3_ID = "1c13657c-0c92-4dec-b308-c41c725a1513";


//            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
//            {
//                Name = "platform admin",
//                NormalizedName = "PLATFORM ADMIN",
//                Id = ROLE1_ID,
//                ConcurrencyStamp = ROLE1_ID
//            });
//            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
//            {
//                Name = "super admin",
//                NormalizedName = "SUPER ADMIN",
//                Id = ROLE2_ID,
//                ConcurrencyStamp = ROLE2_ID
//            });

//            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
//            {
//                Name = "operations",
//                NormalizedName = "OPERATIONS",
//                Id = ROLE3_ID,
//                ConcurrencyStamp = ROLE3_ID
//            });


//            var appUser1 = new ApplicationUser
//            {
//                Id = appUserAId,
//                Email = "gdevol417@gmail.com",
//                EmailConfirmed = true,
//                FirstName = "George",
//                LastName = "Devol",
//                UserName = "+256709958370",
//                PhoneNumber = "+256709958370",
//                NormalizedEmail = "GDEVOL417@GMAIL.COM",
//                NormalizedUserName = "+256709958370",
//                SecurityStamp = Guid.NewGuid().ToString()
//            };
//            //set user password
//            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
//            appUser1.PasswordHash = ph.HashPassword(appUser1, "GDevol@1");

//            //seed user
//            modelBuilder.Entity<ApplicationUser>().HasData(appUser1);
//            // modelBuilder.Entity<IdentityUser>().HasData(appUser);

//            //set user role to admin
//            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
//            {
//                RoleId = ROLE1_ID,
//                UserId = appUserAId
//            });


//            var appUser2 = new ApplicationUser
//            {
//                Id = appUserBId,
//                Email = "canabill@gmail.com",
//                EmailConfirmed = true,
//                FirstName = "Canada",
//                LastName = "Bill",
//                UserName = "+256726186350",
//                PhoneNumber = "+256726186350",
//                NormalizedEmail = "CANADABILL@GMAIL.COM",
//                NormalizedUserName = "+256726186350",
//                SecurityStamp = Guid.NewGuid().ToString()
//             ,

//            };
//            //set user password
//            PasswordHasher<ApplicationUser> ph1 = new PasswordHasher<ApplicationUser>();
//            appUser2.PasswordHash = ph1.HashPassword(appUser2, "Canada@1");

//            //seed user
//            modelBuilder.Entity<ApplicationUser>().HasData(appUser2);
//            // modelBuilder.Entity<IdentityUser>().HasData(appUser);


//            //set user role to admin
//            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
//            {
//                RoleId = ROLE3_ID,
//                UserId = appUserBId
//            });
//            var appUser3 = new ApplicationUser
//            {
//                Id = appUserCId,
//                Email = "monorib@gmail.com",
//                EmailConfirmed = false,
//                FirstName = "Monorib",
//                LastName = "Admin",
//                UserName = "+256777471583",
//                PhoneNumber = "+256777471583",
//                NormalizedEmail = "MONORIB@GMAIL.COM",
//                NormalizedUserName = "+256777471583",
//                SecurityStamp = Guid.NewGuid().ToString()

//            };
//            //set user password
//            PasswordHasher<ApplicationUser> ph2 = new PasswordHasher<ApplicationUser>();
//            appUser3.PasswordHash = ph2.HashPassword(appUser3, "Admin@Monorib@1");

//            //seed user
//            modelBuilder.Entity<ApplicationUser>().HasData(appUser3);
//            // modelBuilder.Entity<IdentityUser>().HasData(appUser);


//            //set user role to pastor
//            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
//            {
//                RoleId = ROLE2_ID,
//                UserId = appUserCId
//            });
//            var address = new Address
//            {
//                AddressId = 1,
//                Region = "Main Street",
//                City = "Kampala",
//                Company = "Monorib",
//                Country = "Uganda",
//            };
//            modelBuilder.Entity<Address>().HasData(address);

//            modelBuilder.Entity<UserProfile>().HasData(
//    new UserProfile
//    {
//        UserProfileId = Guid.NewGuid(),
//        IdentityUserId = appUserAId,
//        AddressId = address.AddressId
//    },
//    new UserProfile
//    {
//        UserProfileId = Guid.NewGuid(),
//        IdentityUserId = appUserBId,
//        AddressId = address.AddressId

//    },
//    new UserProfile
//    {
//        UserProfileId = Guid.NewGuid(),
//        IdentityUserId = appUserCId,
//        AddressId = address.AddressId

//    }
//);

//            //modelBuilder.ApplyConfiguration(new LugandaHymnSeedData());
//            //modelBuilder.ApplyConfiguration(new LugandaHymnSlideSeedData());
//            modelBuilder.Entity<Order>()
//             .HasMany(o => o.OrderItems)
//             .WithOne(oi => oi.Order)
//             .OnDelete(DeleteBehavior.Cascade);
//            modelBuilder.Entity<GeneralCategory>()
//         .HasMany(o => o.Categories)
//         .WithOne(oi => oi.GeneralCategory)
//         .OnDelete(DeleteBehavior.Restrict);

//            modelBuilder.Entity<Product>()
//    .HasOne(p => p.SubCategoryCategory)
//    .WithMany(sc => sc.Products)
//    .HasForeignKey(p => p.SubCategoryCategoryId)
//       .OnDelete(DeleteBehavior.NoAction);
//            modelBuilder.Entity<Trade>()
//  .HasOne(p => p.SubCategoryCategory)
//  .WithMany(sc => sc.Trades)
//  .HasForeignKey(p => p.SubCategoryCategoryId)
//     .OnDelete(DeleteBehavior.NoAction);

//            modelBuilder.Entity<Product>()
//      .HasOne(p => p.SubCategory)
//      .WithMany(sc => sc.Products)
//      .HasForeignKey(p => p.SubCategoryId)
//         .OnDelete(DeleteBehavior.NoAction);
//            modelBuilder.Entity<Trade>()
//      .HasOne(p => p.SubCategory)
//      .WithMany(sc => sc.Trades)
//      .HasForeignKey(p => p.SubCategoryId)
//         .OnDelete(DeleteBehavior.NoAction);

//            modelBuilder.Entity<OrderItem>().HasOne(o => o.Order).WithMany(oi => oi.OrderItems).HasForeignKey(fk => fk.OrderId).OnDelete(DeleteBehavior.Cascade);



//            modelBuilder.Entity<GroupMember>(entity =>
//            {
//                entity.HasKey(gm => gm.GroupMemberId);

//                entity.HasOne(gm => gm.Group)
//                    .WithMany(g => g.Members)
//                    .HasForeignKey(gm => gm.UserGroupId)
//                    .OnDelete(DeleteBehavior.Restrict);

//                entity.HasOne(gm => gm.UserProfile)
//                    .WithMany(up => up.GroupMemberships)
//                    .HasForeignKey(gm => gm.UserProfileId)
//                    .OnDelete(DeleteBehavior.Restrict);

//                entity.HasOne(gm => gm.GroupRole)
//                    .WithMany()
//                    .HasForeignKey(gm => gm.GroupRoleId);

//                entity.HasOne(gm => gm.MemberStatus)
//                    .WithMany()
//                    .HasForeignKey(gm => gm.MemberStatusId);
//            });


//            modelBuilder.Entity<SellerProfile>().HasMany(sp => sp.SellerRestrictions).WithOne(sr => sr.SellerProfile).HasForeignKey(sr => sr.SellerProfileId).OnDelete(DeleteBehavior.NoAction);
//            modelBuilder.Entity<BuyerProfile>().HasOne(p => p.UserProfile).WithOne(k => k.BuyerProfile).OnDelete(DeleteBehavior.NoAction);
//            modelBuilder.Entity<BuyerProfile>().HasOne(p => p.UserGroup).WithOne(k => k.BuyerProfile).OnDelete(DeleteBehavior.NoAction);
//            modelBuilder.Entity<UserGroup>().HasOne(p => p.BuyerProfile).WithOne(k => k.UserGroup).OnDelete(DeleteBehavior.NoAction);
//            modelBuilder.Entity<UserGroupNotification>().HasOne(p => p.UserProfile).WithOne().OnDelete(DeleteBehavior.NoAction);
//            modelBuilder.Entity<ProductAttributeValue>().HasOne(p => p.CategoryAttribute).WithMany(p => p.ProductAttributeValues).HasForeignKey(k => k.CategoryAttributeId).OnDelete(DeleteBehavior.Cascade);
//            modelBuilder.Entity<TradeAttributeValue>().HasOne(p => p.CategoryAttribute).WithMany(p => p.TradeAttributeValues).HasForeignKey(k => k.CategoryAttributeId).OnDelete(DeleteBehavior.Cascade);


//            modelBuilder.Entity<Trade>().HasMany(sp => sp.Reviews).WithOne(sr => sr.Trade).HasForeignKey(sr => sr.TradeId).OnDelete(DeleteBehavior.NoAction);
//            modelBuilder.Entity<SellerProfile>().HasOne(sp => sp.SellerOffering)
//       .WithMany()
//       .HasForeignKey(sp => sp.SellerOfferingId)
//       .OnDelete(DeleteBehavior.Restrict);

//            modelBuilder.Entity<CategoryAttribute>()
//    .HasIndex(ca => new { ca.CategoryId, ca.AttributeName })
//    .IsUnique();
//            modelBuilder.Entity<ProductAttributeValue>()
//    .HasIndex(pav => new { pav.ProductId, pav.CategoryAttributeId })
//    .IsUnique();

//            modelBuilder.Entity<TradeAttributeValue>()
//.HasIndex(pav => new { pav.TradeId, pav.CategoryAttributeId })
//.IsUnique();
//            modelBuilder.Entity<CategoryAttribute>()
//    .HasOne(ca => ca.AttributeDataType)
//    .WithMany()
//    .HasForeignKey(ca => ca.AttributeDataTypeId)
//    .OnDelete(DeleteBehavior.Restrict);

//            modelBuilder.Entity<Product>()
//                   .HasIndex(p => p.CategoryId);
//            modelBuilder.Entity<Product>()
//                  .HasIndex(p => p.SubCategoryCategoryId);

//            modelBuilder.Entity<Product>()
//                .HasIndex(p => p.SubCategoryId);

//            modelBuilder.Entity<Product>()
//                .HasIndex(p => new { p.CategoryId, p.IsActive, p.IsDeleted, p.Price });

//            modelBuilder.Entity<Product>()
//                .HasIndex(p => p.SellerProfileId);

//            modelBuilder.Entity<Product>()
//                .HasIndex(p => p.CreatedAt);

//            modelBuilder.Entity<Product>()
//                .HasIndex(p => p.Price);






//            modelBuilder.Entity<Trade>()
//               .HasIndex(p => p.CategoryId);
//            modelBuilder.Entity<Trade>()
//                  .HasIndex(p => p.SubCategoryCategoryId);

//            modelBuilder.Entity<Trade>()
//                .HasIndex(p => p.SubCategoryId);

//            modelBuilder.Entity<Trade>()
//                .HasIndex(p => new { p.CategoryId, p.IsActive, p.IsDeleted, p.FixedPrice });

//            modelBuilder.Entity<Trade>()
//                .HasIndex(p => p.SellerProfileId);
//            modelBuilder.Entity<Trade>()
//    .HasIndex(p => p.CreatedAt);


//        }
//        public DbSet<Product> Products { get; set; }
//        public DbSet<ProductImage> ProductImages { get; set; }
//        public DbSet<Category> Categories { get; set; }
//        public DbSet<Review> Reviews { get; set; }
//        public DbSet<UserGroup> UserGroups { get; set; }
//        public DbSet<Order> Orders { get; set; }
//        public DbSet<OrderItem> OrderItems { get; set; }
//        public DbSet<WishList> WishlistItems { get; set; }
//        public DbSet<CartItem> CartItems { get; set; }
//        public DbSet<SubCategory> SubCategories { get; set; }
//        public DbSet<SubCategoryCategory> SubCategoryCategories { get; set; }
//        public DbSet<Address> Addresses { get; set; }
//        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
//        public DbSet<UserProfile> UserProfiles { get; set; }
//        public DbSet<GroupMember> GroupMembers { get; set; }
//        public DbSet<CommodityClass> CommodityClasses { get; set; }
//        public DbSet<UserStatus> UserStatuses { get; set; }
//        public DbSet<SellerPolicy> SellerPolicies { get; set; }
//        public DbSet<GroupCategory> GroupCategories { get; set; }
//        public DbSet<SellerTier> SellerTiers { get; set; }
//        public DbSet<SellerProfile> SellerProfiles { get; set; }
//        public DbSet<BuyerTier> BuyerTiers { get; set; }
//        public DbSet<SellerType> SellerTypes { get; set; }
//        public DbSet<BuyerType> BuyerTypes { get; set; }
//        public DbSet<GroupType> GroupTypes { get; set; }
//        public DbSet<SellerRestriction> SellerRestrictions { get; set; }
//        public DbSet<SellerViolation> SellerViolations { get; set; }
//        public DbSet<UserGroupSeller> UserGroupsSellers { get; set; }
//        public DbSet<UserGroupNotification> UserGroupNotifications { get; set; }
//        public DbSet<AttributeDataType> AttributeDataTypes { get; set; }
//        public DbSet<CategoryAttribute> CategoryAttributes { get; set; }
//        public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
//        public DbSet<UserDevice> UserDevices { get; set; }

//        public DbSet<UserOtp> UserOtps { get; set; }
//        public DbSet<GeneralCategory> GeneralCategories { get; set; }

//        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
//        public DbSet<Trade> Trades { get; set; }
//        public DbSet<TradeAttributeValue> TradeAttributeValues { get; set; }
//        public DbSet<TradeImage> TradeImages { get; set; }
//        public DbSet<SellerOffering> SellerOfferings { get; set; }

//    }
//}
