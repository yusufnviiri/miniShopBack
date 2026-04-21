using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class isrow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRow",
                table: "HomePageCards",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0119502-bcd8-46cc-8ed9-7c9c5593e5a7", new DateTime(2026, 4, 17, 21, 9, 45, 179, DateTimeKind.Utc).AddTicks(7554), "AQAAAAIAAYagAAAAEEbl9EoXXdXT8IBXDJ1vDjmGzXEml8x2+u72LhiHqJ7ZyH3SIJlPw+rwTZA1PmrdUg==", "78c23413-413d-4f41-b46c-b371aa4b6508" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07ae5a95-8786-4ae3-a7e3-40461486e9df", new DateTime(2026, 4, 17, 21, 9, 45, 280, DateTimeKind.Utc).AddTicks(3603), "AQAAAAIAAYagAAAAEJX4BnvN2ZBznJVIXg/uBPlwc82EGwKZ/EhQgsWq6OVFQYOUWhEirHd4vrL2Df/sPw==", "597250ed-baa8-4d6d-916b-1a8b1e430817" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f7b2bc2-bb35-4fff-ac13-3f99d43adb48", new DateTime(2026, 4, 17, 21, 9, 45, 380, DateTimeKind.Utc).AddTicks(8519), "AQAAAAIAAYagAAAAEDQrpSOMRA6C6S5Yhwx16Np4jKNUsjsKh0pSMWQjnBCr1pSTAQjY9N604dwBfREVjw==", "4f2a17e2-507b-4e04-913b-1a29a3e92a8d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7a1b57c-ccca-46c4-965e-084bc4d2e9e7", new DateTime(2026, 4, 17, 21, 9, 45, 479, DateTimeKind.Utc).AddTicks(5978), "AQAAAAIAAYagAAAAEJ9jW+diipsm3ggGU2gfs42f47sOQ9dns09p427uB4j1OqJnIje5TlVrqwAt2Qcq/g==", "672bacf6-58db-48d3-b853-1fa772565221" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 21, 9, 45, 578, DateTimeKind.Utc).AddTicks(7654));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 21, 9, 45, 578, DateTimeKind.Utc).AddTicks(7697));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 21, 9, 45, 578, DateTimeKind.Utc).AddTicks(7701));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 21, 9, 45, 578, DateTimeKind.Utc).AddTicks(7857));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRow",
                table: "HomePageCards");

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
        }
    }
}
