using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class pinned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_SellerProfiles_SellerProfileId",
                table: "UserProfiles");

            migrationBuilder.AddColumn<bool>(
                name: "IsPinned",
                table: "UserGroups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1024fea9-67c8-4d34-a3a8-b33c2c4c8555", new DateTime(2026, 5, 31, 21, 33, 31, 561, DateTimeKind.Utc).AddTicks(5796), "AQAAAAIAAYagAAAAEA80MG6pbmvCkzUBmaErtfnsxtGxsHrpD44ww5JsHalH46KcQNZGUkBN8/EUp4kj7g==", "3ae0a224-4ddf-4fbe-8541-3ca13f7b922d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9856e658-cf9b-48d5-a24f-d91f20f897dc", new DateTime(2026, 5, 31, 21, 33, 31, 664, DateTimeKind.Utc).AddTicks(5820), "AQAAAAIAAYagAAAAECa7QTIDJJYzJ+zHBdzC6Mi8EbWQXCTFus1WrgUArgNxZ1+8o4rHLnxCE1OWoZOe3g==", "45aa59e1-1dba-4881-a775-490fad1ba0c6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ba0c124-d15b-47cc-98dc-017660333968", new DateTime(2026, 5, 31, 21, 33, 31, 767, DateTimeKind.Utc).AddTicks(9012), "AQAAAAIAAYagAAAAEDVBbxLalJY/8VGvew6Hkm0zLsGgYpvJ12YqTn1iGRFFQKu/L4cs8yXt984rW9gzsQ==", "9d9e4a57-092c-4e58-b7eb-974213211ea7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2cf8b43e-5769-4dc8-b248-fb01cb853e00", new DateTime(2026, 5, 31, 21, 33, 31, 870, DateTimeKind.Utc).AddTicks(7706), "AQAAAAIAAYagAAAAEODS0PgmEV8kXiehHELdSqDnkEUSMXcJYVMNxj177WX5JfoQZR0rH+V+9BEZijijtw==", "d75af071-3637-4e39-8ad8-f8612b867d49" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 31, 21, 33, 31, 973, DateTimeKind.Utc).AddTicks(5325));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 31, 21, 33, 31, 973, DateTimeKind.Utc).AddTicks(5373));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 31, 21, 33, 31, 973, DateTimeKind.Utc).AddTicks(5378));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 31, 21, 33, 31, 973, DateTimeKind.Utc).AddTicks(5381));

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_SellerProfileId",
                table: "UserProfiles",
                column: "SellerProfileId",
                unique: true,
                filter: "[SellerProfileId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_SellerProfiles_SellerProfileId",
                table: "UserProfiles",
                column: "SellerProfileId",
                principalTable: "SellerProfiles",
                principalColumn: "SellerProfileId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_SellerProfiles_SellerProfileId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_SellerProfileId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "IsPinned",
                table: "UserGroups");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e371df13-5994-4a28-9dd9-1056fabba33b", new DateTime(2026, 5, 24, 20, 15, 59, 677, DateTimeKind.Utc).AddTicks(1095), "AQAAAAIAAYagAAAAEK9TiqyX85vvrZRBsvVyZT0nWMGTJpGzp4/GSgY/tNSNAZuBJOoSu1blB0O/9VD2Bg==", "937fa9c0-cb88-4b6c-8c99-a0650c0df4cd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e08c54d7-d4d5-4125-b0bb-4c8b23e5c78c", new DateTime(2026, 5, 24, 20, 15, 59, 785, DateTimeKind.Utc).AddTicks(873), "AQAAAAIAAYagAAAAEGaA0ll/1y3DhMD2cdCdN91MCwf0EXsmNSmGFmH8iSCbEl8ssoV7JZcQ25PINiwU/g==", "5b950abb-2b91-45f3-9de0-22f700ac45cc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af559c48-68c0-46ee-9f41-6e6df2e0afae", new DateTime(2026, 5, 24, 20, 15, 59, 885, DateTimeKind.Utc).AddTicks(3231), "AQAAAAIAAYagAAAAEOXRwkClybz8UASdp3PKDrckaAbXHvoFV1TypRSyG2tNCkDgwLN6wNTnX40AoguNig==", "5a25e664-b72c-40af-a1ff-e7bbdcaeb65b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a0ae3dcb-94e0-4881-b682-ffd04bfd152f", new DateTime(2026, 5, 24, 20, 16, 0, 0, DateTimeKind.Utc).AddTicks(1863), "AQAAAAIAAYagAAAAELT9lC3BjZP4zqX//MG9VMIrjxXtVTn7fNM61vj6oVjpITxOOFp9IRu8ISAGg0ikDw==", "8934e8fd-d276-4029-aa83-a9a9007caed9" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 24, 20, 16, 0, 103, DateTimeKind.Utc).AddTicks(3968));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 24, 20, 16, 0, 103, DateTimeKind.Utc).AddTicks(4014));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 24, 20, 16, 0, 103, DateTimeKind.Utc).AddTicks(4020));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 24, 20, 16, 0, 103, DateTimeKind.Utc).AddTicks(4023));

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_SellerProfiles_SellerProfileId",
                table: "UserProfiles",
                column: "SellerProfileId",
                principalTable: "SellerProfiles",
                principalColumn: "SellerProfileId");
        }
    }
}
