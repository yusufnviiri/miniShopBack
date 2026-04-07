using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class work : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e0f5fe7-83a2-47c1-bc24-94735148462c", new DateTime(2026, 4, 5, 21, 22, 11, 301, DateTimeKind.Utc).AddTicks(5176), "AQAAAAIAAYagAAAAENSQ3E677/RfJbgZnG1ATCL1lXBxWiJP1EULKLuWO/m46b1utPCn9YhmHGIea2URLw==", "dce154ab-6560-4350-a979-a5b82fa76db5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "824518e9-590e-480f-9ef2-437478fcd1cf", new DateTime(2026, 4, 5, 21, 22, 11, 408, DateTimeKind.Utc).AddTicks(4344), "AQAAAAIAAYagAAAAEP7IZs4m66fhDIzmpvD8dA7fgHhBu0NXjHrAB9uIa47t8xdN0hhWd3quD0H3gK8lAg==", "e6975806-9b26-419e-829e-6f974d87157d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b36e0494-cf0a-4630-bd30-472df656e986", new DateTime(2026, 4, 5, 21, 22, 11, 510, DateTimeKind.Utc).AddTicks(5717), "AQAAAAIAAYagAAAAEJXQtD6kkNravg4EuRktTFQRLchRprIbB8jvCVdUoZXaPj5bh8ACA0dEKbqeGSC58g==", "859deb1f-deae-477e-9b98-09ef04561117" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4d6c4fd-51c7-4346-8e57-54e27f1e05f2", new DateTime(2026, 4, 5, 21, 22, 11, 604, DateTimeKind.Utc).AddTicks(3531), "AQAAAAIAAYagAAAAEPS9nJysEBFSjduKV1aqTKYNvHYk0WPKL4DEktCpEXOA3XKrD/kQAHBc8W8R0Z8R9Q==", "66fce29d-c410-4e7c-ae8a-0df60710a9b6" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                columns: new[] { "CreatedAt", "IdentityUserId" },
                values: new object[] { new DateTime(2026, 4, 5, 21, 22, 11, 697, DateTimeKind.Utc).AddTicks(4104), "42cd3af3-f319-4118-a604-4442d487b923" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                columns: new[] { "CreatedAt", "IdentityUserId" },
                values: new object[] { new DateTime(2026, 4, 5, 21, 22, 11, 697, DateTimeKind.Utc).AddTicks(4123), "5f2b8a40-d899-4345-aa3e-7b98712bc112" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                columns: new[] { "CreatedAt", "IdentityUserId" },
                values: new object[] { new DateTime(2026, 4, 5, 21, 22, 11, 697, DateTimeKind.Utc).AddTicks(4128), "bd363936-63d0-4ace-bc46-a2f1348cb61e" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 5, 21, 22, 11, 697, DateTimeKind.Utc).AddTicks(4131));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "84f4f3bd-9e5a-44e0-9d90-c2e667527bc7", new DateTime(2026, 4, 5, 21, 16, 18, 746, DateTimeKind.Utc).AddTicks(9530), "AQAAAAIAAYagAAAAED5hzA6yrPDFGcJPar2/UCdiN+oQOrl7HHG2xTauS5ou+jDTufamqM83J3JBIXQM8A==", "e149b2cf-7e7c-42c6-bbbb-7383cdb677aa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f206dce9-21b3-4f27-b851-31f159dddb36", new DateTime(2026, 4, 5, 21, 16, 18, 837, DateTimeKind.Utc).AddTicks(8835), "AQAAAAIAAYagAAAAEEOrUTUQHPx7t4btHzSNoE4pufY945J48rGbm/DVyG7SgrlX/fA9buR8iEQ3ikVYqA==", "112cc67d-03ad-424d-887f-d927d35020f4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "596dfe75-3ce8-4dea-831e-576b7ea10770", new DateTime(2026, 4, 5, 21, 16, 18, 932, DateTimeKind.Utc).AddTicks(6557), "AQAAAAIAAYagAAAAEC2ZTZvArUEZJgJA3CAVS8ZJ69pi4d3PHDOWSJA5XK2AMoi0e04SKd1qmbGdsOJjRw==", "846faf10-16e7-4441-a25f-353b82c48a14" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20bb1802-46e8-4e5f-bc8f-a56e7c528ee7", new DateTime(2026, 4, 5, 21, 16, 19, 25, DateTimeKind.Utc).AddTicks(9978), null, "eb2e378b-b88d-4850-b7ae-c0eece767da2" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                columns: new[] { "CreatedAt", "IdentityUserId" },
                values: new object[] { new DateTime(2026, 4, 5, 21, 16, 19, 120, DateTimeKind.Utc).AddTicks(8875), "5f2b8a40-d899-4345-aa3e-7b98712bc112" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                columns: new[] { "CreatedAt", "IdentityUserId" },
                values: new object[] { new DateTime(2026, 4, 5, 21, 16, 19, 120, DateTimeKind.Utc).AddTicks(8878), "bd363936-63d0-4ace-bc46-a2f1348cb61e" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                columns: new[] { "CreatedAt", "IdentityUserId" },
                values: new object[] { new DateTime(2026, 4, 5, 21, 16, 19, 120, DateTimeKind.Utc).AddTicks(8846), "42cd3af3-f319-4118-a604-4442d487b923" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 5, 21, 16, 19, 120, DateTimeKind.Utc).AddTicks(8881));
        }
    }
}
