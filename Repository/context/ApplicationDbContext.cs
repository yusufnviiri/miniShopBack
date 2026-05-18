using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository.context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>()
                .HavePrecision(18, 2);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplySeedData(modelBuilder);
            ConfigureCoreRelationships(modelBuilder);
            ConfigureProduct(modelBuilder);
            ConfigureTrade(modelBuilder);
            ConfigureFilters(modelBuilder);
            ConfigureIndexes(modelBuilder);
            SeedUserData(modelBuilder);
                CreateSlugReference(modelBuilder);
        }
        private void CreateSlugReference(ModelBuilder modelBuilder)
        {


            modelBuilder.HasSequence<long>("ProductSlugSeq")
                .StartsAt(1)
                .IncrementsBy(1);
            modelBuilder.HasSequence<long>("TradeSlugSeq")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<long>("UserSlugSeq")
                .StartsAt(1)
                .IncrementsBy(1);
            modelBuilder.HasSequence<long>("UserGroupSlugSeq")
            .StartsAt(1)
            .IncrementsBy(1);
            modelBuilder.HasSequence<long>("SellerProfileSlugSeq")
.StartsAt(1)
.IncrementsBy(1);
        }
        private void SeedUserData(ModelBuilder modelBuilder)
        {
            string appUserAId = "42cd3af3-f319-4118-a604-4442d487b923";
            string appUserBId = "5f2b8a40-d899-4345-aa3e-7b98712bc112";
            string appUserCId = "bd363936-63d0-4ace-bc46-a2f1348cb61e";
            string appUserDId = "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513";
            string ROLE1_ID = "25153dfe-8a7a-48f3-a2f9-c314232dd6a3";
            string ROLE2_ID = "bb69367c-0380-470b-8337-35644e861587";
            string ROLE3_ID = "1c13657c-0c92-4dec-b308-c41c725a1513";
            string ROLE4_ID = "206cc3ce-9a8a-4360-ce15-08de92fa781e";


            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "platform admin",
                NormalizedName = "PLATFORM ADMIN",
                Id = ROLE1_ID,
                ConcurrencyStamp = ROLE1_ID
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "super admin",
                NormalizedName = "SUPER ADMIN",
                Id = ROLE2_ID,
                ConcurrencyStamp = ROLE2_ID
            });


            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "operations",
                NormalizedName = "OPERATIONS",
                Id = ROLE3_ID,
                ConcurrencyStamp = ROLE3_ID
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "system admin",
                NormalizedName = "SYSTEM ADMIN",
                Id = ROLE4_ID,
                ConcurrencyStamp = ROLE4_ID
            });


            var appUser1 = new ApplicationUser
            {
                Id = appUserAId,
                Email = "gdevol417@gmail.com",
                EmailConfirmed = true,
                FirstName = "George",
                LastName = "Devol",
                UserName = "+256709958370",
                PhoneNumber = "+256709958370",
                NormalizedEmail = "GDEVOL417@GMAIL.COM",
                NormalizedUserName = "+256709958370",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            //set user password
            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            appUser1.PasswordHash = ph.HashPassword(appUser1, "GDevol@1");

            //seed user
            modelBuilder.Entity<ApplicationUser>().HasData(appUser1);
            // modelBuilder.Entity<IdentityUser>().HasData(appUser);

            //set user role to admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE1_ID,
                UserId = appUserAId
            });


            var appUser2 = new ApplicationUser
            {
                Id = appUserBId,
                Email = "canabill@gmail.com",
                EmailConfirmed = true,
                FirstName = "Canada",
                LastName = "Bill",
                UserName = "+256726186350",
                PhoneNumber = "+256726186350",
                NormalizedEmail = "CANADABILL@GMAIL.COM",
                NormalizedUserName = "+256726186350",
                SecurityStamp = Guid.NewGuid().ToString()
             ,

            };
            //set user password
            PasswordHasher<ApplicationUser> ph1 = new PasswordHasher<ApplicationUser>();
            appUser2.PasswordHash = ph1.HashPassword(appUser2, "Canada@1");

            //seed user
            modelBuilder.Entity<ApplicationUser>().HasData(appUser2);
            // modelBuilder.Entity<IdentityUser>().HasData(appUser);


            //set user role to admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE3_ID,
                UserId = appUserBId
            });
            var appUser3 = new ApplicationUser
            {
                Id = appUserCId,
                Email = "monorib@gmail.com",
                EmailConfirmed = false,
                FirstName = "Monorib",
                LastName = "Admin",
                UserName = "+256777471583",
                PhoneNumber = "+256777471583",
                NormalizedEmail = "MONORIB@GMAIL.COM",
                NormalizedUserName = "+256777471583",
                SecurityStamp = Guid.NewGuid().ToString()

            };
            //set user password
            PasswordHasher<ApplicationUser> ph2 = new PasswordHasher<ApplicationUser>();
            appUser3.PasswordHash = ph2.HashPassword(appUser3, "Admin@Monorib@1");

            //seed user
            modelBuilder.Entity<ApplicationUser>().HasData(appUser3);
          
            // modelBuilder.Entity<IdentityUser>().HasData(appUser);


            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE2_ID,
                UserId = appUserCId
            });







            var appUser4 = new ApplicationUser
            {
                Id = appUserDId,
                Email = "chiyiya@gmail.com",
                EmailConfirmed = true,
                FirstName = "Chiyiya",
                LastName = "Yusuf",
                UserName = "+256782909090",
                PhoneNumber = "+256782909090",
                NormalizedEmail = "CHIYIYA@GMAIL.COM",
                NormalizedUserName = "+256782909090",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            //set user password
            PasswordHasher<ApplicationUser> phx = new PasswordHasher<ApplicationUser>();
            appUser4.PasswordHash = phx.HashPassword(appUser4, "Chiyiya@1");

            //seed user
            modelBuilder.Entity<ApplicationUser>().HasData(appUser4);
            // modelBuilder.Entity<IdentityUser>().HasData(appUser);

            //set user role to admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE4_ID,
                UserId = appUserDId
            });







            var address = new Address
            {
                AddressId = 1,
                Region = "Main Street",
                City = "Kampala",
                Company = "Monorib",
                Country = "Uganda",
            };
            modelBuilder.Entity<Address>().HasData(address);

            modelBuilder.Entity<UserProfile>().HasData(
                new UserProfile
                {
                    UserProfileId = Guid.Parse("220cc6eb-238a-424d-962b-2dca18f731d1"),
                    IdentityUserId = appUserAId,
                    AddressId = address.AddressId
                },
                new UserProfile
                {
                    UserProfileId = Guid.Parse("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                    IdentityUserId = appUserBId,
                    AddressId = address.AddressId
                },
                new UserProfile
                {
                    UserProfileId = Guid.Parse("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                    IdentityUserId = appUserCId,
                    AddressId = address.AddressId
                },
                new UserProfile
                {
                    UserProfileId = Guid.Parse("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                    IdentityUserId = appUserDId,
                    AddressId = address.AddressId
                }
            );
        }

        #region 🔹 Seed Data
        private void ApplySeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GroupRoleSeedData());
            modelBuilder.ApplyConfiguration(new GroupTypeDataSeed());
            modelBuilder.ApplyConfiguration(new MemberStatusSeedData());
            modelBuilder.ApplyConfiguration(new CommodityClassSeedData());
            modelBuilder.ApplyConfiguration(new UserStatusSeedData());
            modelBuilder.ApplyConfiguration(new SellerPolicySeedData());
            modelBuilder.ApplyConfiguration(new GroupCategorySeedData());
            modelBuilder.ApplyConfiguration(new SellerTierSeedData());
            modelBuilder.ApplyConfiguration(new BuyerTierSeedData());
            modelBuilder.ApplyConfiguration(new SellerTypeSeedData());
            modelBuilder.ApplyConfiguration(new SellerOfferingSeedData());
            modelBuilder.ApplyConfiguration(new AttributeDataTypeSeedData());
            modelBuilder.ApplyConfiguration(new GeneralCategorySeedData());
            modelBuilder.ApplyConfiguration(new BookingStatusSeedData());

            CategoryMigrationDataSeeder.Seed(modelBuilder);
            CategoryAttributeSeedData.Seed(modelBuilder);
        }
        #endregion

        #region 🔹 Core Relationships
        private void ConfigureCoreRelationships(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TradeReview>(entity =>
            {
                entity.HasKey(x => x.TradeReviewId);

                entity.HasOne(x => x.Trade)
                    .WithMany(t => t.TradeReviews)
                    .HasForeignKey(x => x.TradeId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Reviewer)
                    .WithMany(u => u.TradeReviews)
                    .HasForeignKey(x => x.UserProfileId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductReview>(entity =>
            {
                entity.HasKey(x => x.ProductReviewId);

                entity.HasOne(x => x.Product)
                    .WithMany(p => p.ProductReviews)
                    .HasForeignKey(x => x.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Reviewer)
                    .WithMany(u => u.ProductReviews)
                    .HasForeignKey(x => x.UserProfileId)
                    .OnDelete(DeleteBehavior.Restrict); 
            });

            modelBuilder.Entity<ProductReview>()
    .HasIndex(x => new { x.ProductId, x.UserProfileId })
    .IsUnique();

            modelBuilder.Entity<TradeReview>()
                .HasIndex(x => new { x.TradeId, x.UserProfileId })
                .IsUnique();

            modelBuilder.Entity<UserPreferenceCategory>()
    .ToTable("UserPreferenceCategories")
    .HasKey(x => new { x.UserPreferenceId, x.CategoryId });

            modelBuilder.Entity<UserPreferenceCategory>()
                .HasOne(x => x.UserPreference)
                .WithMany(x => x.UserPreferenceCategories)
                .HasForeignKey(x => x.UserPreferenceId);

            modelBuilder.Entity<UserPreferenceCategory>()
                .HasOne(x => x.Category)
                .WithMany(x => x.UserPreferenceCategories)
                .HasForeignKey(x => x.CategoryId);


            modelBuilder.Entity<UserPreferenceSellerProfile>()
    .ToTable("UserPreferenceSellerProfiles")
    .HasKey(x => new { x.UserPreferenceId, x.SellerProfileId });

            modelBuilder.Entity<UserPreferenceSellerProfile>()
                .HasOne(x => x.UserPreference)
                .WithMany(x => x.UserPreferenceSellerProfiles)
                .HasForeignKey(x => x.UserPreferenceId);

            modelBuilder.Entity<UserPreferenceSellerProfile>()
                .HasOne(x => x.SellerProfile)
                .WithMany(x => x.UserPreferenceSellerProfiles)
                .HasForeignKey(x => x.SellerProfileId);

            modelBuilder.Entity<UserPreference>()
    .HasOne(up => up.UserProfile)
    .WithOne()
    .HasForeignKey<UserPreference>(up => up.UserProfileId);



            modelBuilder.Entity<UserGroupNotification>()
               .HasOne(x => x.UserProfile)
               .WithMany()
               .HasForeignKey(x => x.UserProfileId)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<GroupSeller>()
              .HasOne(x => x.Member)
              .WithMany(p=>p.GroupSellers)
              .OnDelete(DeleteBehavior.SetNull);
         
            modelBuilder.Entity<GeneralCategory>()
                .HasMany(g => g.Categories)
                .WithOne(c => c.GeneralCategory)
                .HasForeignKey(c => c.GeneralCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<GroupMember>()
              .HasMany(o => o.GroupSellers)
              .WithOne(i => i.Member)
              .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<HomePageCard>()
          .HasMany(o => o.CategoryLinks)
          .WithOne(i => i.HomePageCard)
          .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupMember>(entity =>
            {
                entity.HasKey(x => x.GroupMemberId);

                entity.HasOne(x => x.Group)
                    .WithMany(g => g.Members)
                    .HasForeignKey(x => x.UserGroupId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.UserProfile)
                    .WithMany(p => p.GroupMemberships)
                    .HasForeignKey(x => x.UserProfileId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
        #endregion

        #region 🔹 Product Config
        private void ConfigureProduct(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Product>();

            entity.HasKey(p => p.ProductId);

            entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.SubCategory)
                .WithMany(p=>p.Products)
                .HasForeignKey(p => p.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.SubCategoryCategory)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.SubCategoryCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.SellerProfile)
                .WithMany(k=>k.Products)
                .HasForeignKey(p => p.SellerProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(p => p.Images)
                .WithOne(p=>p.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

         

            //entity.HasMany(p => p.ProductAttributeValues)
            //    .WithOne()
            //    .HasForeignKey(v => v.ProductId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
        #endregion

        #region 🔹 Trade Config
        private void ConfigureTrade(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Trade>();

            entity.HasKey(t => t.TradeId);

            entity.HasOne(t => t.Category)
                .WithMany(c => c.Trades)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.SubCategory)
                .WithMany(t=>t.Trades)
                .HasForeignKey(t => t.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.SubCategoryCategory)
                .WithMany(t => t.Trades)
                .HasForeignKey(t => t.SubCategoryCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.SellerProfile)
                .WithMany(t => t.Trades)
                .HasForeignKey(t => t.SellerProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(t => t.Images)
                .WithOne(t=>t.Trade)
                .HasForeignKey(i => i.TradeId)
                .OnDelete(DeleteBehavior.Cascade);


            //entity.HasMany(t => t.TradeAttributeValues)
            //    .WithOne()
            //    .HasForeignKey(v => v.TradeId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
        #endregion

        #region 🔹 Query Filters
        private void ConfigureFilters(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>()
            //    .HasQueryFilter(p => !p.IsDeleted);

            //modelBuilder.Entity<Trade>()
            //    .HasQueryFilter(t => !t.IsDeleted);

            modelBuilder.Entity<OrderItem>()
                .HasQueryFilter(o => !o.Product.IsDeleted);
        }
        #endregion

        #region 🔹 Indexes
        private void ConfigureIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.CategoryId, p.IsActive, p.IsDeleted, p.Price });

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.CreatedAt);

            //modelBuilder.Entity<Trade>()
            //    .HasIndex(t => new { t.CategoryId, t.IsActive, t.IsDeleted, t.FixedPrice });

            modelBuilder.Entity<Trade>()
                .HasIndex(t => t.CreatedAt);

            modelBuilder.Entity<CategoryAttribute>()
                .HasIndex(c => new { c.CategoryId, c.AttributeName })
                .IsUnique();

            modelBuilder.Entity<ProductAttributeValue>()
                .HasIndex(p => new { p.ProductId, p.CategoryAttributeId })
                .IsUnique();

            modelBuilder.Entity<TradeAttributeValue>()
                .HasIndex(t => new { t.TradeId, t.CategoryAttributeId })
                .IsUnique();
        }
        #endregion

        #region 🔹 DbSets
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductReview> ProductReviews  { get; set; }
        public DbSet<TradeReview> TradeReviews  { get; set; }

        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<WishList> WishlistItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<SubCategoryCategory> SubCategoryCategories { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<CommodityClass> CommodityClasses { get; set; }
        public DbSet<UserStatus> UserStatuses { get; set; }
        public DbSet<SellerPolicy> SellerPolicies { get; set; }
        public DbSet<GroupCategory> GroupCategories { get; set; }
        public DbSet<SellerTier> SellerTiers { get; set; }
        public DbSet<SellerProfile> SellerProfiles { get; set; }
        public DbSet<BuyerTier> BuyerTiers { get; set; }
        public DbSet<SellerType> SellerTypes { get; set; }
        public DbSet<BuyerType> BuyerTypes { get; set; }
        public DbSet<GroupType> GroupTypes { get; set; }
        public DbSet<SellerRestriction> SellerRestrictions { get; set; }
        public DbSet<SellerViolation> SellerViolations { get; set; }
        public DbSet<UserGroupSeller> UserGroupsSellers { get; set; }
        public DbSet<UserGroupNotification> UserGroupNotifications { get; set; }
        public DbSet<AttributeDataType> AttributeDataTypes { get; set; }
        public DbSet<CategoryAttribute> CategoryAttributes { get; set; }
        public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
        public DbSet<UserDevice> UserDevices { get; set; }
        public DbSet<UserOtp> UserOtps { get; set; }
        public DbSet<GeneralCategory> GeneralCategories { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<TradeAttributeValue> TradeAttributeValues { get; set; }
        public DbSet<TradeImage> TradeImages { get; set; }
        public DbSet<SellerOffering> SellerOfferings { get; set; }
        public DbSet<GroupSeller> GroupSellers  { get; set; }
        public DbSet<HomePageCardCategory> HomePageCardCategories { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }
        public DbSet<UserPreferenceCategory> UserPreferenceCategories { get; set; }
        public DbSet<UserPreferenceSellerProfile> UserPreferenceSellerProfiles { get; set; }
        public DbSet<TradeImpression> TradeImpressions { get; set; }
        public DbSet<ProductImpression> ProductImpressions { get; set; }
        public DbSet<HomePageCard> HomePageCards { get; set; }
        public DbSet<GroupFeaturedProduct> GroupFeaturedProducts { get; set; }
        


        #endregion
    }
}