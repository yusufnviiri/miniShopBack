using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class undousercardsellerslug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SellerSlugName",
                table: "HomePageCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "47e4fc1f-397c-4c46-ba5b-dee34aa0183d", new DateTime(2026, 5, 12, 13, 18, 49, 395, DateTimeKind.Utc).AddTicks(9996), "AQAAAAIAAYagAAAAEGAjliLRjUZJNp8F894W3iIlih3HDMUNxABvgLkSW8xIKBPtr3NNC9o9slx0cXmUQQ==", "430e3f87-afe6-4cce-987e-28f7b3950b8e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c37416f8-b56c-47d2-9f18-0e73baef02ae", new DateTime(2026, 5, 12, 13, 18, 49, 497, DateTimeKind.Utc).AddTicks(7387), "AQAAAAIAAYagAAAAEKOD2x9ps1o/7XmBWRKLkon66t1dsWeQhjJTGK3FerT1wuiqqoJp7RCGKpC2mRrCZw==", "8fb039c5-ef8b-415e-bf69-4d88f886d1cd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cd2bd50a-1549-4589-b232-0dd513a14a3d", new DateTime(2026, 5, 12, 13, 18, 49, 611, DateTimeKind.Utc).AddTicks(5204), "AQAAAAIAAYagAAAAEHtXXlggDpDkeOOlCzjBkBLfCBrJgTgFNxHvLXto8ogMFhNEZLcSnhoa5PgaZIGxZw==", "1c877195-839b-4664-b572-fef69d58b143" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4be12e79-3bf2-4943-8f58-2a893e4f4599", new DateTime(2026, 5, 12, 13, 18, 49, 710, DateTimeKind.Utc).AddTicks(9776), "AQAAAAIAAYagAAAAEH7ZVWq/xnUXWOlEjQwRzerapksWqOltjAdGye5RjuWOfC0PSW3LFpuQXDnRekjEgg==", "25e24921-f19b-4224-b709-4b23fd6a3cc0" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 13, 18, 49, 813, DateTimeKind.Utc).AddTicks(6707));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 13, 18, 49, 813, DateTimeKind.Utc).AddTicks(6759));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 13, 18, 49, 813, DateTimeKind.Utc).AddTicks(6763));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 13, 18, 49, 813, DateTimeKind.Utc).AddTicks(6767));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellerSlugName",
                table: "HomePageCards");

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
    }
}
