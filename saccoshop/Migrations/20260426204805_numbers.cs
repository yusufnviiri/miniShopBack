using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class numbers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaximumSellers",
                table: "UserGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumAllowedItems",
                table: "SellerProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumSellers",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "MaximumAllowedItems",
                table: "SellerProfiles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "407d9a81-75cf-4af4-ba3f-dedb68efd478", new DateTime(2026, 4, 21, 22, 18, 22, 791, DateTimeKind.Utc).AddTicks(6103), "AQAAAAIAAYagAAAAEMTvCzrAaOk2ftIY/g26aDTkOY2zXD+DPJOvxZpoZyYDRgYwUJMSrLKqT5ugm+iMyQ==", "3674d057-7e53-4791-9d11-6610249a0d7b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "921de644-9c3e-474e-bbdc-96887dea4d30", new DateTime(2026, 4, 21, 22, 18, 22, 910, DateTimeKind.Utc).AddTicks(5374), "AQAAAAIAAYagAAAAEFQsQ+i797F3in4mtvnQCf9RlTGmAy6PTHiBOcf/a0qB7rILd0h3emfTx6Rx+PoY5Q==", "6317e3c6-fc7b-4505-9ae0-46f93f21b4ad" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "197d2fe7-d685-4256-909a-a132f2db287e", new DateTime(2026, 4, 21, 22, 18, 23, 46, DateTimeKind.Utc).AddTicks(3542), "AQAAAAIAAYagAAAAEKW0PfKPLPGcpVFBND8QQLUnkcOvfE1ozWohfdC4K65K8kDDo8/2kosLYa7T1O4Ejg==", "46a54c69-80f3-48c6-bfb1-af5aa1cc50ba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8f393e92-4dd2-487a-83f7-1de624ad7dd3", new DateTime(2026, 4, 21, 22, 18, 23, 158, DateTimeKind.Utc).AddTicks(1145), "AQAAAAIAAYagAAAAEPeKsJT5SGYKbReJdwquaG82ksA17v3UoR0z/VXkNmS8qfvzlly8uEdJs1Kuneh9kw==", "af971c95-d6ad-45ed-9e68-62c2c68a3a5a" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 22, 18, 23, 258, DateTimeKind.Utc).AddTicks(9919));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 22, 18, 23, 258, DateTimeKind.Utc).AddTicks(9968));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 22, 18, 23, 258, DateTimeKind.Utc).AddTicks(9973));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 22, 18, 23, 258, DateTimeKind.Utc).AddTicks(9977));
        }
    }
}
