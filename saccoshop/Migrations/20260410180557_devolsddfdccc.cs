using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class devolsddfdccc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ae2da493-14b5-480d-ba91-362d72177e6c", new DateTime(2026, 4, 10, 18, 5, 55, 423, DateTimeKind.Utc).AddTicks(3961), "AQAAAAIAAYagAAAAELgumlGXaDReAuH8hm1wpCZFSPnjridblReyOGNhmrexSNJ1qDDIkv93ttIP7aD/NQ==", "4b88a2e4-4dd8-475a-97cb-381512fdb483" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "05201254-4bc1-4fed-bf80-add500b14ded", new DateTime(2026, 4, 10, 18, 5, 55, 513, DateTimeKind.Utc).AddTicks(6353), "AQAAAAIAAYagAAAAEM/v23JLbiuitWGNNX1TR/Kh1ONjGl3iU0C8ROmiEVq5b60u8DHgKsDailT2h5zHuw==", "dfc14fcf-80f8-405a-ab1c-3d2739917cfd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "193c9d16-de07-4260-840a-0f33d1cea41d", new DateTime(2026, 4, 10, 18, 5, 55, 607, DateTimeKind.Utc).AddTicks(154), "AQAAAAIAAYagAAAAEP0z/ckPScRen8izHMNei3VmYlbLgir4AhKfVf9kAAauD4edgjARB3U722pVMtuLpw==", "83f0211b-3d1d-4a5e-a2ac-886fe880c797" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a2851c5-0f8e-4ff1-b32a-53fae5fe2171", new DateTime(2026, 4, 10, 18, 5, 55, 703, DateTimeKind.Utc).AddTicks(4455), "AQAAAAIAAYagAAAAEIR7Mbibi3Jp+Rym9PowgF/viKIECMXg5Tv864jUO0urzV+kYqhhBS6RHU+qeUpowQ==", "b6b6d968-4fc8-4ff9-a5c2-7ebeb799496d" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 18, 5, 55, 797, DateTimeKind.Utc).AddTicks(9327));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 18, 5, 55, 797, DateTimeKind.Utc).AddTicks(9345));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 18, 5, 55, 797, DateTimeKind.Utc).AddTicks(9349));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 18, 5, 55, 797, DateTimeKind.Utc).AddTicks(9353));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "08a8be89-83c7-45f5-9f98-d398db8bc092", new DateTime(2026, 4, 10, 18, 4, 42, 742, DateTimeKind.Utc).AddTicks(624), "AQAAAAIAAYagAAAAEIDCRsUgq+1vYbALUIVL0qjO4/36WupH85qqCftbrBCFEoz9iNk+xyIg1ZQoDr4n3w==", "58bd7290-90c1-4e28-ab66-fafac0e94e97" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5398fe5d-a14d-4ac2-82cc-c5b5d4b02e6c", new DateTime(2026, 4, 10, 18, 4, 42, 847, DateTimeKind.Utc).AddTicks(8156), "AQAAAAIAAYagAAAAEKqBD1Ir4OFde3wRnGA5SYBYsuZBSQknVbXIQphjCP4Y1rO2Oy4M3Qchrvwtqelolw==", "a9649d05-25e8-4eb4-8fb2-292740b18c8e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3aeb87a1-36bf-4cc2-b694-d61534913446", new DateTime(2026, 4, 10, 18, 4, 42, 948, DateTimeKind.Utc).AddTicks(7808), "AQAAAAIAAYagAAAAEBB+hYLGcIVEU7Y55GgmF+scU4lcYDh8jbJKJvGsWc6dj46h6twuwAF371TG9f+Wqw==", "8b158bcf-a3a4-4a28-9dfb-377d3e99c5fc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "557ad818-f2bc-46b6-b8ab-8dec43fe8d4d", new DateTime(2026, 4, 10, 18, 4, 43, 47, DateTimeKind.Utc).AddTicks(3679), "AQAAAAIAAYagAAAAEKcHH6qCgq8drx+vM2ZQvxAnya6lFvWiISwbSw4HiwXSZI5TIdnycilJ98o0uEFndw==", "71150bbe-3325-415c-9127-11b028e00542" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 18, 4, 43, 149, DateTimeKind.Utc).AddTicks(8287));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 18, 4, 43, 149, DateTimeKind.Utc).AddTicks(8306));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 18, 4, 43, 149, DateTimeKind.Utc).AddTicks(8310));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 18, 4, 43, 149, DateTimeKind.Utc).AddTicks(8313));
        }
    }
}
