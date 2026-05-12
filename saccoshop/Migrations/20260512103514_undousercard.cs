using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class undousercard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGroupCard",
                table: "HomePageCardCategories");

            migrationBuilder.DropColumn(
                name: "UserGroupId",
                table: "HomePageCardCategories");

            migrationBuilder.AddColumn<bool>(
                name: "IsGroupCard",
                table: "HomePageCards",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "UserGroupId",
                table: "HomePageCards",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0cbea26d-12b7-4cd2-9106-3011df95d990", new DateTime(2026, 5, 12, 10, 35, 12, 151, DateTimeKind.Utc).AddTicks(9522), "AQAAAAIAAYagAAAAEHE0Js0I94h6YjmcbqR3zBRFl8HH4uM9s6wqGzPJP3v2j6v0J4DH/9x/hzZGZIuiQw==", "c005425d-b66e-40db-8f13-e724593c138c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ca77b1a-3b84-46a6-a675-9551df55d925", new DateTime(2026, 5, 12, 10, 35, 12, 246, DateTimeKind.Utc).AddTicks(3266), "AQAAAAIAAYagAAAAENGG4hvoTfhpC0LSnRrkCQMzCX1dsCdufgItrdtSAWYO2h1UGtazXTXwHYtlAmWG+g==", "b0ce6a90-f7a8-4640-917f-85497a599ed0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06ba0af1-5d2e-4632-a179-6a345b0daf64", new DateTime(2026, 5, 12, 10, 35, 12, 364, DateTimeKind.Utc).AddTicks(1825), "AQAAAAIAAYagAAAAEG2MGZDOSMnuescOjfqh1ODnl+31+3k1EVhPuYe9d0fmlbW5I5WVDMoVbOAykADGAA==", "b8c69b9c-006d-4709-9331-d65e3f5c8c1c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "488903ef-7f1d-4f92-9c8f-d9d78fca2be3", new DateTime(2026, 5, 12, 10, 35, 12, 477, DateTimeKind.Utc).AddTicks(6880), "AQAAAAIAAYagAAAAEGOwMbspKJmJ78HdHNHOw8h8qrTo8vVuf/FtW/ubOB7olb0CeXiyrXU3JUQQBRuaSA==", "27760902-67c0-4a7d-9953-e11e2dfaff81" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 10, 35, 12, 598, DateTimeKind.Utc).AddTicks(2211));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 10, 35, 12, 598, DateTimeKind.Utc).AddTicks(2254));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 10, 35, 12, 598, DateTimeKind.Utc).AddTicks(2259));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 10, 35, 12, 598, DateTimeKind.Utc).AddTicks(2262));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGroupCard",
                table: "HomePageCards");

            migrationBuilder.DropColumn(
                name: "UserGroupId",
                table: "HomePageCards");

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
    }
}
