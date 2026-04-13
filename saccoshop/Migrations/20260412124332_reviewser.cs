using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class reviewser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviews_UserProfiles_UserProfileId",
                table: "ProductReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_TradeReviews_Trades_TradeId",
                table: "TradeReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_TradeReviews_UserProfiles_UserProfileId",
                table: "TradeReviews");

            migrationBuilder.DropIndex(
                name: "IX_TradeReviews_TradeId",
                table: "TradeReviews");

            migrationBuilder.DropIndex(
                name: "IX_ProductReviews_ProductId",
                table: "ProductReviews");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fd923fa8-9013-4d5b-b1c4-090f637d1832", new DateTime(2026, 4, 12, 12, 43, 30, 201, DateTimeKind.Utc).AddTicks(51), "AQAAAAIAAYagAAAAEAsUuCaXqN4TY/bTnLMGl/a6zesspOpVo7BIZQEWzZOW/uxpBRW5kt/TvMXKcPJWWQ==", "683acf68-ba97-4609-9673-4c03e897efac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5bfe5865-9276-4eb8-84d1-22e1ec853be8", new DateTime(2026, 4, 12, 12, 43, 30, 295, DateTimeKind.Utc).AddTicks(2067), "AQAAAAIAAYagAAAAED/3iZz/ExZ7YeGkKRgr2THeA2pSzuU4cCo5f52akRplFYdsK6XQzwZnXId5G+FE8A==", "3077e4fc-a45f-476f-9dc4-6a4bb743ae89" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e9d1785-b16b-4af1-a433-0b98eb24b5e5", new DateTime(2026, 4, 12, 12, 43, 30, 404, DateTimeKind.Utc).AddTicks(610), "AQAAAAIAAYagAAAAENTvhueSVW1BPHkpSp4eXma2w0A8DfGPap8RaLFncHUTv4evoRHcJ4fXCKLvUzvpZA==", "2dfb0a81-d296-449f-9323-3170a5460409" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ade4b1d7-c186-4c2d-a32e-9a29ce7385df", new DateTime(2026, 4, 12, 12, 43, 30, 502, DateTimeKind.Utc).AddTicks(2245), "AQAAAAIAAYagAAAAEOXV0wjZ5hnQGqbcijQlvSKzRAU5sh6v9nzu0QHIHM+L008s/IBo1vJPJDru2jOQhQ==", "69e3a062-9d47-4231-adbc-00832e7ef1af" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 12, 12, 43, 30, 600, DateTimeKind.Utc).AddTicks(1897));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 12, 12, 43, 30, 600, DateTimeKind.Utc).AddTicks(1937));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 12, 12, 43, 30, 600, DateTimeKind.Utc).AddTicks(1942));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 12, 12, 43, 30, 600, DateTimeKind.Utc).AddTicks(1945));

            migrationBuilder.CreateIndex(
                name: "IX_TradeReviews_TradeId_UserProfileId",
                table: "TradeReviews",
                columns: new[] { "TradeId", "UserProfileId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId_UserProfileId",
                table: "ProductReviews",
                columns: new[] { "ProductId", "UserProfileId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviews_UserProfiles_UserProfileId",
                table: "ProductReviews",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TradeReviews_Trades_TradeId",
                table: "TradeReviews",
                column: "TradeId",
                principalTable: "Trades",
                principalColumn: "TradeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TradeReviews_UserProfiles_UserProfileId",
                table: "TradeReviews",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviews_UserProfiles_UserProfileId",
                table: "ProductReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_TradeReviews_Trades_TradeId",
                table: "TradeReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_TradeReviews_UserProfiles_UserProfileId",
                table: "TradeReviews");

            migrationBuilder.DropIndex(
                name: "IX_TradeReviews_TradeId_UserProfileId",
                table: "TradeReviews");

            migrationBuilder.DropIndex(
                name: "IX_ProductReviews_ProductId_UserProfileId",
                table: "ProductReviews");

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
                name: "IX_TradeReviews_TradeId",
                table: "TradeReviews",
                column: "TradeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId",
                table: "ProductReviews",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviews_UserProfiles_UserProfileId",
                table: "ProductReviews",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TradeReviews_Trades_TradeId",
                table: "TradeReviews",
                column: "TradeId",
                principalTable: "Trades",
                principalColumn: "TradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TradeReviews_UserProfiles_UserProfileId",
                table: "TradeReviews",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
