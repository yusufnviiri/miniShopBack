using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class reviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.CreateTable(
                name: "ProductReviews",
                columns: table => new
                {
                    ProductReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviews", x => x.ProductReviewId);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductReviews_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "UserProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradeReviews",
                columns: table => new
                {
                    TradeReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeReviews", x => x.TradeReviewId);
                    table.ForeignKey(
                        name: "FK_TradeReviews_Trades_TradeId",
                        column: x => x.TradeId,
                        principalTable: "Trades",
                        principalColumn: "TradeId");
                    table.ForeignKey(
                        name: "FK_TradeReviews_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "UserProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce5ad8a5-2578-49b7-80d5-73b6df66b4f1", new DateTime(2026, 4, 12, 12, 12, 48, 103, DateTimeKind.Utc).AddTicks(9459), "AQAAAAIAAYagAAAAEI9QqxvI1Lm1TBwgx/2onMQowVpmBmHtdnngpy2at/ZoBzg/gtqmrrSWzGBV0PkONw==", "714281fd-bb97-4ec1-ad68-4a4e7917fc66" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b4e2b28-6cb3-4863-9a9c-cffcde203e8c", new DateTime(2026, 4, 12, 12, 12, 48, 228, DateTimeKind.Utc).AddTicks(4536), "AQAAAAIAAYagAAAAEDMM8detlNkaExq3nV6+CceCfWxvokyH3yJqB1YR2VC3JjdSMSkFrmf5vD+ZV7fUow==", "f519d305-5f85-4a7d-b2d8-bcad6e448dd9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63a780da-137a-4455-8b23-cffbe9e948fe", new DateTime(2026, 4, 12, 12, 12, 48, 345, DateTimeKind.Utc).AddTicks(3281), "AQAAAAIAAYagAAAAEBXIJ+GGjCUQNbWVjKxhiZIlWjYeRzivmoq1Pskjcq0Uu7W+W0z2kyDi5DAAzdt/2Q==", "68516d23-9b55-452a-8a77-01ebb09ebabc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3892a70b-80c2-46a6-a0ab-7bb1f1fa289e", new DateTime(2026, 4, 12, 12, 12, 48, 467, DateTimeKind.Utc).AddTicks(324), "AQAAAAIAAYagAAAAED3CjUeHmInbmDG1JaYRsXYl3lt/VBUL5R64GvYtKKzCA8H37O3eAl9ZQtbw9J65Iw==", "62234cf4-2954-435e-bd03-2a9c948e5602" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 12, 12, 12, 48, 582, DateTimeKind.Utc).AddTicks(8017));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 12, 12, 12, 48, 582, DateTimeKind.Utc).AddTicks(8036));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 12, 12, 12, 48, 582, DateTimeKind.Utc).AddTicks(8040));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 12, 12, 12, 48, 582, DateTimeKind.Utc).AddTicks(8043));

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId",
                table: "ProductReviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_UserProfileId",
                table: "ProductReviews",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeReviews_TradeId",
                table: "TradeReviews",
                column: "TradeId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeReviews_UserProfileId",
                table: "TradeReviews",
                column: "UserProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductReviews");

            migrationBuilder.DropTable(
                name: "TradeReviews");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ae2da493-14b5-480d-ba91-362d72177e6c", new DateTime(2026, 4, 10, 18, 5, 55, 423, DateTimeKind.Utc).AddTicks(3961), "AQAAAAIAAYagAAAAELgumlGXaDReAuH8hm1wpCZFSPnjridblReyOGNhmrexSNJ1qDDIkv93ttIP7aD/NQ==", "4b88a2e4-4dd8-475a-97cb-381512fdb483" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "05201254-4bc1-4fed-bf80-add500b14ded", new DateTime(2026, 4, 10, 18, 5, 55, 513, DateTimeKind.Utc).AddTicks(6353), "AQAAAAIAAYagAAAAEM/v23JLbiuitWGNNX1TR/Kh1ONjGl3iU0C8ROmiEVq5b60u8DHgKsDailT2h5zHuw==", "dfc14fcf-80f8-405a-ab1c-3d2739917cfd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "193c9d16-de07-4260-840a-0f33d1cea41d", new DateTime(2026, 4, 10, 18, 5, 55, 607, DateTimeKind.Utc).AddTicks(154), "AQAAAAIAAYagAAAAEP0z/ckPScRen8izHMNei3VmYlbLgir4AhKfVf9kAAauD4edgjARB3U722pVMtuLpw==", "83f0211b-3d1d-4a5e-a2ac-886fe880c797" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a2851c5-0f8e-4ff1-b32a-53fae5fe2171", new DateTime(2026, 4, 10, 18, 5, 55, 703, DateTimeKind.Utc).AddTicks(4455), "AQAAAAIAAYagAAAAEIR7Mbibi3Jp+Rym9PowgF/viKIECMXg5Tv864jUO0urzV+kYqhhBS6RHU+qeUpowQ==", "b6b6d968-4fc8-4ff9-a5c2-7ebeb799496d" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 18, 5, 55, 797, DateTimeKind.Utc).AddTicks(9327));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 18, 5, 55, 797, DateTimeKind.Utc).AddTicks(9345));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 18, 5, 55, 797, DateTimeKind.Utc).AddTicks(9349));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 18, 5, 55, 797, DateTimeKind.Utc).AddTicks(9353));

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
        }
    }
}
