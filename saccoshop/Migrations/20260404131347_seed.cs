using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneNumberVerified = table.Column<bool>(type: "bit", nullable: false),
                    AccountConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    MfaEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecoveryPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecoveryQuestion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecoveryAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributeDataTypes",
                columns: table => new
                {
                    AttributeDataTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeDataTypes", x => x.AttributeDataTypeId);
                });

            migrationBuilder.CreateTable(
                name: "BookingStatus",
                columns: table => new
                {
                    BookingStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingStatus", x => x.BookingStatusId);
                });

            migrationBuilder.CreateTable(
                name: "BuyerTiers",
                columns: table => new
                {
                    BuyerTierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerTiers", x => x.BuyerTierId);
                });

            migrationBuilder.CreateTable(
                name: "BuyerTypes",
                columns: table => new
                {
                    BuyerTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerTypes", x => x.BuyerTypeId);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItemId);
                });

            migrationBuilder.CreateTable(
                name: "CommodityClasses",
                columns: table => new
                {
                    CommodityClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommodityClassName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommodityClasses", x => x.CommodityClassId);
                });

            migrationBuilder.CreateTable(
                name: "GeneralCategories",
                columns: table => new
                {
                    GeneralCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeneralCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralCategories", x => x.GeneralCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "GroupCategories",
                columns: table => new
                {
                    GroupCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupCategories", x => x.GroupCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "GroupRole",
                columns: table => new
                {
                    GroupRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupRole", x => x.GroupRoleId);
                });

            migrationBuilder.CreateTable(
                name: "GroupTypes",
                columns: table => new
                {
                    GroupTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTypes", x => x.GroupTypeId);
                });

            migrationBuilder.CreateTable(
                name: "MemberStatus",
                columns: table => new
                {
                    MemberStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberStatus", x => x.MemberStatusId);
                });

            migrationBuilder.CreateTable(
                name: "SellerOfferings",
                columns: table => new
                {
                    SellerOfferingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerOfferingName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerOfferings", x => x.SellerOfferingId);
                });

            migrationBuilder.CreateTable(
                name: "SellerPolicies",
                columns: table => new
                {
                    SellerPolicyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerPolicyName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerPolicies", x => x.SellerPolicyId);
                });

            migrationBuilder.CreateTable(
                name: "SellerTiers",
                columns: table => new
                {
                    SellerTierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerTierDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerTiers", x => x.SellerTierId);
                });

            migrationBuilder.CreateTable(
                name: "SellerTypes",
                columns: table => new
                {
                    SellerTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerTypes", x => x.SellerTypeId);
                });

            migrationBuilder.CreateTable(
                name: "SellerViolations",
                columns: table => new
                {
                    SellerViolationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerViolations", x => x.SellerViolationId);
                });

            migrationBuilder.CreateTable(
                name: "UserStatuses",
                columns: table => new
                {
                    UserStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatuses", x => x.UserStatusId);
                });

            migrationBuilder.CreateTable(
                name: "WishlistItems",
                columns: table => new
                {
                    WishListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistItems", x => x.WishListId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDevices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTrusted = table.Column<bool>(type: "bit", nullable: false),
                    FirstSeenAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastSeenAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDevices_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOtps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CodeHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttemptCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Used = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOtps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOtps_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TokenHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeviceId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryAttributes",
                columns: table => new
                {
                    CategoryAttributeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    AttributeDataTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryAttributes", x => x.CategoryAttributeId);
                    table.ForeignKey(
                        name: "FK_CategoryAttributes_AttributeDataTypes_AttributeDataTypeId",
                        column: x => x.AttributeDataTypeId,
                        principalTable: "AttributeDataTypes",
                        principalColumn: "AttributeDataTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralCategoryId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_GeneralCategories_GeneralCategoryId",
                        column: x => x.GeneralCategoryId,
                        principalTable: "GeneralCategories",
                        principalColumn: "GeneralCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SellerProfiles",
                columns: table => new
                {
                    SellerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerOfferingId = table.Column<int>(type: "int", nullable: false),
                    SellerTypeId = table.Column<int>(type: "int", nullable: false),
                    SellerPolicyId = table.Column<int>(type: "int", nullable: false),
                    SellerTierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerProfiles", x => x.SellerProfileId);
                    table.ForeignKey(
                        name: "FK_SellerProfiles_SellerOfferings_SellerOfferingId",
                        column: x => x.SellerOfferingId,
                        principalTable: "SellerOfferings",
                        principalColumn: "SellerOfferingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SellerProfiles_SellerPolicies_SellerPolicyId",
                        column: x => x.SellerPolicyId,
                        principalTable: "SellerPolicies",
                        principalColumn: "SellerPolicyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SellerProfiles_SellerTypes_SellerTypeId",
                        column: x => x.SellerTypeId,
                        principalTable: "SellerTypes",
                        principalColumn: "SellerTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeValues",
                columns: table => new
                {
                    ProductAttributeValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CategoryAttributeId = table.Column<int>(type: "int", nullable: false),
                    StringValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntValue = table.Column<int>(type: "int", nullable: true),
                    DecimalValue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    BoolValue = table.Column<bool>(type: "bit", nullable: true),
                    DateValue = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeValues", x => x.ProductAttributeValueId);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValues_CategoryAttributes_CategoryAttributeId",
                        column: x => x.CategoryAttributeId,
                        principalTable: "CategoryAttributes",
                        principalColumn: "CategoryAttributeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradeAttributeValues",
                columns: table => new
                {
                    TradeAttributeValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryAttributeId = table.Column<int>(type: "int", nullable: false),
                    StringValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntValue = table.Column<int>(type: "int", nullable: true),
                    DecimalValue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    BoolValue = table.Column<bool>(type: "bit", nullable: true),
                    DateValue = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeAttributeValues", x => x.TradeAttributeValueId);
                    table.ForeignKey(
                        name: "FK_TradeAttributeValues_CategoryAttributes_CategoryAttributeId",
                        column: x => x.CategoryAttributeId,
                        principalTable: "CategoryAttributes",
                        principalColumn: "CategoryAttributeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    SubCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.SubCategoryId);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SellerRestrictions",
                columns: table => new
                {
                    SellerRestrictionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerRestrictions", x => x.SellerRestrictionId);
                    table.ForeignKey(
                        name: "FK_SellerRestrictions_SellerProfiles_SellerProfileId",
                        column: x => x.SellerProfileId,
                        principalTable: "SellerProfiles",
                        principalColumn: "SellerProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    UserGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserStatusId = table.Column<int>(type: "int", nullable: false),
                    GroupCategoryId = table.Column<int>(type: "int", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    SellerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.UserGroupId);
                    table.ForeignKey(
                        name: "FK_UserGroups_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroups_GroupCategories_GroupCategoryId",
                        column: x => x.GroupCategoryId,
                        principalTable: "GroupCategories",
                        principalColumn: "GroupCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroups_GroupTypes_GroupTypeId",
                        column: x => x.GroupTypeId,
                        principalTable: "GroupTypes",
                        principalColumn: "GroupTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroups_SellerProfiles_SellerProfileId",
                        column: x => x.SellerProfileId,
                        principalTable: "SellerProfiles",
                        principalColumn: "SellerProfileId");
                    table.ForeignKey(
                        name: "FK_UserGroups_UserStatuses_UserStatusId",
                        column: x => x.UserStatusId,
                        principalTable: "UserStatuses",
                        principalColumn: "UserStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategoryCategories",
                columns: table => new
                {
                    SubCategoryCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoryCategories", x => x.SubCategoryCategoryId);
                    table.ForeignKey(
                        name: "FK_SubCategoryCategories_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "SubCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuyerProfile",
                columns: table => new
                {
                    BuyerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseLimit = table.Column<int>(type: "int", nullable: false),
                    BuyerTierId = table.Column<int>(type: "int", nullable: false),
                    BuyerTypeId = table.Column<int>(type: "int", nullable: false),
                    UserGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerProfile", x => x.BuyerProfileId);
                    table.ForeignKey(
                        name: "FK_BuyerProfile_BuyerTiers_BuyerTierId",
                        column: x => x.BuyerTierId,
                        principalTable: "BuyerTiers",
                        principalColumn: "BuyerTierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuyerProfile_BuyerTypes_BuyerTypeId",
                        column: x => x.BuyerTypeId,
                        principalTable: "BuyerTypes",
                        principalColumn: "BuyerTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuyerProfile_UserGroups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroups",
                        principalColumn: "UserGroupId");
                });

            migrationBuilder.CreateTable(
                name: "UserGroupsSellers",
                columns: table => new
                {
                    UserGroupSellerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroupsSellers", x => x.UserGroupSellerId);
                    table.ForeignKey(
                        name: "FK_UserGroupsSellers_SellerProfiles_SellerProfileId",
                        column: x => x.SellerProfileId,
                        principalTable: "SellerProfiles",
                        principalColumn: "SellerProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroupsSellers_UserGroups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroups",
                        principalColumn: "UserGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryCategoryId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OldPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    HasImage = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommodityClassId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_SellerProfiles_SellerProfileId",
                        column: x => x.SellerProfileId,
                        principalTable: "SellerProfiles",
                        principalColumn: "SellerProfileId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "SubCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_SubCategoryCategories_SubCategoryCategoryId",
                        column: x => x.SubCategoryCategoryId,
                        principalTable: "SubCategoryCategories",
                        principalColumn: "SubCategoryCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    TradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TradeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryCategoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    HasImage = table.Column<bool>(type: "bit", nullable: false),
                    IsModified = table.Column<bool>(type: "bit", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    SellerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommodityClassId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => x.TradeId);
                    table.ForeignKey(
                        name: "FK_Trades_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trades_SellerProfiles_SellerProfileId",
                        column: x => x.SellerProfileId,
                        principalTable: "SellerProfiles",
                        principalColumn: "SellerProfileId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trades_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "SubCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trades_SubCategoryCategories_SubCategoryCategoryId",
                        column: x => x.SubCategoryCategoryId,
                        principalTable: "SubCategoryCategories",
                        principalColumn: "SubCategoryCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    ActiveGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserStatusId = table.Column<int>(type: "int", nullable: false),
                    BuyerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SellerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.UserProfileId);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfiles_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfiles_BuyerProfile_BuyerProfileId",
                        column: x => x.BuyerProfileId,
                        principalTable: "BuyerProfile",
                        principalColumn: "BuyerProfileId");
                    table.ForeignKey(
                        name: "FK_UserProfiles_SellerProfiles_SellerProfileId",
                        column: x => x.SellerProfileId,
                        principalTable: "SellerProfiles",
                        principalColumn: "SellerProfileId");
                    table.ForeignKey(
                        name: "FK_UserProfiles_UserStatuses_UserStatusId",
                        column: x => x.UserStatusId,
                        principalTable: "UserStatuses",
                        principalColumn: "UserStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    ProductImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    Folder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.ProductImageId);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishListItem",
                columns: table => new
                {
                    WishListItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WishListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishListItem", x => x.WishListItemId);
                    table.ForeignKey(
                        name: "FK_WishListItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishListItem_WishlistItems_WishListId",
                        column: x => x.WishListId,
                        principalTable: "WishlistItems",
                        principalColumn: "WishListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Trades_TradeId",
                        column: x => x.TradeId,
                        principalTable: "Trades",
                        principalColumn: "TradeId");
                });

            migrationBuilder.CreateTable(
                name: "TradeImages",
                columns: table => new
                {
                    TradeImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    Folder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeImages", x => x.TradeImageId);
                    table.ForeignKey(
                        name: "FK_TradeImages_Trades_TradeId",
                        column: x => x.TradeId,
                        principalTable: "Trades",
                        principalColumn: "TradeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupMembers",
                columns: table => new
                {
                    GroupMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupRoleId = table.Column<int>(type: "int", nullable: false),
                    MemberStatusId = table.Column<int>(type: "int", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMembers", x => x.GroupMemberId);
                    table.ForeignKey(
                        name: "FK_GroupMembers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupMembers_GroupRole_GroupRoleId",
                        column: x => x.GroupRoleId,
                        principalTable: "GroupRole",
                        principalColumn: "GroupRoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMembers_MemberStatus_MemberStatusId",
                        column: x => x.MemberStatusId,
                        principalTable: "MemberStatus",
                        principalColumn: "MemberStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMembers_UserGroups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroups",
                        principalColumn: "UserGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupMembers_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "UserProfileId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinationAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStatusDetails = table.Column<int>(type: "int", nullable: false),
                    DeliveryFee = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PayementMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Payment = table.Column<int>(type: "int", nullable: false),
                    PlacedByProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_Orders_UserGroups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroups",
                        principalColumn: "UserGroupId");
                    table.ForeignKey(
                        name: "FK_Orders_UserProfiles_PlacedByProfileId",
                        column: x => x.PlacedByProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "UserProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradeBooking",
                columns: table => new
                {
                    TradeBookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateBooked = table.Column<DateOnly>(type: "date", nullable: false),
                    AgreedPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsHourly = table.Column<bool>(type: "bit", nullable: false),
                    HoursBooked = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    BookingStatusId = table.Column<int>(type: "int", nullable: false),
                    CancellationReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancelledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeBooking", x => x.TradeBookingId);
                    table.ForeignKey(
                        name: "FK_TradeBooking_BookingStatus_BookingStatusId",
                        column: x => x.BookingStatusId,
                        principalTable: "BookingStatus",
                        principalColumn: "BookingStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TradeBooking_Trades_TradeId",
                        column: x => x.TradeId,
                        principalTable: "Trades",
                        principalColumn: "TradeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TradeBooking_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "UserProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroupNotifications",
                columns: table => new
                {
                    UserGroupNotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroupNotifications", x => x.UserGroupNotificationId);
                    table.ForeignKey(
                        name: "FK_UserGroupNotifications_UserGroups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroups",
                        principalColumn: "UserGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroupNotifications_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "UserProfileId");
                });

            migrationBuilder.CreateTable(
                name: "GroupSellers",
                columns: table => new
                {
                    GroupSellerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupSellers", x => x.GroupSellerId);
                    table.ForeignKey(
                        name: "FK_GroupSellers_GroupMembers_GroupMemberId",
                        column: x => x.GroupMemberId,
                        principalTable: "GroupMembers",
                        principalColumn: "GroupMemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupSellers_SellerProfiles_SellerProfileId",
                        column: x => x.SellerProfileId,
                        principalTable: "SellerProfiles",
                        principalColumn: "SellerProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderItemTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressId", "City", "Company", "Country", "Region" },
                values: new object[] { 1, "Kampala", "Monorib", "Uganda", "Main Street" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c13657c-0c92-4dec-b308-c41c725a1513", "1c13657c-0c92-4dec-b308-c41c725a1513", "operations", "OPERATIONS" },
                    { "25153dfe-8a7a-48f3-a2f9-c314232dd6a3", "25153dfe-8a7a-48f3-a2f9-c314232dd6a3", "platform admin", "PLATFORM ADMIN" },
                    { "bb69367c-0380-470b-8337-35644e861587", "bb69367c-0380-470b-8337-35644e861587", "super admin", "SUPER ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountConfirmed", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MfaEnabled", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhoneNumberVerified", "RecoveryAnswer", "RecoveryPhoneNumber", "RecoveryQuestion", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "42cd3af3-f319-4118-a604-4442d487b923", 0, false, "6e78e0e2-2d75-4163-a4d2-6ba4951cd682", new DateTime(2026, 4, 4, 13, 13, 45, 954, DateTimeKind.Utc).AddTicks(6882), "gdevol417@gmail.com", true, "George", "Devol", false, null, false, "GDEVOL417@GMAIL.COM", "+256709958370", "AQAAAAIAAYagAAAAEIlpuL9OzSfvkzrnhC7TsJWcaM1FdQU4HiewXnSnD2yDsoqppwEgMxkkhiCF2YGvUA==", "+256709958370", false, false, "", "", "", "4f93a028-31ee-48f1-9833-583a3b6e6bb9", false, "+256709958370" },
                    { "5f2b8a40-d899-4345-aa3e-7b98712bc112", 0, false, "20df1c70-a634-4e11-af9f-60ec36f3f2b3", new DateTime(2026, 4, 4, 13, 13, 46, 62, DateTimeKind.Utc).AddTicks(5419), "canabill@gmail.com", true, "Canada", "Bill", false, null, false, "CANADABILL@GMAIL.COM", "+256726186350", "AQAAAAIAAYagAAAAEEa0mn6GoU9NmoLkRUUvfbESQqU2aLQb6Lo4atKDryldNAIv+Z+6AUyfW4DGkS/buA==", "+256726186350", false, false, "", "", "", "e667172e-ed49-4c20-b6f4-57eff0e3258d", false, "+256726186350" },
                    { "bd363936-63d0-4ace-bc46-a2f1348cb61e", 0, false, "309317a8-bb3f-4808-ab40-052483c14395", new DateTime(2026, 4, 4, 13, 13, 46, 163, DateTimeKind.Utc).AddTicks(6276), "monorib@gmail.com", false, "Monorib", "Admin", false, null, false, "MONORIB@GMAIL.COM", "+256777471583", "AQAAAAIAAYagAAAAELs6yis7qXARutliuIBs35YZYkAlzrVkH4i3pvuBlVLzLrnXJLObiD38p039C4O0Uw==", "+256777471583", false, false, "", "", "", "192ee46c-8a4f-4aa8-968c-716ea3c63a8c", false, "+256777471583" }
                });

            migrationBuilder.InsertData(
                table: "AttributeDataTypes",
                columns: new[] { "AttributeDataTypeId", "DataTypeName" },
                values: new object[,]
                {
                    { 1, "String" },
                    { 2, "Int" },
                    { 3, "Decimal" },
                    { 4, "Bool" },
                    { 5, "Date" }
                });

            migrationBuilder.InsertData(
                table: "BookingStatus",
                columns: new[] { "BookingStatusId", "Status" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Accepted" },
                    { 3, "Rejected" },
                    { 4, "InProgress" },
                    { 5, "Completed" },
                    { 6, "Cancelled" },
                    { 7, "Disputed" }
                });

            migrationBuilder.InsertData(
                table: "BuyerTiers",
                columns: new[] { "BuyerTierId", "Description" },
                values: new object[,]
                {
                    { 1, "REGULAR" },
                    { 2, "VIP" },
                    { 3, "INSTITUTIONAL" }
                });

            migrationBuilder.InsertData(
                table: "CommodityClasses",
                columns: new[] { "CommodityClassId", "CommodityClassName" },
                values: new object[,]
                {
                    { 1, "Lower Class" },
                    { 2, "Middle Class" },
                    { 3, "Upper Class" }
                });

            migrationBuilder.InsertData(
                table: "GeneralCategories",
                columns: new[] { "GeneralCategoryId", "GeneralCategoryName" },
                values: new object[,]
                {
                    { 1, "Phones and Gadgets" },
                    { 2, "Computers" },
                    { 3, "Men's wear" },
                    { 4, "Women's wear" },
                    { 5, "Kids fashion" },
                    { 6, "Brands" },
                    { 7, "Home" },
                    { 8, "Bags" },
                    { 9, "Appliances" },
                    { 10, "Others" },
                    { 11, "Health and Beauty" },
                    { 12, "Trades" },
                    { 13, "Software" },
                    { 14, "Food & Agriculture" },
                    { 15, "Property" },
                    { 16, "Fashion & Apparel" },
                    { 17, "Electronics & Home Entertainment" },
                    { 18, "Food & Beverages" }
                });

            migrationBuilder.InsertData(
                table: "GroupCategories",
                columns: new[] { "GroupCategoryId", "Description" },
                values: new object[,]
                {
                    { 1, "ECONOMY" },
                    { 2, "MIDDLE" },
                    { 3, "PREMIUM" }
                });

            migrationBuilder.InsertData(
                table: "GroupRole",
                columns: new[] { "GroupRoleId", "Description" },
                values: new object[,]
                {
                    { 1, "Member" },
                    { 2, "Treasurer" },
                    { 3, "Secretary" },
                    { 4, "Chairperson" },
                    { 5, "Admin" },
                    { 6, "Auditor" }
                });

            migrationBuilder.InsertData(
                table: "GroupTypes",
                columns: new[] { "GroupTypeId", "Description" },
                values: new object[,]
                {
                    { 1, "Saving Cooperative" },
                    { 2, "Company" },
                    { 3, "Church" },
                    { 4, "IslamicInstitution" },
                    { 5, "School" },
                    { 6, "NGO" }
                });

            migrationBuilder.InsertData(
                table: "MemberStatus",
                columns: new[] { "MemberStatusId", "Description" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Suspended" },
                    { 3, "Removed" }
                });

            migrationBuilder.InsertData(
                table: "SellerOfferings",
                columns: new[] { "SellerOfferingId", "SellerOfferingName" },
                values: new object[,]
                {
                    { 1, "Product Seller" },
                    { 2, "Service Provider" }
                });

            migrationBuilder.InsertData(
                table: "SellerPolicies",
                columns: new[] { "SellerPolicyId", "SellerPolicyName" },
                values: new object[,]
                {
                    { 1, "can_sell_lower" },
                    { 2, "can_sell_middle" },
                    { 3, "can_sell_upper" },
                    { 4, "can_sell_everywhere" }
                });

            migrationBuilder.InsertData(
                table: "SellerTiers",
                columns: new[] { "SellerTierId", "SellerTierDescription" },
                values: new object[,]
                {
                    { 1, "BASIC" },
                    { 2, "TRUSTED" },
                    { 3, "VERIFIED" },
                    { 4, "RESTRICTED" }
                });

            migrationBuilder.InsertData(
                table: "SellerTypes",
                columns: new[] { "SellerTypeId", "SellerTypeName" },
                values: new object[,]
                {
                    { 1, "Individual" },
                    { 2, "Group" }
                });

            migrationBuilder.InsertData(
                table: "UserStatuses",
                columns: new[] { "UserStatusId", "StatusName" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Suspended" },
                    { 3, "Banned" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "25153dfe-8a7a-48f3-a2f9-c314232dd6a3", "42cd3af3-f319-4118-a604-4442d487b923" },
                    { "1c13657c-0c92-4dec-b308-c41c725a1513", "5f2b8a40-d899-4345-aa3e-7b98712bc112" },
                    { "bb69367c-0380-470b-8337-35644e861587", "bd363936-63d0-4ace-bc46-a2f1348cb61e" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "GeneralCategoryId", "Type" },
                values: new object[,]
                {
                    { 1, "Electronics & Gadgets", 1, "product" },
                    { 2, "Fashion & Apparel", 2, "product" },
                    { 3, "Home & Kitchen Appliances", 9, "product" },
                    { 4, "Health & Beauty Products", 11, "product" },
                    { 5, "Sports & Fitness Equipment", 10, "product" },
                    { 6, "Software & Digital Products", 13, "product" },
                    { 7, "Agricultural Products", 14, "product" },
                    { 8, "Transport & Logistics", 12, "trade" },
                    { 9, "Education and Professional Training", 12, "trade" },
                    { 10, "Electrical Services", 12, "trade" },
                    { 11, "Events & Hospitality (Mikolo)", 12, "trade" },
                    { 12, "Construction & Building", 12, "trade" },
                    { 13, "Interior and Urban Design", 12, "trade" },
                    { 14, "Land", 15, "product" },
                    { 15, "Shoes", 16, "product" },
                    { 16, "Phones", 1, "product" },
                    { 17, "Televisions (TVs)", 17, "product" },
                    { 18, "Computers & Laptops", 2, "product" },
                    { 19, "Women's Clothing", 16, "product" },
                    { 20, "Food & Beverages", 18, "product" },
                    { 21, "Men's Clothing", 3, "product" },
                    { 22, "Decoration", 12, "trade" }
                });

            migrationBuilder.InsertData(
                table: "CategoryAttributes",
                columns: new[] { "CategoryAttributeId", "AttributeDataTypeId", "AttributeName", "CategoryId" },
                values: new object[,]
                {
                    { 1, 1, "Ingredients", 19 },
                    { 2, 1, "Net Weight / Volume", 19 },
                    { 3, 1, "Expiry Date", 19 },
                    { 4, 1, "Storage Instructions", 19 },
                    { 5, 1, "Certification", 19 },
                    { 6, 1, "Brand", 61 },
                    { 7, 1, "Model Name/Generation", 61 },
                    { 8, 4, "New?", 61 },
                    { 9, 4, "Refurbished", 61 },
                    { 10, 1, "Operating System", 61 },
                    { 11, 1, "Processor Model", 61 },
                    { 12, 1, "Processor Speed", 61 },
                    { 13, 1, "Number of Cores", 61 },
                    { 14, 1, "RAM Size", 61 },
                    { 15, 1, "Storage Capacity", 61 },
                    { 16, 1, "Graphics Details", 61 },
                    { 17, 1, "Screen Size", 61 },
                    { 18, 4, "HDMI / DisplayPort", 61 },
                    { 19, 4, "Ethernet Port", 61 },
                    { 20, 2, "USB Ports", 61 },
                    { 21, 1, "Color", 61 },
                    { 22, 1, "Battery Life", 61 },
                    { 23, 1, "Brand", 56 },
                    { 24, 1, "Model", 56 },
                    { 25, 1, "Operating System", 56 },
                    { 26, 1, "Release Year", 56 },
                    { 27, 1, "Screen Size (inches)", 56 },
                    { 28, 1, "RAM", 56 },
                    { 29, 1, "Processor Info", 56 },
                    { 30, 1, "Internal Storage", 56 },
                    { 31, 1, "Expandable Storage", 56 },
                    { 32, 1, "Rear Camera (MP)", 56 },
                    { 33, 1, "Front Camera (MP)", 56 },
                    { 34, 1, "Battery Capacity (mAh)", 56 },
                    { 35, 4, "Removable Battery ", 56 },
                    { 36, 1, "Charging Type (USB-C, Micro-USB)", 56 },
                    { 37, 1, "Number of SIMs", 56 },
                    { 38, 1, "Network (2G / 3G / 4G / 5G)", 56 },
                    { 39, 1, "Color", 56 },
                    { 40, 1, "Build Material", 56 },
                    { 41, 4, "Fingerprint Sensor", 56 },
                    { 42, 4, "Torchlight", 56 },
                    { 43, 4, "FM Radio", 56 },
                    { 44, 1, "Specify Animal ", 19 },
                    { 45, 1, "Crop Type", 18 },
                    { 46, 1, "Packaging", 18 },
                    { 47, 1, "Storage", 18 },
                    { 48, 1, "Expiry Date", 18 },
                    { 49, 1, "Nutrition", 18 },
                    { 50, 1, "General data", 20 },
                    { 51, 1, "Land Use", 50 },
                    { 52, 1, "Land Size", 50 },
                    { 53, 1, "Title Status", 50 },
                    { 54, 1, "Electricity Availability", 50 },
                    { 55, 1, "Road Access", 50 },
                    { 56, 1, "Water Access", 50 },
                    { 57, 1, "Land Use", 48 },
                    { 58, 1, "Land Size", 48 },
                    { 59, 1, "Title Status", 48 },
                    { 60, 1, "Electricity Availability", 48 },
                    { 61, 1, "Road Access", 48 },
                    { 62, 1, "Water Access", 48 },
                    { 63, 1, "Brand", 63 },
                    { 64, 1, "Model Name/Generation", 63 },
                    { 65, 4, "New?", 63 },
                    { 66, 4, "Refurbished", 63 },
                    { 67, 1, "Operating System", 63 },
                    { 68, 1, "Processor Model", 63 },
                    { 69, 1, "Screen Size", 63 },
                    { 70, 1, "Processor Speed", 63 },
                    { 71, 1, "Number of Cores", 63 },
                    { 72, 1, "RAM Size", 63 },
                    { 73, 2, "Storage Capacity", 63 },
                    { 74, 1, "Graphics Details", 63 },
                    { 75, 4, "HDMI / DisplayPort", 63 },
                    { 76, 1, "Ethernet Port", 63 },
                    { 77, 2, "USB Ports", 63 },
                    { 78, 1, "Color", 63 },
                    { 79, 1, "Brand", 62 },
                    { 80, 1, "Model Name/Generation", 62 },
                    { 81, 4, "Brand New?", 62 },
                    { 82, 4, "Refurbished", 62 },
                    { 83, 1, "Operating System", 62 },
                    { 84, 1, "Processor Model", 62 },
                    { 85, 1, "Processor Speed", 62 },
                    { 86, 1, "Number of Cores", 62 },
                    { 87, 1, "RAM Size", 62 },
                    { 88, 1, "Storage Capacity", 62 },
                    { 89, 1, "Graphics Details", 62 },
                    { 90, 4, "HDMI / DisplayPort", 62 },
                    { 91, 4, "Ethernet Port", 62 },
                    { 92, 2, "USB Ports", 62 },
                    { 93, 1, "Color", 62 },
                    { 94, 1, "Brand", 64 },
                    { 95, 1, "Model Name/Generation", 64 },
                    { 96, 4, "New?", 64 },
                    { 97, 4, "Refurbished", 64 },
                    { 98, 1, "Operating System", 64 },
                    { 99, 1, "Processor Model", 64 },
                    { 100, 1, "Processor Speed", 64 },
                    { 101, 1, "Number of Cores", 64 },
                    { 102, 1, "RAM Size", 64 },
                    { 103, 1, "Storage Capacity", 64 },
                    { 104, 1, "Graphics Details", 64 },
                    { 105, 4, "HDMI / DisplayPort", 64 },
                    { 106, 4, "Ethernet Port", 64 },
                    { 107, 2, "USB Ports", 64 },
                    { 108, 1, "Color", 64 },
                    { 109, 1, "Brand", 65 },
                    { 110, 1, "Model Name/Generation", 65 },
                    { 111, 4, "Brand New", 65 },
                    { 112, 1, "Operating System", 65 },
                    { 113, 1, "Processor Model", 65 },
                    { 114, 1, "Processor Speed", 65 },
                    { 115, 1, "Number of Cores", 65 },
                    { 116, 1, "RAM Size", 65 },
                    { 117, 1, "Storage Capacity", 65 },
                    { 118, 1, "Graphics Details", 65 },
                    { 119, 4, "HDMI / DisplayPort", 65 },
                    { 120, 4, "Ethernet Port", 65 },
                    { 121, 1, "Color", 65 },
                    { 122, 1, "Brand", 39 },
                    { 123, 1, "Weight", 39 },
                    { 124, 1, "Capacity", 39 },
                    { 125, 1, "Power Consumption", 39 },
                    { 126, 1, "Voltage rating", 39 },
                    { 127, 1, "Frequency", 39 },
                    { 128, 1, "Warranty Period", 39 },
                    { 129, 1, "Brand", 43 },
                    { 130, 1, "Model Number", 43 },
                    { 131, 1, "Weight", 43 },
                    { 132, 4, "Brand New?", 43 },
                    { 133, 1, "Color", 43 },
                    { 134, 1, "Capacity", 43 },
                    { 135, 1, "Power Consumption", 43 },
                    { 136, 1, "Voltage Rating", 43 },
                    { 137, 1, "Frequency", 43 },
                    { 138, 1, "Warranty Period", 43 },
                    { 139, 1, "Brand", 37 },
                    { 140, 1, "Model", 37 },
                    { 141, 4, "Brand New?", 37 },
                    { 142, 1, "Warranty Period", 37 },
                    { 143, 1, "Weight", 37 },
                    { 144, 1, "Color", 37 },
                    { 145, 1, "Capacity", 37 },
                    { 146, 1, "Power Rating", 37 },
                    { 147, 1, "Voltage Rating", 37 },
                    { 148, 1, "Frequency", 37 },
                    { 149, 1, "Brand", 42 },
                    { 150, 1, "Model", 42 },
                    { 151, 1, "Warranty Period", 42 },
                    { 152, 1, "Condition", 42 },
                    { 153, 1, "Fuel Type", 42 },
                    { 154, 1, "Dimensions", 42 },
                    { 155, 1, "Power Rating", 42 },
                    { 156, 1, "Voltage Rating", 42 },
                    { 157, 1, "Color", 42 },
                    { 158, 1, "Material", 42 },
                    { 159, 1, "Capacity", 42 },
                    { 160, 1, "Brand", 41 },
                    { 161, 1, "Weight", 41 },
                    { 162, 1, "Capacity", 41 },
                    { 163, 1, "Color", 36 },
                    { 164, 1, "Material", 36 },
                    { 165, 1, "Brand", 38 },
                    { 166, 1, "Model", 38 },
                    { 167, 4, "Brand New?", 38 },
                    { 168, 1, "Power Rating", 38 },
                    { 169, 1, "Voltage Rating", 38 },
                    { 170, 1, "Frequency", 38 },
                    { 171, 1, "Warranty Period", 38 },
                    { 172, 1, "Weight", 40 },
                    { 173, 1, "Voltage Rating", 40 },
                    { 174, 1, "Brand", 40 },
                    { 175, 1, "Condition", 40 },
                    { 176, 1, "Warranty Period", 40 },
                    { 177, 1, "Power Rating", 40 },
                    { 178, 1, "Model", 40 },
                    { 179, 1, "Manufacturer", 54 },
                    { 180, 1, "Size", 54 },
                    { 181, 1, "Color", 54 },
                    { 182, 1, "Material", 54 },
                    { 183, 4, "Brand New?", 54 },
                    { 184, 1, "Manufacturer", 52 },
                    { 185, 1, "Size", 52 },
                    { 186, 1, "Color", 52 },
                    { 187, 1, "Material", 52 },
                    { 188, 4, "Brand New?", 52 },
                    { 189, 1, "Manufacturer", 55 },
                    { 190, 1, "Size", 55 },
                    { 191, 1, "Color", 55 },
                    { 192, 1, "Material", 55 },
                    { 193, 4, "Brand New?", 55 },
                    { 194, 1, "Color", 53 },
                    { 195, 1, "Manufacturer", 53 },
                    { 196, 4, "Brand New?", 53 },
                    { 197, 1, "Size", 53 },
                    { 198, 1, "Material", 53 },
                    { 199, 1, "Color", 9 },
                    { 200, 1, "Manufacturer", 9 },
                    { 201, 1, "Color", 10 },
                    { 202, 1, "Manufacturer", 10 },
                    { 203, 1, "Warranty Period", 10 },
                    { 204, 1, "Color", 59 },
                    { 205, 1, "Manufacturer", 59 },
                    { 206, 1, "Power Rating", 59 },
                    { 207, 1, "Voltage Rating", 59 },
                    { 208, 1, "Screen Size (inches)", 59 },
                    { 209, 1, "Application", 59 },
                    { 210, 1, "Color", 58 },
                    { 211, 1, "Screen Size (inches)", 58 },
                    { 212, 1, "Manufacturer", 58 },
                    { 213, 1, "Warranty Period", 58 },
                    { 214, 1, "Operating System", 58 },
                    { 215, 4, "HDMI Port", 58 },
                    { 216, 4, "USB Ports", 58 },
                    { 217, 3, "Wireless Connectivity", 58 },
                    { 218, 4, "BlueTooth", 58 },
                    { 219, 4, "AV Input", 58 },
                    { 220, 1, "Power Rating", 58 },
                    { 221, 1, "Voltage Rating", 58 },
                    { 222, 1, "Resolution", 58 },
                    { 223, 1, "HDR Support", 58 },
                    { 224, 1, "Resolution", 60 },
                    { 225, 1, "Manufacturer", 60 },
                    { 226, 1, "Operating System", 60 },
                    { 227, 1, "Power Rating", 60 },
                    { 228, 1, "Voltage Rating", 60 },
                    { 229, 4, "HDMI Ports", 60 },
                    { 230, 1, "Warranty Period", 60 },
                    { 231, 1, "AV Input", 60 },
                    { 232, 4, "HDR Support", 60 },
                    { 233, 1, "Professional Qualification", 34 },
                    { 234, 1, "Experience (Years)", 34 },
                    { 235, 2, "Project Type", 34 },
                    { 236, 1, "Scope of Work", 34 },
                    { 237, 1, "Design Software Used", 34 },
                    { 238, 1, "Deliverables", 34 },
                    { 239, 1, "Consultation Fee", 34 },
                    { 240, 1, "Additional Details", 34 },
                    { 241, 1, "Brand", 32 },
                    { 242, 1, "Grade / Strength", 32 },
                    { 243, 1, "Material Composition", 32 },
                    { 244, 1, "Weight", 32 },
                    { 245, 1, "Color", 32 },
                    { 246, 1, "Packaging", 32 },
                    { 247, 4, "Water Resistance", 32 },
                    { 248, 1, "Suitable Use", 32 },
                    { 249, 4, "Delivery Available", 32 },
                    { 250, 1, "Warranty Period", 32 },
                    { 251, 1, "Additional Details", 32 },
                    { 252, 1, "Brand", 35 },
                    { 253, 1, "Model Number", 35 },
                    { 254, 1, "Power Source", 35 },
                    { 255, 1, "Power Rating", 35 },
                    { 256, 1, "Capacity", 35 },
                    { 257, 1, "Weight", 35 },
                    { 258, 4, "Brand New?", 35 },
                    { 259, 1, "Usage Type", 35 },
                    { 260, 4, "Rental Available", 35 },
                    { 261, 1, "Accessories Included", 35 },
                    { 262, 1, "Additional Details", 35 },
                    { 263, 1, "Experience (Years)", 33 },
                    { 264, 1, "Schedule", 26 },
                    { 265, 1, "Delivery Mode (online,physical or workshop)", 26 },
                    { 266, 1, "Entry Requirements", 26 },
                    { 267, 1, "Language of Instruction", 26 },
                    { 268, 1, "Skill Level (Beginner, Intermediate, Advanced)", 26 },
                    { 269, 4, "Accredited", 26 },
                    { 270, 1, "Training Field", 26 },
                    { 271, 1, "Programming Language", 23 },
                    { 272, 1, "Software/Tools Covered", 23 },
                    { 273, 1, "Skill Level (Beginner, Intermediate, Advanced)", 23 },
                    { 274, 1, "Delivery Mode (Online, Onsite, Hybrid)", 23 },
                    { 275, 1, "Course Duration", 23 },
                    { 276, 4, "Hands-on Projects", 23 },
                    { 277, 1, "Certificate Provider", 23 },
                    { 278, 1, "Entry Requirements", 23 },
                    { 279, 1, "Target Audience", 23 },
                    { 280, 4, "Job Placement Support", 23 },
                    { 281, 1, "Course Schedule (Weekend, weekday, evening)", 23 },
                    { 282, 1, "Engineering Discipline(Mechanical, Civil, Electrical)", 24 },
                    { 283, 1, "Skill Level (Beginner, Intermediate, Advanced)", 24 },
                    { 284, 1, "Training Type(Certification, workshop, diploma)", 24 },
                    { 285, 1, "Delivery Mode (Online, Onsite, Hybrid)", 24 },
                    { 286, 1, "Training Duration", 24 },
                    { 287, 1, "Required Background", 24 },
                    { 288, 4, "Certification Offered", 24 },
                    { 289, 1, "Assessment Type", 24 },
                    { 292, 1, "Hardware Requirements", 24 },
                    { 293, 1, "Entry Requirements", 25 },
                    { 294, 1, "Training Duration", 25 },
                    { 295, 4, "Certification Offered", 25 },
                    { 296, 1, "Training Location", 25 },
                    { 297, 4, "Job Placement Assistance", 25 },
                    { 298, 4, "Accreditation", 25 },
                    { 299, 1, "Instructor Profile", 25 },
                    { 300, 1, "Delivery Mode (Online, Onsite, Hybrid)", 25 },
                    { 301, 1, "Electrical Contractor License / Certification", 28 },
                    { 302, 1, "Years of Experience", 28 },
                    { 303, 1, "Service Area", 28 },
                    { 304, 1, "Electrical Contractor License / Certification", 27 },
                    { 305, 1, "Years of Experience", 27 },
                    { 306, 1, "Service Area", 27 },
                    { 307, 1, "Brand", 82 },
                    { 308, 4, "Brand New?", 82 },
                    { 309, 1, "Capacity", 82 },
                    { 310, 1, "Input Voltage", 82 },
                    { 311, 1, "Connector Type (USB-A, USB-C, Lightning)", 82 },
                    { 312, 1, "Color", 82 },
                    { 313, 1, "Warranty Period", 82 },
                    { 314, 1, "Brand", 81 },
                    { 315, 1, "Model", 81 },
                    { 316, 4, "Brand New?", 81 },
                    { 317, 1, "Connectivity (Bluetooth, Wi-Fi, NFC)", 81 },
                    { 318, 1, "Battery Life(hrs)", 81 },
                    { 319, 4, "App Support", 81 },
                    { 320, 1, "Color", 81 },
                    { 321, 1, "Warranty Period", 81 },
                    { 322, 1, "Weight", 81 },
                    { 323, 1, "Brand", 80 },
                    { 324, 1, "Model", 80 },
                    { 325, 1, "Camera Type (DSLR, Mirrorless, Action Camera)", 80 },
                    { 326, 1, "Megapixels", 80 },
                    { 327, 1, "Optical Zoom", 80 },
                    { 328, 1, "Digital Zoom", 80 },
                    { 329, 1, "Video Resolution (1080p, 4K, 8K)", 80 },
                    { 330, 1, "Display Screen Size", 80 },
                    { 331, 1, "Connectivity (Wi-Fi, Bluetooth, NFC)", 80 },
                    { 332, 1, "Memory Card Type", 80 },
                    { 333, 1, "Battery Life(hrs)", 80 },
                    { 334, 1, "Weight", 80 },
                    { 335, 1, "Warranty Period", 80 },
                    { 336, 1, "Color", 80 },
                    { 337, 1, "Brand", 5 },
                    { 338, 1, "Model", 5 },
                    { 339, 1, "Console Type (Home / Portable)", 5 },
                    { 340, 1, "Storage Capacity (GB/TB)", 5 },
                    { 341, 4, "Graphics Processing Unit (GPU)", 5 },
                    { 342, 1, "RAM Size", 5 },
                    { 343, 1, "Maximum Resolution (1080p / 4K / 8K)", 5 },
                    { 344, 1, "Connectivity (Wi-Fi, Ethernet, Bluetooth)", 5 },
                    { 345, 1, "Supported Game Media (Disc / Digital)", 5 },
                    { 346, 2, "Number of Controllers Included", 5 },
                    { 347, 1, "Controller Type (Wireless / Wired)", 5 },
                    { 348, 4, "Online Gaming Support", 5 },
                    { 349, 1, "Warranty Period", 5 },
                    { 350, 1, "Brand", 4 },
                    { 351, 1, "Model", 4 },
                    { 352, 1, "Connectivity Type (Bluetooth, Wired, Wi-Fi)", 4 },
                    { 353, 4, "Noise Cancellation", 4 },
                    { 354, 1, "Battery Capacity (mAh)", 4 },
                    { 355, 1, "Charging Type (USB-C, Micro-USB, Lightning)", 4 },
                    { 356, 1, "Color", 4 },
                    { 357, 1, "Compatibility (Android, iOS, Windows)", 4 },
                    { 358, 1, "Years of Experience", 29 },
                    { 359, 1, "Team Size", 29 },
                    { 360, 1, "Years of Experience", 30 },
                    { 361, 1, "Team Size", 30 },
                    { 362, 1, "Year Established", 31 },
                    { 363, 1, "Location", 31 },
                    { 364, 1, "GPS Coordinates", 31 },
                    { 365, 1, "Capacity", 31 },
                    { 366, 1, "Number of Halls / Rooms", 31 },
                    { 367, 1, "Parking Capacity", 31 },
                    { 368, 4, "Wi-Fi Availability", 31 },
                    { 369, 4, "Power Backup (Generator)", 31 },
                    { 370, 4, "Catering Services Available", 31 },
                    { 371, 1, "Kitchen Facilities", 31 },
                    { 372, 4, "Sound System Availability", 31 },
                    { 373, 4, "Stage Availability", 31 },
                    { 374, 4, "Live Streaming Capability", 31 },
                    { 375, 4, "Security", 31 },
                    { 376, 1, "Check-in / Check-out Times", 31 },
                    { 377, 1, "Event Permit Requirements", 31 },
                    { 378, 1, "Ingredients", 75 },
                    { 379, 1, "Net Weight / Volume", 75 },
                    { 380, 1, "Packaging Type", 75 },
                    { 381, 1, "Expiry Date", 75 },
                    { 382, 1, "Storage Recommendation", 75 },
                    { 383, 1, "Flavor", 75 },
                    { 384, 1, "Certification", 75 },
                    { 385, 1, "Ingredients", 77 },
                    { 386, 4, "Organic / Natural", 75 },
                    { 388, 1, "Net Weight / Volume", 77 },
                    { 389, 1, "Packaging Type", 77 },
                    { 390, 1, "Expiry Date", 77 },
                    { 391, 1, "Storage Recommendation", 77 },
                    { 392, 1, "Flavor", 77 },
                    { 393, 1, "Organic / Natural", 77 },
                    { 394, 1, "Ingredients", 79 },
                    { 395, 1, "Net Weight / Volume", 79 },
                    { 396, 1, "Packaging Type", 79 },
                    { 397, 1, "Storage Recommendation", 79 },
                    { 398, 4, "Organic / Natural", 79 },
                    { 399, 1, "Ingredients", 76 },
                    { 400, 1, "Net Weight / Volume", 76 },
                    { 401, 1, "Packaging Type", 76 },
                    { 402, 1, "Storage Recommendation", 76 },
                    { 403, 1, "Flavor", 76 },
                    { 404, 4, "Carbonated", 76 },
                    { 406, 1, "Storage", 75 },
                    { 407, 1, "Ingredients", 78 },
                    { 408, 1, "Net Weight / Volume", 78 },
                    { 409, 1, "Expiry Date", 78 },
                    { 410, 1, "Storage Recommendation", 78 },
                    { 411, 1, "Brand", 11 },
                    { 412, 1, "Target Area", 11 },
                    { 413, 1, "Key Ingredients", 11 },
                    { 414, 1, "Skin Concern (Acne, Aging, Brightening, Hydration)", 11 },
                    { 415, 4, "Organic/Natural", 11 },
                    { 416, 1, "Gender", 11 },
                    { 417, 1, "Expiry Date", 11 },
                    { 418, 1, "Usage Instructions", 11 },
                    { 419, 1, "Brand", 84 },
                    { 420, 1, "Tool Type", 84 },
                    { 421, 1, "Power Source (Manual/Battery/Electric)", 84 },
                    { 422, 1, "Skin Type Compatibility", 84 },
                    { 423, 1, "Color", 84 },
                    { 424, 1, "Usage Area", 84 },
                    { 425, 1, "Warranty Period", 84 },
                    { 426, 1, "Years of Experience", 45 },
                    { 427, 1, "Location", 45 },
                    { 428, 1, "Estimated Processing Time", 46 },
                    { 429, 4, "Digital Document Copies", 46 },
                    { 430, 1, "Survey Outputs", 44 },
                    { 431, 1, "Other details", 44 },
                    { 432, 1, "Land Use", 49 },
                    { 433, 1, "Land Size", 49 },
                    { 434, 1, "Title Status", 49 },
                    { 435, 1, "Electricity Availability", 49 },
                    { 436, 1, "Road Access", 49 },
                    { 437, 1, "Water Access", 49 },
                    { 438, 1, "Land Use", 47 },
                    { 439, 1, "Land Size", 47 },
                    { 440, 1, "Title Status", 47 },
                    { 441, 1, "Electricity Availability", 47 },
                    { 442, 1, "Road Access", 47 },
                    { 443, 1, "Water Access", 47 },
                    { 444, 1, "Brand", 57 },
                    { 445, 1, "Model", 57 },
                    { 446, 1, "Battery Capacity (mAh)", 57 },
                    { 447, 1, "Number of SIMs", 57 },
                    { 448, 1, "Color", 57 },
                    { 449, 4, "Torchlight", 57 },
                    { 450, 4, "FM Radio", 57 },
                    { 451, 1, "Software Version / Release", 14 },
                    { 452, 1, "Supported Operating Systems (Windows, Linux, MacOS)", 14 },
                    { 453, 1, "Minimum System Requirements", 14 },
                    { 454, 1, "Architecture (32-bit / 64-bit)", 14 },
                    { 455, 1, "License Type (Single-user, Multi-user, Enterprise, Subscription)", 14 },
                    { 456, 4, "License key", 14 },
                    { 457, 1, "File Size", 14 },
                    { 458, 1, "Category (Web development, design, automation)", 17 },
                    { 459, 1, "Version", 17 },
                    { 460, 1, "Compatible Platforms ", 17 },
                    { 461, 1, "File Size", 17 },
                    { 462, 1, "Installation Instructions", 17 },
                    { 463, 1, "License Type (Commercial, Personal, Extended license)", 17 },
                    { 464, 1, "Delivery Criteria", 17 },
                    { 465, 1, "Programming Language", 17 },
                    { 466, 1, "Engineering Field", 15 },
                    { 467, 1, "Developer / Vendor", 15 },
                    { 468, 1, "Supported Platforms", 15 },
                    { 469, 1, "System Requirements", 15 },
                    { 470, 1, "License Type ", 15 },
                    { 471, 1, "Serial Key", 15 },
                    { 472, 1, "Developer / Publisher", 16 },
                    { 473, 1, "App Version", 16 },
                    { 474, 1, "Platform (Android, iOS, Cross-platform)", 16 },
                    { 475, 1, "Minimum OS Version", 16 },
                    { 476, 1, "File Size", 16 },
                    { 477, 4, "Offline Capability?", 16 },
                    { 478, 1, "Activation Method", 16 },
                    { 479, 1, "Delivery Criteria", 16 },
                    { 480, 1, "Other details", 16 },
                    { 481, 1, "Developer / Vendor", 13 },
                    { 482, 1, "Release Year", 13 },
                    { 483, 1, "Version", 13 },
                    { 484, 1, "Architecture (32-bit / 64-bit)", 13 },
                    { 485, 1, "Minimum System Requirements", 13 },
                    { 486, 1, "Activation Method", 13 },
                    { 487, 1, "Passenger Capacity", 21 },
                    { 488, 1, "Number Plate", 21 },
                    { 489, 1, "Number of Vehicles", 21 },
                    { 490, 1, "Cargo Capacity", 22 },
                    { 491, 1, "Number Plate", 22 },
                    { 492, 1, "Other details", 22 },
                    { 493, 1, "Brand", 12 },
                    { 494, 1, "Power Type (Manual / Electric / Battery)", 12 },
                    { 495, 1, "Motor Power (HP) – for treadmills", 12 },
                    { 496, 1, "Speed Range", 12 },
                    { 497, 1, "Maximum User Weight Capacity", 12 },
                    { 498, 1, "Dimensions (L × W × H)", 12 },
                    { 499, 1, "Frame Material", 12 },
                    { 500, 4, "App compatibility", 12 },
                    { 501, 1, "Warranty Period", 12 },
                    { 502, 1, "Brand", 86 },
                    { 503, 1, "Individual Weight", 86 },
                    { 504, 1, "Total Set Weight", 86 },
                    { 505, 1, "Material", 86 },
                    { 506, 1, "Size/Diameter", 86 },
                    { 507, 1, "Brand", 87 },
                    { 508, 1, "Weight", 87 },
                    { 509, 1, "Dimensions", 87 },
                    { 510, 1, "Material", 87 },
                    { 511, 1, "Usage Instructions", 87 },
                    { 512, 1, "Brand", 88 },
                    { 513, 1, "Material", 88 },
                    { 514, 1, "Dimensions", 88 },
                    { 515, 1, "Brand", 89 },
                    { 516, 1, "Material", 89 },
                    { 517, 1, "Weight", 89 },
                    { 518, 1, "Other details", 89 },
                    { 519, 1, "Brand", 85 },
                    { 520, 1, "Maximum Load Capacity", 85 },
                    { 521, 1, "Material", 85 },
                    { 522, 1, "Brand", 66 },
                    { 523, 1, "Size", 66 },
                    { 524, 1, "Color", 66 },
                    { 525, 1, "Neckline", 66 },
                    { 526, 1, "Material / Fabric", 66 },
                    { 527, 1, "Brand", 72 },
                    { 528, 1, "Material / Fabric", 72 },
                    { 529, 1, "Color", 72 },
                    { 530, 1, "Size", 72 },
                    { 531, 1, "Brand", 70 },
                    { 532, 1, "Size", 70 },
                    { 533, 1, "Material / Fabric", 70 },
                    { 534, 1, "Color", 70 },
                    { 535, 1, "Brand", 68 },
                    { 536, 1, "Color", 68 },
                    { 537, 1, "Material / Fabric", 68 },
                    { 538, 1, "Size", 68 },
                    { 542, 1, "Brand", 69 },
                    { 543, 1, "Color", 69 },
                    { 544, 1, "Size", 69 },
                    { 545, 1, "Material / Fabric", 69 },
                    { 546, 1, "Brand", 74 },
                    { 547, 1, "Color", 74 },
                    { 548, 1, "Size", 74 },
                    { 549, 1, "Material / Fabric", 74 },
                    { 550, 1, "Brand", 71 },
                    { 551, 1, "Color", 71 },
                    { 552, 1, "Material / Fabric", 71 },
                    { 553, 1, "Size", 71 },
                    { 554, 1, "Brand", 67 },
                    { 555, 1, "Color", 67 },
                    { 556, 1, "Material / Fabric", 67 },
                    { 557, 1, "Size", 67 },
                    { 559, 1, "Brand", 73 },
                    { 560, 1, "Color", 73 },
                    { 562, 1, "Size", 73 },
                    { 563, 1, "Material / Fabric", 73 },
                    { 564, 1, "Brand", 90 },
                    { 565, 1, "Color", 90 },
                    { 566, 1, "Size", 90 },
                    { 567, 1, "Material / Fabric", 90 },
                    { 568, 1, "Brand", 91 },
                    { 569, 1, "Color", 91 },
                    { 570, 1, "Size", 91 },
                    { 571, 1, "Material / Fabric", 91 },
                    { 572, 1, "Other details", 91 },
                    { 573, 1, "Brand", 92 },
                    { 574, 1, "Size", 92 },
                    { 575, 1, "Material / Fabric", 92 },
                    { 576, 1, "Color", 92 },
                    { 577, 1, "Brand", 93 },
                    { 578, 1, "Size", 93 },
                    { 579, 1, "Material / Fabric", 93 },
                    { 580, 1, "Color", 93 },
                    { 581, 1, "Brand", 94 },
                    { 582, 1, "Size", 94 },
                    { 583, 1, "Color", 94 },
                    { 584, 1, "Material / Fabric", 94 },
                    { 585, 1, "Brand", 95 },
                    { 586, 1, "Size", 95 },
                    { 587, 1, "Color", 95 },
                    { 588, 1, "Material / Fabric", 95 },
                    { 589, 1, "Brand", 96 },
                    { 590, 1, "Size", 96 },
                    { 591, 1, "Color", 96 },
                    { 592, 1, "Material / Fabric", 96 },
                    { 593, 1, "Brand", 97 },
                    { 594, 1, "Size", 97 },
                    { 595, 1, "Color", 97 },
                    { 596, 1, "Material / Fabric", 97 }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "UserProfileId", "ActiveGroupId", "AddressId", "BuyerProfileId", "CreatedAt", "IdentityUserId", "SellerProfileId", "UserStatusId" },
                values: new object[,]
                {
                    { new Guid("471c7d67-9faa-4beb-b28a-98eb9ae40983"), null, 1, null, new DateTime(2026, 4, 4, 13, 13, 46, 266, DateTimeKind.Utc).AddTicks(2435), "42cd3af3-f319-4118-a604-4442d487b923", null, 1 },
                    { new Guid("644f775e-5871-425e-8182-a8523877041b"), null, 1, null, new DateTime(2026, 4, 4, 13, 13, 46, 266, DateTimeKind.Utc).AddTicks(2469), "5f2b8a40-d899-4345-aa3e-7b98712bc112", null, 1 },
                    { new Guid("caeac90a-7543-4bf1-a81d-2f2f59078512"), null, 1, null, new DateTime(2026, 4, 4, 13, 13, 46, 266, DateTimeKind.Utc).AddTicks(2473), "bd363936-63d0-4ace-bc46-a2f1348cb61e", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "SubCategoryId", "CategoryId", "SubCategoryName" },
                values: new object[,]
                {
                    { 4, 1, "Audio Devices" },
                    { 5, 1, "Gaming Consoles" },
                    { 9, 2, "Bags" },
                    { 10, 2, "Watches" },
                    { 11, 4, "Skincare" },
                    { 12, 5, "Gym Equipment" },
                    { 13, 6, "Operating Systems" },
                    { 14, 6, "Application Software" },
                    { 15, 6, "Engineering & CAD Software" },
                    { 16, 6, "Mobile Applications (APK & App Licenses)" },
                    { 17, 6, "Digital Assets" },
                    { 18, 7, "Plant-Based Foods" },
                    { 19, 7, "Animal-Based Foods" },
                    { 20, 7, "Others" },
                    { 21, 8, "Passenger Transport" },
                    { 22, 8, "Freight and Cargo" },
                    { 23, 9, "ICT and Digital Skills" },
                    { 24, 9, "Technical and Engineering Training" },
                    { 25, 9, "Vocational Training" },
                    { 26, 9, "Adult and Continuous Training" },
                    { 27, 10, "Residential Electrical Works" },
                    { 28, 10, "Industrial Electrical Works" },
                    { 29, 11, "Event Planning" },
                    { 30, 11, "Event Services" },
                    { 31, 11, "Venues" },
                    { 32, 12, "Building Materials" },
                    { 33, 12, "Construction Services" },
                    { 34, 12, "Architecture & Engineering Services" },
                    { 35, 12, "Construction Equipment & Tools" },
                    { 36, 3, "Kitchen Appliances" },
                    { 37, 3, "Cooling & Climate Control" },
                    { 38, 3, "Laundry Appliances" },
                    { 39, 3, "Cleaning Appliances" },
                    { 40, 3, "Water & Heating Appliances" },
                    { 41, 3, "Home Comfort & Living Appliances" },
                    { 42, 3, "Gas & Energy Appliances" },
                    { 43, 3, "Commercial Home Appliances" },
                    { 44, 13, "Surveying and Land Services" },
                    { 45, 13, "Interior Design" },
                    { 46, 13, "Land Documentation" },
                    { 47, 14, "Mailo Land" },
                    { 48, 14, "Freehold Land" },
                    { 49, 14, "Leasehold Land" },
                    { 50, 14, "Customary Land" },
                    { 52, 15, "Men Shoes" },
                    { 53, 15, "Women Shoes" },
                    { 54, 15, "Kids Shoes" },
                    { 55, 15, "Unisex" },
                    { 56, 16, "Smartphones" },
                    { 57, 16, "Feature Phones (Non-Smartphones / Mapeesa)" },
                    { 58, 17, "Smart Televisions" },
                    { 59, 17, "Non-Smart Televisions" },
                    { 60, 17, "Special Purpose TVs" },
                    { 61, 18, "Laptops" },
                    { 62, 18, "Desktop & Tower Computers" },
                    { 63, 18, "All-in-One Computers" },
                    { 64, 18, "Mini / Compact PCs" },
                    { 65, 18, "Workstations" },
                    { 66, 19, "Dresses" },
                    { 67, 19, "Tops & Blouses" },
                    { 68, 19, "Pants & Trousers" },
                    { 69, 19, "Skirts" },
                    { 70, 19, "Outerwear" },
                    { 71, 19, "Suits & Sets" },
                    { 72, 19, "Jumpsuits & Rompers" },
                    { 73, 19, "Traditional / African Wear" },
                    { 74, 19, "Sportswear & Activewear" },
                    { 75, 20, "Traditional Beverages and Drinks" },
                    { 76, 20, "Packaged Juices & Soft Drinks" },
                    { 77, 20, "Dairy Drinks" },
                    { 78, 20, "Traditional Snacks & Seeds" },
                    { 79, 20, "Nut Butters & Pastes" },
                    { 80, 1, "Cameras & Photography Devices" },
                    { 81, 1, "Wearable Technology" },
                    { 82, 1, "Accessories & Peripherals" },
                    { 84, 4, "Beauty & Personal Care Tools" },
                    { 85, 5, "Strength Training Equipment" },
                    { 86, 5, "Free Weights" },
                    { 87, 5, "Functional Training Equipment" },
                    { 88, 5, "Gym Accessories" },
                    { 89, 5, "Outdoor Sports Equipment" },
                    { 90, 21, "Top Wear" },
                    { 91, 21, "Bottom Wear" },
                    { 92, 21, "Traditional & Cultural Wear" },
                    { 93, 21, "Outerwear" },
                    { 94, 21, "Underwear & Sleepwear" },
                    { 95, 21, "Sportswear & Activewear" },
                    { 96, 21, "Men's Suits & Formal Wear" },
                    { 97, 21, "Accessories (Clothing Related)" }
                });

            migrationBuilder.InsertData(
                table: "SubCategoryCategories",
                columns: new[] { "SubCategoryCategoryId", "SubCategoryCategoryName", "SubCategoryId" },
                values: new object[,]
                {
                    { 7, "Bluetooth Speakers", 4 },
                    { 8, "Wireless Headphones", 4 },
                    { 9, "PlayStation Consoles", 5 },
                    { 10, "Xbox Consoles", 5 },
                    { 17, "Handbags", 9 },
                    { 18, "Travel Bags", 9 },
                    { 19, "Smart Watches", 10 },
                    { 20, "Luxury Watches", 10 },
                    { 21, "Facial Skincare", 11 },
                    { 22, "Serums & Toners", 11 },
                    { 23, "Treadmills", 12 },
                    { 24, "Exercise Bikes", 12 },
                    { 25, "Desktop OS", 13 },
                    { 26, "Server OS", 13 },
                    { 27, "Mobile OS", 13 },
                    { 28, "Security Software", 14 },
                    { 29, "Productivity Software (Office)", 14 },
                    { 30, "Accounting & Business Software", 14 },
                    { 31, "Engineering & CAD Software", 14 },
                    { 32, "Creative & Design Software", 14 },
                    { 33, "AutoCAD", 15 },
                    { 34, "AutoCAD Electrical", 15 },
                    { 35, "SolidWorks", 15 },
                    { 36, "PLC Software", 15 },
                    { 37, "Android Applications", 16 },
                    { 38, "iOS Applications", 16 },
                    { 39, "Premium App Licenses", 16 },
                    { 40, "Website Themes", 17 },
                    { 41, "Software/Platforms", 17 },
                    { 42, "Tubers & Root Crops", 18 },
                    { 43, "Bananas & Plantains", 18 },
                    { 44, "Legumes & Nuts", 18 },
                    { 45, "Grains & Cereals", 18 },
                    { 46, "Vegetables", 18 },
                    { 47, "Fruits", 18 },
                    { 48, "Others", 18 },
                    { 49, "Meat", 19 },
                    { 50, "Poultry", 19 },
                    { 51, "Fish & Seafood", 19 },
                    { 52, "Dairy", 19 },
                    { 53, "Eggs", 19 },
                    { 54, "General", 20 },
                    { 55, "Private Car Hire", 21 },
                    { 56, "Airport Transport", 21 },
                    { 57, "Taxi/Ride", 21 },
                    { 58, "Bus Services", 21 },
                    { 59, "School Transport", 21 },
                    { 60, "Local Delivery (Lorry)", 22 },
                    { 61, "Long Distance Haulage", 22 },
                    { 62, "Motor Cycle (Boda)", 22 },
                    { 63, "Refrigerated Transport", 22 },
                    { 64, "Computer Training", 23 },
                    { 65, "Microsoft Office Training", 23 },
                    { 66, "Programming Courses", 23 },
                    { 67, "AI Training", 23 },
                    { 68, "Graphics Design", 23 },
                    { 69, "AutoCad Training", 24 },
                    { 70, "Civil 3D Training", 24 },
                    { 71, "Solid Works Training", 24 },
                    { 72, "Tailoring", 25 },
                    { 73, "Plumbing", 25 },
                    { 74, "Welding", 25 },
                    { 75, "Electrical Installation Training", 25 },
                    { 76, "Adult Literacy Program", 26 },
                    { 77, "House Wiring", 27 },
                    { 78, "Generator Installation/Hire", 27 },
                    { 79, "Solar System Installation", 27 },
                    { 80, "Camera Installation", 27 },
                    { 81, "Industrial Wiring", 28 },
                    { 82, "Electrical Panel Design & Installation", 28 },
                    { 83, "Factory Maintenance", 28 },
                    { 84, "Power Backup Systems", 28 },
                    { 85, "Wedding Planning", 29 },
                    { 86, "Corporate Events", 29 },
                    { 87, "Funeral Services", 29 },
                    { 88, "Cultural Ceremonies", 29 },
                    { 89, "Catering", 30 },
                    { 90, "Decoration", 30 },
                    { 91, "Tent & Chair Hire", 30 },
                    { 92, "Photography and Videography", 30 },
                    { 93, "Sound and DJ Services", 30 },
                    { 94, "MC and Host Services", 30 },
                    { 95, "Venues", 31 },
                    { 96, "Event Security", 30 },
                    { 97, "Stage Set-Up", 30 },
                    { 98, "Cement & Concrete Materials", 32 },
                    { 99, "Steel & Reinforcement", 32 },
                    { 100, "Roofing Materials", 32 },
                    { 101, "Timber & Wood Products", 32 },
                    { 102, "Plumbing Materials", 32 },
                    { 103, "Finishing Materials", 32 },
                    { 104, "General Construction", 33 },
                    { 105, "Specialized Trades", 33 },
                    { 106, "Civil & Infrastructure Works", 33 },
                    { 107, "Architectural Services", 34 },
                    { 108, "Engineering Services", 34 },
                    { 109, "Surveying & Planning", 34 },
                    { 110, "Heavy Machinery", 35 },
                    { 111, "Hand & Power Tools", 35 },
                    { 112, "Large Kitchen Appliances", 36 },
                    { 113, "Small Kitchen Appliances", 36 },
                    { 114, "Cooking & Food Preparation", 36 },
                    { 115, "Coffee & Beverage Appliances", 36 },
                    { 116, "Air Conditioning", 37 },
                    { 117, "Fans & Ventilation", 37 },
                    { 118, "Air Quality", 37 },
                    { 119, "Washing Machines", 38 },
                    { 120, "Washer-Dryer", 38 },
                    { 121, "Steam Irons", 38 },
                    { 122, "Vacuum Cleaners", 39 },
                    { 123, "Carpet Cleaners", 39 },
                    { 124, "Water Heaters", 40 },
                    { 125, "Water Pumps", 40 },
                    { 126, "Electric Heaters", 40 },
                    { 127, "Mosquito Killers", 41 },
                    { 128, "Aroma Diffusers", 41 },
                    { 129, "Insect Repellents", 41 },
                    { 130, "Gas Cylinders", 42 },
                    { 131, "Solar Home Systems", 42 },
                    { 132, "Commercial Refrigerators", 43 },
                    { 133, "Bakery Ovens", 43 },
                    { 134, "Cookers", 43 },
                    { 135, "Land Survey", 44 },
                    { 136, "Construction Setting Out", 44 },
                    { 137, "Topographic Survey", 44 },
                    { 138, "Interior Design", 45 },
                    { 139, "Landscape Design", 45 },
                    { 140, "Urban Planning", 45 },
                    { 141, "Title Processing", 46 },
                    { 142, "Land Valuation", 46 },
                    { 143, "Property Demarcation", 46 },
                    { 144, "Mailo Residential Plots", 47 },
                    { 145, "Mailo Agricultural Land", 47 },
                    { 146, "Mailo Estate Development Land", 47 },
                    { 147, "Freehold Residential Plots", 48 },
                    { 148, "Freehold Farm Land", 48 },
                    { 149, "Freehold Commercial Land", 48 },
                    { 150, "Leasehold Residential Land", 49 },
                    { 151, "Leasehold Industrial Land", 49 },
                    { 152, "Leasehold Agricultural Land", 49 },
                    { 153, "Customary Agricultural Land", 50 },
                    { 154, "Customary Farm Land", 50 },
                    { 155, "Casual Shoes", 52 },
                    { 156, "Formal Shoes", 52 },
                    { 157, "Sports & Athletic Shoes", 52 },
                    { 158, "Sandals", 52 },
                    { 159, "Slippers & Indoor Shoes", 52 },
                    { 160, "Casual Shoes", 53 },
                    { 161, "Formal Shoes (Gentle)", 53 },
                    { 163, "Boots", 53 },
                    { 164, "Slippers & Indoor Shoes", 53 },
                    { 165, "Work & Safety Shoes", 53 },
                    { 166, "Casual Shoes", 54 },
                    { 167, "Formal Shoes", 54 },
                    { 168, "Sports & Athletic Shoes", 54 },
                    { 169, "Heels", 54 },
                    { 170, "Flats", 54 },
                    { 171, "Casual Shoes", 55 },
                    { 172, "Sports & Athletic Shoes", 55 },
                    { 173, "Boots", 55 },
                    { 174, "Sandals", 55 },
                    { 175, "Slippers & Indoor Shoes", 55 },
                    { 176, "Work & Safety Shoes", 55 },
                    { 177, "Dual-SIM", 57 },
                    { 178, "Senior Citizen Phones", 57 },
                    { 179, "Basic Keypad Phones", 57 },
                    { 180, "Android Smartphones", 56 },
                    { 181, "iPhones (iOS Smartphones)", 56 },
                    { 182, "Gaming Smartphones", 56 },
                    { 183, "Digital LED TVs", 59 },
                    { 184, "Analog TVs", 59 },
                    { 185, "Basic Flat Screen TVs", 59 },
                    { 186, "Android TVs", 58 },
                    { 187, "Google TVs", 58 },
                    { 188, "WebOS TVs", 58 },
                    { 189, "Tizen OS TVs", 58 },
                    { 190, "Roku TVs", 58 },
                    { 191, "Fire TV Edition TVs", 58 },
                    { 192, "Gaming TVs", 60 },
                    { 193, "Outdoor TVs", 60 },
                    { 194, "Hospitality / Hotel TVs", 60 },
                    { 195, "Commercial Display TVs", 60 },
                    { 196, "Portable TVs", 60 },
                    { 197, "Business All-in-One PCs", 63 },
                    { 198, "Home All-in-One PCs", 63 },
                    { 199, "Touchscreen All-in-One PCs", 63 },
                    { 200, "Educational All-in-One PCs", 63 },
                    { 201, "Home & Office PC", 62 },
                    { 202, "Gaming PC", 62 },
                    { 203, "Mini Desktop", 62 },
                    { 204, "Complete Unit (Full Set)", 62 },
                    { 205, "Ultrabooks", 61 },
                    { 206, "Student Laptops", 61 },
                    { 207, "General Purpose Laptops", 61 },
                    { 208, "2-in-1 Convertible Laptops", 61 },
                    { 209, "Chromebook", 61 },
                    { 210, "Mac Laptops", 61 },
                    { 211, "Thin Clients", 64 },
                    { 212, "Micro PCs", 64 },
                    { 213, "Stick PCs", 64 },
                    { 214, "Mini PCs", 64 },
                    { 215, "Engineering Workstations", 65 },
                    { 216, "Video Editing Workstations", 65 },
                    { 217, "AI / Data Workstations", 65 },
                    { 218, "CAD Workstations", 65 },
                    { 219, "Casual Dresses", 66 },
                    { 220, "Formal / Office Dresses", 66 },
                    { 221, "Evening / Party Dresses", 66 },
                    { 222, "Maxi Dresses", 66 },
                    { 223, "Bodycon Dresses", 66 },
                    { 224, "Dresses for Pregnant Women", 66 },
                    { 225, "African Print Dresses", 66 },
                    { 226, "Formal Trousers", 68 },
                    { 227, "Casual Pants", 68 },
                    { 228, "Leggings", 68 },
                    { 229, "Sports & Athletic Pants", 68 },
                    { 230, "Jackets & Hoodies", 70 },
                    { 231, "Cardigans & Coats", 70 },
                    { 232, "Mini Skirts", 69 },
                    { 233, "Maxi Skirts", 69 },
                    { 234, "General Purpose Skirts", 69 },
                    { 235, "Gym & Yoga Wear", 74 },
                    { 236, "Tracksuits", 74 },
                    { 237, "T-Shirts", 67 },
                    { 238, "Tank Tops / Camisoles", 67 },
                    { 239, "Shirts & Blouses", 67 },
                    { 240, "Two-Piece Sets", 71 },
                    { 241, "Skirt & Pant Suits", 71 },
                    { 242, "Lounge Sets", 71 },
                    { 243, "Gomesi / Busuuti", 73 },
                    { 244, "Kitenge Wear", 73 },
                    { 245, "African Gowns", 73 },
                    { 246, "Ankara Wear", 73 },
                    { 247, "Nigerian Style", 73 },
                    { 248, "Casual Jumpsuits", 72 },
                    { 249, "Formal Jumpsuits", 72 },
                    { 250, "Rompers", 72 },
                    { 251, "Drinking Yoghurt", 77 },
                    { 252, "Fresh Milk", 77 },
                    { 253, "Flavoured Milk", 77 },
                    { 254, "Cultured Milk", 77 },
                    { 255, "Groundnut Paste", 79 },
                    { 256, "Simsim Paste", 79 },
                    { 257, "Peanut Butter", 79 },
                    { 258, "Fruit Juice", 76 },
                    { 259, "Soft Drinks", 76 },
                    { 260, "Energy Drinks", 76 },
                    { 261, "Flavoured Drinks", 76 },
                    { 262, "Malt Drinks", 76 },
                    { 263, "Bushera", 75 },
                    { 264, "Natural Juice (No Preservatives)", 75 },
                    { 265, "Fermented Traditional Drinks", 75 },
                    { 266, "Roasted Groundnuts", 78 },
                    { 267, "Powdered Groundnuts", 78 },
                    { 268, "Roasted Simsim", 78 },
                    { 269, "Simsim Seeds", 78 },
                    { 270, "Power Banks", 82 },
                    { 271, "Chargers & Adapters", 82 },
                    { 272, "Cables", 82 },
                    { 273, "Memory Cards", 82 },
                    { 274, "USB Flash Drives", 82 },
                    { 275, "Device Stands & Holders", 82 },
                    { 276, "Smart Watches", 81 },
                    { 277, "Fitness Trackers", 81 },
                    { 278, "Smart Glasses", 81 },
                    { 279, "VR Headsets", 81 },
                    { 280, "AR Devices", 81 },
                    { 281, "Digital Cameras", 80 },
                    { 282, "DSLR Cameras", 80 },
                    { 283, "Mirrorless Cameras", 80 },
                    { 284, "Action Cameras", 80 },
                    { 285, "Camcorders", 80 },
                    { 286, "Camera Lenses", 80 },
                    { 287, "Camera Accessories", 80 },
                    { 288, "Earbuds / Earphones", 4 },
                    { 289, "Soundbars", 4 },
                    { 290, "Home Theater Systems", 4 },
                    { 291, "Studio Microphones", 4 },
                    { 292, "Portable Radios", 4 },
                    { 293, "Body Skincare", 11 },
                    { 294, "Baby Skincare", 11 },
                    { 295, "Facial Care Tools", 84 },
                    { 296, "Skincare Accessories", 84 },
                    { 297, "Rowing Machine", 12 },
                    { 298, "Ellipticals", 12 },
                    { 299, "Stair Climbers", 12 },
                    { 300, "Dumbbells", 86 },
                    { 301, "Barbells", 86 },
                    { 302, "Kettlebells", 86 },
                    { 303, "Weight Plates", 86 },
                    { 304, "Resistance Bands", 87 },
                    { 305, "Medicine Balls", 87 },
                    { 306, "Battle Ropes", 87 },
                    { 307, "Suspension Trainers", 87 },
                    { 308, "Yoga Mats", 88 },
                    { 309, "Gloves", 88 },
                    { 310, "Lifting Belts", 88 },
                    { 311, "Foam Rollers", 88 },
                    { 312, "Football / Soccer Equipment", 89 },
                    { 313, "Basketball Equipment", 89 },
                    { 314, "Volleyball Equipment", 89 },
                    { 315, "Athletics Training Equipment", 89 },
                    { 316, "Cycling Equipment", 89 },
                    { 317, "Weight Benches", 85 },
                    { 318, "Squat Racks", 85 },
                    { 319, "Power Racks", 85 },
                    { 320, "Cable Machines", 85 },
                    { 321, "T-Shirts", 90 },
                    { 322, "Shirts", 90 },
                    { 323, "Polo Shirts", 90 },
                    { 324, "Sweaters & Cardigans", 90 },
                    { 325, "Hoodies & Sweatshirts", 90 },
                    { 326, "Jeans", 91 },
                    { 327, "Trousers", 91 },
                    { 328, "Shorts", 91 },
                    { 329, "Joggers & Sweatpants", 91 },
                    { 330, "Kanzu", 92 },
                    { 331, "African Suits", 92 },
                    { 332, "Traditional Shirts", 92 },
                    { 333, "Jackets", 93 },
                    { 334, "Coats", 93 },
                    { 335, "Blazers", 93 },
                    { 336, "Underwear", 94 },
                    { 337, "Undershirts", 94 },
                    { 338, "Sleepwear", 94 },
                    { 339, "Tracksuits", 95 },
                    { 340, "Gym Wear", 95 },
                    { 341, "Football Kits", 95 },
                    { 342, "Suits", 96 },
                    { 343, "Waistcoats", 96 },
                    { 344, "Tuxedos", 96 },
                    { 345, "Belts", 97 },
                    { 346, "Hats & Caps", 97 },
                    { 347, "Scarves & Gloves", 97 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PhoneNumber",
                table: "AspNetUsers",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerProfile_BuyerTierId",
                table: "BuyerProfile",
                column: "BuyerTierId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerProfile_BuyerTypeId",
                table: "BuyerProfile",
                column: "BuyerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerProfile_UserGroupId",
                table: "BuyerProfile",
                column: "UserGroupId",
                unique: true,
                filter: "[UserGroupId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_GeneralCategoryId",
                table: "Categories",
                column: "GeneralCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryAttributes_AttributeDataTypeId",
                table: "CategoryAttributes",
                column: "AttributeDataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryAttributes_CategoryId_AttributeName",
                table: "CategoryAttributes",
                columns: new[] { "CategoryId", "AttributeName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_ApplicationUserId",
                table: "GroupMembers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupRoleId",
                table: "GroupMembers",
                column: "GroupRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_MemberStatusId",
                table: "GroupMembers",
                column: "MemberStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_UserGroupId",
                table: "GroupMembers",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_UserProfileId",
                table: "GroupMembers",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupSellers_GroupMemberId",
                table: "GroupSellers",
                column: "GroupMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupSellers_SellerProfileId",
                table: "GroupSellers",
                column: "SellerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ApplicationUserId",
                table: "Orders",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PlacedByProfileId",
                table: "Orders",
                column: "PlacedByProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserGroupId",
                table: "Orders",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_CategoryAttributeId",
                table: "ProductAttributeValues",
                column: "CategoryAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_ProductId_CategoryAttributeId",
                table: "ProductAttributeValues",
                columns: new[] { "ProductId", "CategoryAttributeId" },
                unique: true,
                filter: "[ProductId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId_IsActive_IsDeleted_Price",
                table: "Products",
                columns: new[] { "CategoryId", "IsActive", "IsDeleted", "Price" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedAt",
                table: "Products",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellerProfileId",
                table: "Products",
                column: "SellerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategoryCategoryId",
                table: "Products",
                column: "SubCategoryCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ApplicationUserId",
                table: "Reviews",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TradeId",
                table: "Reviews",
                column: "TradeId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerProfiles_SellerOfferingId",
                table: "SellerProfiles",
                column: "SellerOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerProfiles_SellerPolicyId",
                table: "SellerProfiles",
                column: "SellerPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerProfiles_SellerTypeId",
                table: "SellerProfiles",
                column: "SellerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerRestrictions_SellerProfileId",
                table: "SellerRestrictions",
                column: "SellerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryCategories_SubCategoryId",
                table: "SubCategoryCategories",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeAttributeValues_CategoryAttributeId",
                table: "TradeAttributeValues",
                column: "CategoryAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeAttributeValues_TradeId_CategoryAttributeId",
                table: "TradeAttributeValues",
                columns: new[] { "TradeId", "CategoryAttributeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TradeBooking_BookingStatusId",
                table: "TradeBooking",
                column: "BookingStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeBooking_TradeId",
                table: "TradeBooking",
                column: "TradeId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeBooking_UserProfileId",
                table: "TradeBooking",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeImages_TradeId",
                table: "TradeImages",
                column: "TradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_CategoryId",
                table: "Trades",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_CreatedAt",
                table: "Trades",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_SellerProfileId",
                table: "Trades",
                column: "SellerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_SubCategoryCategoryId",
                table: "Trades",
                column: "SubCategoryCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_SubCategoryId",
                table: "Trades",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDevices_DeviceId",
                table: "UserDevices",
                column: "DeviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDevices_UserId",
                table: "UserDevices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupNotifications_UserGroupId",
                table: "UserGroupNotifications",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupNotifications_UserProfileId",
                table: "UserGroupNotifications",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_AddressId",
                table: "UserGroups",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_GroupCategoryId",
                table: "UserGroups",
                column: "GroupCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_GroupTypeId",
                table: "UserGroups",
                column: "GroupTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_SellerProfileId",
                table: "UserGroups",
                column: "SellerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_UserStatusId",
                table: "UserGroups",
                column: "UserStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupsSellers_SellerProfileId",
                table: "UserGroupsSellers",
                column: "SellerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupsSellers_UserGroupId",
                table: "UserGroupsSellers",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOtps_UserId",
                table: "UserOtps",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_AddressId",
                table: "UserProfiles",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_BuyerProfileId",
                table: "UserProfiles",
                column: "BuyerProfileId",
                unique: true,
                filter: "[BuyerProfileId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_IdentityUserId",
                table: "UserProfiles",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_SellerProfileId",
                table: "UserProfiles",
                column: "SellerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserStatusId",
                table: "UserProfiles",
                column: "UserStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_TokenHash",
                table: "UserRefreshTokens",
                column: "TokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_UserId",
                table: "UserRefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItem_ProductId",
                table: "WishListItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItem_WishListId",
                table: "WishListItem",
                column: "WishListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "CommodityClasses");

            migrationBuilder.DropTable(
                name: "GroupSellers");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ProductAttributeValues");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "SellerRestrictions");

            migrationBuilder.DropTable(
                name: "SellerTiers");

            migrationBuilder.DropTable(
                name: "SellerViolations");

            migrationBuilder.DropTable(
                name: "TradeAttributeValues");

            migrationBuilder.DropTable(
                name: "TradeBooking");

            migrationBuilder.DropTable(
                name: "TradeImages");

            migrationBuilder.DropTable(
                name: "UserDevices");

            migrationBuilder.DropTable(
                name: "UserGroupNotifications");

            migrationBuilder.DropTable(
                name: "UserGroupsSellers");

            migrationBuilder.DropTable(
                name: "UserOtps");

            migrationBuilder.DropTable(
                name: "UserRefreshTokens");

            migrationBuilder.DropTable(
                name: "WishListItem");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "GroupMembers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "CategoryAttributes");

            migrationBuilder.DropTable(
                name: "BookingStatus");

            migrationBuilder.DropTable(
                name: "Trades");

            migrationBuilder.DropTable(
                name: "WishlistItems");

            migrationBuilder.DropTable(
                name: "GroupRole");

            migrationBuilder.DropTable(
                name: "MemberStatus");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "AttributeDataTypes");

            migrationBuilder.DropTable(
                name: "SubCategoryCategories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BuyerProfile");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "BuyerTiers");

            migrationBuilder.DropTable(
                name: "BuyerTypes");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "GroupCategories");

            migrationBuilder.DropTable(
                name: "GroupTypes");

            migrationBuilder.DropTable(
                name: "SellerProfiles");

            migrationBuilder.DropTable(
                name: "UserStatuses");

            migrationBuilder.DropTable(
                name: "GeneralCategories");

            migrationBuilder.DropTable(
                name: "SellerOfferings");

            migrationBuilder.DropTable(
                name: "SellerPolicies");

            migrationBuilder.DropTable(
                name: "SellerTypes");
        }
    }
}
