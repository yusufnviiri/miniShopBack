using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class globalseler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsGlobalSeller",
                table: "SellerProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsGroupSeller",
                table: "SellerProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f5e81723-c308-4f34-bb64-5b2b37bd9764", new DateTime(2026, 5, 24, 7, 34, 58, 957, DateTimeKind.Utc).AddTicks(899), "AQAAAAIAAYagAAAAELsGTJDwktJG5i6zsgTbiIwpHTK1h2MYNt9/9rAagcM8hfxm9b7C97ZC3hmZ3Lay+w==", "40b30064-8093-40de-a65f-98d35e275483" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1478c22f-08d9-4e89-9d0e-ee751d6d960f", new DateTime(2026, 5, 24, 7, 34, 59, 117, DateTimeKind.Utc).AddTicks(1266), "AQAAAAIAAYagAAAAELRpUNRtuOu1cvqtEP6t5JAMDfzVMaVhBPUnI23tDvtFxol6AxiTJt1tH7nhYP4sKQ==", "045b32ee-c09d-4405-ac42-5fdcaa8d8656" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab78571b-b94a-48c7-92cd-507e8f49aeba", new DateTime(2026, 5, 24, 7, 34, 59, 237, DateTimeKind.Utc).AddTicks(588), "AQAAAAIAAYagAAAAEPbP8FToE8DDgORVbwh8/7lgZJjfkFb2pm7sbZZ6V2T1x3bELa7WxXSpp+zIYAbeNA==", "d364ec57-b2c6-4874-95c8-e60fc5dbeb25" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a772007-e664-40d0-9b65-7cc349c8f241", new DateTime(2026, 5, 24, 7, 34, 59, 325, DateTimeKind.Utc).AddTicks(6021), "AQAAAAIAAYagAAAAEHNwVZffN2rO5ZyAZcj5p160j9OeiyuT0SCdLhGX2JeQqjd9iD49b9oG6cv1N3IJ0g==", "54a796c9-e48a-462b-a2d1-282737b9380d" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 24, 7, 34, 59, 427, DateTimeKind.Utc).AddTicks(8864));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 24, 7, 34, 59, 427, DateTimeKind.Utc).AddTicks(8886));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 24, 7, 34, 59, 427, DateTimeKind.Utc).AddTicks(9017));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 24, 7, 34, 59, 427, DateTimeKind.Utc).AddTicks(9021));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGlobalSeller",
                table: "SellerProfiles");

            migrationBuilder.DropColumn(
                name: "IsGroupSeller",
                table: "SellerProfiles");

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
    }
}
