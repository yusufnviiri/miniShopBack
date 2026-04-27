using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class condition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Condition",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condition",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3298d533-8fc5-4d9b-b730-7ee0bd83cd75", new DateTime(2026, 4, 26, 20, 48, 3, 789, DateTimeKind.Utc).AddTicks(4560), "AQAAAAIAAYagAAAAEO9+znGdAMy0HvK4p0GpcuIJ2u9y4rLiUZsaGweUTSZGnCkGfCCWHOTdz9/e4FAf2Q==", "18ab914f-bd21-4b1c-a4fe-6daa73548ff2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9f0f5c1c-ca14-49dc-9565-f9cf928450ce", new DateTime(2026, 4, 26, 20, 48, 3, 890, DateTimeKind.Utc).AddTicks(2217), "AQAAAAIAAYagAAAAEIPGCPZq3eLAi6/OLU2yKyYMZnLRUUjHlRZfJ8+jCfIFKSvP7kZNdqMv7aHd3Xo5ig==", "8d5c7281-2157-4540-be8f-819164f5b486" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5405f227-c282-43b9-ba17-eb46d2cead82", new DateTime(2026, 4, 26, 20, 48, 3, 988, DateTimeKind.Utc).AddTicks(9273), "AQAAAAIAAYagAAAAEGKOwfWnZq6yWErLsQ4d2MAEMjXBqBvODzl2yvzshJkcUcPlrST2/anGL8Ssv3y8Sw==", "ed59fabd-3ed9-4949-a66a-5c17b85b057f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4efe044c-c2a2-4d16-8f4f-5022645f9432", new DateTime(2026, 4, 26, 20, 48, 4, 87, DateTimeKind.Utc).AddTicks(6981), "AQAAAAIAAYagAAAAEJmWNnzgjZGaE5qlKNonu0UF6lTWvV8RjayVaFxmYZ7ODoLWnkxx9/fWmKScp99ccw==", "5b6324c1-d5ec-4616-9477-32fd4ad9d14f" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 26, 20, 48, 4, 185, DateTimeKind.Utc).AddTicks(9410));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 26, 20, 48, 4, 185, DateTimeKind.Utc).AddTicks(9453));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 26, 20, 48, 4, 185, DateTimeKind.Utc).AddTicks(9457));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 26, 20, 48, 4, 185, DateTimeKind.Utc).AddTicks(9460));
        }
    }
}
