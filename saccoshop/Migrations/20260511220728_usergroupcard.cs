using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class usergroupcard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsGroupCard",
                table: "HomePageCardCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "UserGroupId",
                table: "HomePageCardCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "946c88af-25e8-48f2-b631-ab118a02d05c", new DateTime(2026, 5, 11, 22, 7, 21, 632, DateTimeKind.Utc).AddTicks(281), "AQAAAAIAAYagAAAAEDLB+tN4DnYMKQWTHBRaOMJWbQovh/aZ4u2gScEijteUepMD6X9JCbJLrW54v1W6iw==", "61fe2cfe-b064-4d40-b455-3b4676ead86c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4e3a4b07-dabf-4c36-869c-35090d933221", new DateTime(2026, 5, 11, 22, 7, 21, 721, DateTimeKind.Utc).AddTicks(1740), "AQAAAAIAAYagAAAAEAf6ELvRtqgDibyJa+mkCIU9tBaeEChgJQB2nVfSDWaWH0+OJcjRIizrpTcKPwqG1Q==", "f6aaca50-82ef-47e2-ba4b-3eff929b26ce" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c8d9a9b-546b-4504-9d83-b1eec2004a57", new DateTime(2026, 5, 11, 22, 7, 21, 809, DateTimeKind.Utc).AddTicks(8409), "AQAAAAIAAYagAAAAEO1w/ugSe5S5yfQGH55iFFn1mxJqqmvPuzzVXCgJIBqlv9/LEfrDxUQiOqXgPEDLYg==", "bfb29b0a-260f-4424-93fe-4f3ff4df0727" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5b78194b-700e-4622-8a86-6e70d9464831", new DateTime(2026, 5, 11, 22, 7, 21, 898, DateTimeKind.Utc).AddTicks(4741), "AQAAAAIAAYagAAAAEKnfM697BKYatiPzsDSTc2WOjgZ+hEeqB0W6P8SXpJt8SCDOr2TapXJ0dQi9aVdOUw==", "a5b59701-5912-4f7e-8ee4-901329a11f7d" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 7, 21, 987, DateTimeKind.Utc).AddTicks(3565));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 7, 21, 987, DateTimeKind.Utc).AddTicks(3583));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 7, 21, 987, DateTimeKind.Utc).AddTicks(3586));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 7, 21, 987, DateTimeKind.Utc).AddTicks(3589));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGroupCard",
                table: "HomePageCardCategories");

            migrationBuilder.DropColumn(
                name: "UserGroupId",
                table: "HomePageCardCategories");

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
    }
}
