using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class devolsddfd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1144f158-60cc-4840-88ef-a47996e24fba", new DateTime(2026, 4, 10, 16, 8, 57, 69, DateTimeKind.Utc).AddTicks(7732), "AQAAAAIAAYagAAAAEH/RGsNevCela3KfrH/APcYRnJnO/vNIXg1M8rhPYN8JWueTLXyD5ciOhCqnKPfxIg==", "2497c5f1-5bca-4803-ab67-cdf0d81cc856" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01e38b33-08eb-4483-a8af-db9236f0d9b4", new DateTime(2026, 4, 10, 16, 8, 57, 177, DateTimeKind.Utc).AddTicks(560), "AQAAAAIAAYagAAAAEOX5JqdK+IjHYB805VupuxF2Yu6sKPE/IgXL5mCV3aExwgAnCMO8EHb/giqq+hRrcA==", "fcd1cd3a-bac2-4f67-ad07-a932407fd17f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e9c23fe-df64-4f59-ae8b-66ef230346d7", new DateTime(2026, 4, 10, 16, 8, 57, 292, DateTimeKind.Utc).AddTicks(2230), "AQAAAAIAAYagAAAAEMcSuJcRTH5/Wvlh8clWMD+OV1JExJToqxPagi9EMiyzxd+l2yJF3FDTm4oQPvp/sQ==", "e907b8b3-3dec-4653-9636-cf83c537bf97" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "97c44638-0368-4fea-9aa3-dc83c8f87e70", new DateTime(2026, 4, 10, 16, 8, 57, 404, DateTimeKind.Utc).AddTicks(2980), "AQAAAAIAAYagAAAAEB2TAWXli4XRqs5TXM6JYPKqib55qEqs6p4mf3iy2Yo+rFb2tjKG+8CzG0EFZD3U6g==", "08f5a873-4ef3-46cb-bc54-5d9ec20e674b" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 16, 8, 57, 516, DateTimeKind.Utc).AddTicks(7796));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 16, 8, 57, 516, DateTimeKind.Utc).AddTicks(7814));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 16, 8, 57, 516, DateTimeKind.Utc).AddTicks(7817));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 16, 8, 57, 516, DateTimeKind.Utc).AddTicks(7823));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b57809fb-6426-485c-b7e7-b56c88ef1b7c", new DateTime(2026, 4, 10, 16, 2, 31, 26, DateTimeKind.Utc).AddTicks(9317), "AQAAAAIAAYagAAAAEKtm3vhtg5TLzqhimSQ2KrMbdeJUHV/E3SBogl9ev7X/hwfdrWW59QpozoFW0bXoxg==", "70e33ef2-eac1-409b-9aab-992b1d0d1f92" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78a2c8ad-acb4-44a1-b20f-ac92d1809107", new DateTime(2026, 4, 10, 16, 2, 31, 123, DateTimeKind.Utc).AddTicks(5699), "AQAAAAIAAYagAAAAEDIIdK8NbxR2eFi2zquxkT6Us/iNxW28Pv9InuQ9SJgY+loORyEDSpVi0aUNdqeXsg==", "6201849a-6683-427f-855d-4af06c4ad002" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "65359803-b632-450a-ace0-cbee6993073b", new DateTime(2026, 4, 10, 16, 2, 31, 226, DateTimeKind.Utc).AddTicks(2615), "AQAAAAIAAYagAAAAEIwtbvUPvMucOKEs48JEFW5Xi9Jo0Rm1qp3MQS08aZ7d4YB9qGrywDwh+XDX2OTtYw==", "c8b3a495-bbc4-4e67-bf38-72c4d736a423" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "923298e1-4e13-4c34-a604-25a7683bf45d", new DateTime(2026, 4, 10, 16, 2, 31, 327, DateTimeKind.Utc).AddTicks(4778), "AQAAAAIAAYagAAAAELC5Zzk2yF9pkV/CKq3H5iWzkDzSbyiDhbYNOjE9QDGyW7qtjBSVR7QtZ/AZpMGWaw==", "ce8563f7-6e16-4241-a9e5-24d8a296ab72" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 16, 2, 31, 433, DateTimeKind.Utc).AddTicks(3154));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 16, 2, 31, 433, DateTimeKind.Utc).AddTicks(3174));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 16, 2, 31, 433, DateTimeKind.Utc).AddTicks(3178));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 16, 2, 31, 433, DateTimeKind.Utc).AddTicks(3180));
        }
    }
}
