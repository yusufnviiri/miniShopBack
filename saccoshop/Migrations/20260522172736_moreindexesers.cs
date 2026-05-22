using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class moreindexesers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0f96893-7e2a-46fa-8432-c2dbd73c55c6", new DateTime(2026, 5, 22, 17, 27, 32, 197, DateTimeKind.Utc).AddTicks(5146), "AQAAAAIAAYagAAAAELzqN8XLcNlza2orMLD35SXw96v1YOFIPcSoNlTX36t7Ufynchnq0pWKiFPSkjd5kw==", "52de8d89-fd9b-41fe-a9cb-387909e733c2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "77d5a018-98fb-4eb8-ab11-3875925a92e4", new DateTime(2026, 5, 22, 17, 27, 32, 332, DateTimeKind.Utc).AddTicks(6226), "AQAAAAIAAYagAAAAEF6o+ivbOFD/z1f9/ze3IrVPiMI4VrGO1Evn930paGvW0MqOOb4/UAJBK1jnoFVRWQ==", "7306b72a-0315-4136-88ab-311785f0c67c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "23b53658-2b23-49ba-8111-f34d82877b9b", new DateTime(2026, 5, 22, 17, 27, 32, 541, DateTimeKind.Utc).AddTicks(7449), "AQAAAAIAAYagAAAAEAUFWT6D3zRr4kuByqRbwV8q9rIpBupMxypd/aMbycFy8TWCI0bbrtdPv5gLrOusuA==", "0ddfd56a-01b4-446a-8694-9708235d3bd1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7423f1d-53f5-4bcf-9d3a-daebb089d48b", new DateTime(2026, 5, 22, 17, 27, 32, 730, DateTimeKind.Utc).AddTicks(6319), "AQAAAAIAAYagAAAAEEMqMeohvZwWw5GaifvGGuiSELV+4mbWpZH09lU/ND6i8Ep5fInIRWSBHoHklJmgfA==", "eb5e18b9-3eca-44db-888d-5aa2db1bdfbd" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 22, 17, 27, 32, 875, DateTimeKind.Utc).AddTicks(7893));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 22, 17, 27, 32, 875, DateTimeKind.Utc).AddTicks(7914));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 22, 17, 27, 32, 875, DateTimeKind.Utc).AddTicks(7918));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 22, 17, 27, 32, 875, DateTimeKind.Utc).AddTicks(7921));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8d8324a0-1612-470d-8b35-b50358c4f753", new DateTime(2026, 5, 22, 17, 25, 48, 461, DateTimeKind.Utc).AddTicks(5142), "AQAAAAIAAYagAAAAEErJ2iZy8zqTYzGKXV3in/xrYkrULL9J8LQ+A3FsfEhzM6Z87lfEN1XDSKYAJpipGQ==", "5fc71a71-bf39-4498-b1eb-6e70461db973" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71b44629-84e3-4d4a-87c2-7d2ca847ea09", new DateTime(2026, 5, 22, 17, 25, 48, 552, DateTimeKind.Utc).AddTicks(7995), "AQAAAAIAAYagAAAAEG7dXYO9ZuuTRc1/fBCY/MYPH/WZgy+4Y2NmjUrVk0cDSyH0qnAblzcwp2XidYxqgQ==", "dc686ec7-9ecb-4070-863d-7623f2ac92bd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6406082e-d41c-4fc5-a099-9a0e63f2d62f", new DateTime(2026, 5, 22, 17, 25, 48, 642, DateTimeKind.Utc).AddTicks(3546), "AQAAAAIAAYagAAAAEElrAgyZqx81b7DNecwz5jh9KknHqaB+NQc3x5Ul+0y6JDdZiNBbqY+tCat74oLfgQ==", "98d9a56d-7c99-4d0f-9bc4-767516f88c88" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "604f58c8-5e12-4986-9303-09c05c06b38f", new DateTime(2026, 5, 22, 17, 25, 48, 732, DateTimeKind.Utc).AddTicks(3152), "AQAAAAIAAYagAAAAEI4/4oZpUh7JqWy9RcejDpN918UFDR8vNELcg1xXgMjmickH6DNkglz8rojdecjntw==", "1b89b0b0-85a3-4d24-86ef-8ee47c62b8b7" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 22, 17, 25, 48, 864, DateTimeKind.Utc).AddTicks(360));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 22, 17, 25, 48, 864, DateTimeKind.Utc).AddTicks(408));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 22, 17, 25, 48, 864, DateTimeKind.Utc).AddTicks(418));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 22, 17, 25, 48, 864, DateTimeKind.Utc).AddTicks(425));
        }
    }
}
