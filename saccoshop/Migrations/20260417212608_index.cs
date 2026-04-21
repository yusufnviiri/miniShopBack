using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "HomePageCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "02157f10-4fe7-4b63-a2ef-9663d819f093", new DateTime(2026, 4, 17, 21, 26, 6, 909, DateTimeKind.Utc).AddTicks(6748), "AQAAAAIAAYagAAAAECDS4c5X4C7OCsc0Te43livmRXqihIQdSCl8QliII9p8Nuyv4W1ZfMg4BN0/vK5prA==", "170e5b06-da64-4cf8-adf2-3acd7d5eba1b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "08339926-1ee8-4a7a-a1b8-83ffb4beb164", new DateTime(2026, 4, 17, 21, 26, 7, 0, DateTimeKind.Utc).AddTicks(3286), "AQAAAAIAAYagAAAAEEoBaC6dVanTirq0KndWv6jYpZMRY+QVfhv3Gx5fSBGgdfKOKDCMWH2XXBzMeLl0Fg==", "503d65e7-5e87-4f15-9ae3-cc50c1274ab9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f20fa8bc-0104-495d-898f-b28dbabe6c57", new DateTime(2026, 4, 17, 21, 26, 7, 94, DateTimeKind.Utc).AddTicks(156), "AQAAAAIAAYagAAAAEMBcfNptIofsuKx2cv9s0WGzaPKXBY9GgphgzT82xDrQ/uRLTps3vLVHkoAGtjH4fg==", "0cb9bf45-568c-49eb-9faf-76cab59dcec1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "247e382a-9abf-4999-9be3-431072b0df40", new DateTime(2026, 4, 17, 21, 26, 7, 189, DateTimeKind.Utc).AddTicks(9797), "AQAAAAIAAYagAAAAEOrW7Lb+Bk7k9og5aV3hCKrHYUzD6Js58nzqTfJP6UK598vgYBzngy3c+sR6D7Qt4Q==", "bca04bd3-26a0-4da8-bb3b-6c16f55a5015" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 21, 26, 7, 283, DateTimeKind.Utc).AddTicks(6498));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 21, 26, 7, 283, DateTimeKind.Utc).AddTicks(6676));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 21, 26, 7, 283, DateTimeKind.Utc).AddTicks(6682));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 21, 26, 7, 283, DateTimeKind.Utc).AddTicks(6690));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "HomePageCards");

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
    }
}
