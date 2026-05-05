using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class gga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "SellerProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SellerLocation",
                table: "SellerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f03fc34-96b0-4583-ab76-70974892d248", new DateTime(2026, 5, 4, 23, 57, 2, 675, DateTimeKind.Utc).AddTicks(5622), "AQAAAAIAAYagAAAAEN5g4O7/yWzQdx9CKuRruKQPi9ricKP04j1C4vxuET45InvH3FA2hGHPwkNDdNhXkQ==", "1ad68f67-8837-4a18-a932-338117b9545e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "28cfafc5-0243-4e20-8c4c-ac1ed250dffd", new DateTime(2026, 5, 4, 23, 57, 2, 792, DateTimeKind.Utc).AddTicks(4715), "AQAAAAIAAYagAAAAEAEtwh/rwfKjXmraudu2OwulBnmg9JRRV1RoFRV4gaVxXTIhOJLPuFRH9Xkma4E6LA==", "c5e3c6f9-dd53-4d9b-a13d-4e4c5468f741" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cce39488-fd0e-4b72-8e1b-703c89efc0cd", new DateTime(2026, 5, 4, 23, 57, 2, 896, DateTimeKind.Utc).AddTicks(2981), "AQAAAAIAAYagAAAAEMAIye2xSuAlZTsg3vVRmqVZb6CuHBA7kIgdBAXgB0vniTuPuAWmofJCB9jeldvVJQ==", "53432411-b719-45e0-b41a-8c3ec9040542" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ef250af-f8ac-42df-a303-b6e3ca726f2a", new DateTime(2026, 5, 4, 23, 57, 2, 997, DateTimeKind.Utc).AddTicks(4433), "AQAAAAIAAYagAAAAEOVFaTffuRsmmyKUn9iVV7skrw8gIaSn8ylB9AVifBZlT2jThwZi6CN5zgDWspmz/g==", "aa92d1ad-00ce-4f51-920e-b5ccce197ea7" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 23, 57, 3, 100, DateTimeKind.Utc).AddTicks(9353));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 23, 57, 3, 100, DateTimeKind.Utc).AddTicks(9397));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 23, 57, 3, 100, DateTimeKind.Utc).AddTicks(9402));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 23, 57, 3, 100, DateTimeKind.Utc).AddTicks(9406));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "SellerProfiles");

            migrationBuilder.DropColumn(
                name: "SellerLocation",
                table: "SellerProfiles");

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
                column: "CreatedAt",
                value: new DateTime(2026, 4, 29, 10, 22, 18, 271, DateTimeKind.Utc).AddTicks(6291));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 29, 10, 22, 18, 271, DateTimeKind.Utc).AddTicks(6384));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 29, 10, 22, 18, 271, DateTimeKind.Utc).AddTicks(6392));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 29, 10, 22, 18, 271, DateTimeKind.Utc).AddTicks(6396));
        }
    }
}
