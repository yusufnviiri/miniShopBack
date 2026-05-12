using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class undousercardrs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserGroupId",
                table: "HomePageCards",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38f66af0-08f2-4bbb-9d48-2ef739ba51ee", new DateTime(2026, 5, 12, 11, 54, 20, 127, DateTimeKind.Utc).AddTicks(5433), "AQAAAAIAAYagAAAAEOZWzDOPNjKPBYu14hfiBkuXZmtbXAOC+uI6jzL70uz+WfePCs9Ti4oafb2lwRBo9A==", "6761fe0f-a2c1-48a1-ba3d-ed50bdcf4b68" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "946ee49c-cbff-4633-8a85-1cd0678d3c9e", new DateTime(2026, 5, 12, 11, 54, 20, 226, DateTimeKind.Utc).AddTicks(7398), "AQAAAAIAAYagAAAAEIGSKreoATmIH9e9r98JIuP4e4J9P/P/HODYgCXA3eGnaIYcqH8l+K7UTRhsGHz0Tw==", "22fbd8a9-df3a-402d-ae21-bcf7a979e625" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc3e26b1-b70d-41c2-8b59-eace154c2a8c", new DateTime(2026, 5, 12, 11, 54, 20, 343, DateTimeKind.Utc).AddTicks(7185), "AQAAAAIAAYagAAAAEFcHW8d2Gs49qqvneFJKdx+3aPKnrpZzVlRCZuknEBpNoyLVwfZvtgIfls+r/g4DNQ==", "45c9dce1-46f8-42d2-b643-57cd57c7052d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0d7339a6-d559-480f-afc7-f3c6cb5524ab", new DateTime(2026, 5, 12, 11, 54, 20, 533, DateTimeKind.Utc).AddTicks(6), "AQAAAAIAAYagAAAAEEPSkIDdjOrsbDO2Sdo35fhKI0ADCuxmDx36v+YVnT6VdkXJTzeuD7zWW6FCzD77QA==", "8c89ff85-cc19-4765-b0a5-6fc7160576c7" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 11, 54, 20, 639, DateTimeKind.Utc).AddTicks(9126));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 11, 54, 20, 639, DateTimeKind.Utc).AddTicks(9179));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 11, 54, 20, 639, DateTimeKind.Utc).AddTicks(9183));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 11, 54, 20, 639, DateTimeKind.Utc).AddTicks(9187));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserGroupId",
                table: "HomePageCards",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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
    }
}
