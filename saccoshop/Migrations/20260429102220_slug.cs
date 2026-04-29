using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class slug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "UserGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Trades",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "SellerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "ProductImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6daf6294-b644-48c8-b6b4-62c9cbe5ebfa", new DateTime(2026, 4, 29, 10, 22, 17, 840, DateTimeKind.Utc).AddTicks(9715), "AQAAAAIAAYagAAAAENv0xMnOuVn5BuTayF795o1MuAUxRdNh9h2xefKhgOkus7V4xadBmtjr4oQszOkhbQ==", "ba9e346f-8724-43bd-87f4-5d1e55b36c0c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f6ce8c9-2b97-4c49-9264-5c9b7b01d10a", new DateTime(2026, 4, 29, 10, 22, 17, 946, DateTimeKind.Utc).AddTicks(9391), "AQAAAAIAAYagAAAAEEE/0K9i0KXee3pREbsD5F4YmudAIlHUP226XJfBn2pQE3lcaVLtEHVTrG+u5vHc5g==", "cb8e74b9-47df-492a-ab84-f59debfee633" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c71a6f0-d272-44a7-9656-eabe9589135a", new DateTime(2026, 4, 29, 10, 22, 18, 51, DateTimeKind.Utc).AddTicks(2124), "AQAAAAIAAYagAAAAENTsRa9E1frxSMuwRv2rV2Cz5D9WgsG6ISMZbHkP9C4hrH51cBnA9byv0Z6yay7xFQ==", "7d1394dd-e3e5-4dee-9b24-fe7e223ffcb5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea814965-71ea-4ab4-91b0-b67ea33fb840", new DateTime(2026, 4, 29, 10, 22, 18, 154, DateTimeKind.Utc).AddTicks(2868), "AQAAAAIAAYagAAAAEKcIP/3NqsnhcnHdhVYZ2sD2Vk5xwEE6JPCwGovxFsUi9BS3F6bGsc+okg/kFvYMlQ==", "d3431c1f-e8f1-4dfa-a22a-9531d03192c2" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 4, 29, 10, 22, 18, 271, DateTimeKind.Utc).AddTicks(6291), "" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 4, 29, 10, 22, 18, 271, DateTimeKind.Utc).AddTicks(6384), "" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 4, 29, 10, 22, 18, 271, DateTimeKind.Utc).AddTicks(6392), "" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                columns: new[] { "CreatedAt", "Slug" },
                values: new object[] { new DateTime(2026, 4, 29, 10, 22, 18, 271, DateTimeKind.Utc).AddTicks(6396), "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "SellerProfiles");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "ProductImages");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b9cc1ec-dc0f-4a64-991e-e3a3c204e45b", new DateTime(2026, 4, 26, 22, 21, 56, 482, DateTimeKind.Utc).AddTicks(5306), "AQAAAAIAAYagAAAAEPZRbL9XXXl5pyqIs3VQhpOOzMjniDuYc2N7yXKS2Tks4UG1/0fGjjWCvi0Nmpx+yw==", "bd85e070-c937-4c3b-8586-1b22f0f6844b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70ff1a8f-ca4a-4cc2-975b-038e31ae207e", new DateTime(2026, 4, 26, 22, 21, 56, 573, DateTimeKind.Utc).AddTicks(9571), "AQAAAAIAAYagAAAAEBirg2ZV+IGIB07SDDxKvNvEuVDAyAUlMfeMJURzmxjpRDV1cDVDX6/xFE3VvhYqIQ==", "4e3592ef-814f-4249-8303-a0f60391db98" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78ae4234-6b39-42e2-bcb7-512af2e079b0", new DateTime(2026, 4, 26, 22, 21, 56, 677, DateTimeKind.Utc).AddTicks(2125), "AQAAAAIAAYagAAAAEBdMx9j4DVEtJZdW3xK4wtrVKGsQ77XgaAk/FeLD848Ifxce6xA/kgo3FknR1hbAhw==", "596f5323-db03-455b-9c5e-b33cc385e6dd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51e1abbf-3316-419d-94a3-11580ddcb4de", new DateTime(2026, 4, 26, 22, 21, 56, 777, DateTimeKind.Utc).AddTicks(3061), "AQAAAAIAAYagAAAAEPli3Xb/GxyNcwlL6ctZmimRSSrEntZc4cM3QyEIBBsKVP69j/rGbQMzMkU3XtZiGw==", "90520782-254f-49dd-8ba7-7e3266394992" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 26, 22, 21, 56, 873, DateTimeKind.Utc).AddTicks(9253));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 26, 22, 21, 56, 873, DateTimeKind.Utc).AddTicks(9304));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 26, 22, 21, 56, 873, DateTimeKind.Utc).AddTicks(9309));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 26, 22, 21, 56, 873, DateTimeKind.Utc).AddTicks(9312));
        }
    }
}
