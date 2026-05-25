using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class sellernull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e371df13-5994-4a28-9dd9-1056fabba33b", new DateTime(2026, 5, 24, 20, 15, 59, 677, DateTimeKind.Utc).AddTicks(1095), "AQAAAAIAAYagAAAAEK9TiqyX85vvrZRBsvVyZT0nWMGTJpGzp4/GSgY/tNSNAZuBJOoSu1blB0O/9VD2Bg==", "937fa9c0-cb88-4b6c-8c99-a0650c0df4cd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e08c54d7-d4d5-4125-b0bb-4c8b23e5c78c", new DateTime(2026, 5, 24, 20, 15, 59, 785, DateTimeKind.Utc).AddTicks(873), "AQAAAAIAAYagAAAAEGaA0ll/1y3DhMD2cdCdN91MCwf0EXsmNSmGFmH8iSCbEl8ssoV7JZcQ25PINiwU/g==", "5b950abb-2b91-45f3-9de0-22f700ac45cc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af559c48-68c0-46ee-9f41-6e6df2e0afae", new DateTime(2026, 5, 24, 20, 15, 59, 885, DateTimeKind.Utc).AddTicks(3231), "AQAAAAIAAYagAAAAEOXRwkClybz8UASdp3PKDrckaAbXHvoFV1TypRSyG2tNCkDgwLN6wNTnX40AoguNig==", "5a25e664-b72c-40af-a1ff-e7bbdcaeb65b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a0ae3dcb-94e0-4881-b682-ffd04bfd152f", new DateTime(2026, 5, 24, 20, 16, 0, 0, DateTimeKind.Utc).AddTicks(1863), "AQAAAAIAAYagAAAAELT9lC3BjZP4zqX//MG9VMIrjxXtVTn7fNM61vj6oVjpITxOOFp9IRu8ISAGg0ikDw==", "8934e8fd-d276-4029-aa83-a9a9007caed9" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 24, 20, 16, 0, 103, DateTimeKind.Utc).AddTicks(3968));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 24, 20, 16, 0, 103, DateTimeKind.Utc).AddTicks(4014));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 24, 20, 16, 0, 103, DateTimeKind.Utc).AddTicks(4020));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 24, 20, 16, 0, 103, DateTimeKind.Utc).AddTicks(4023));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
