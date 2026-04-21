using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class whatsappnumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WhatsAppNumber",
                table: "SellerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba6410ee-f3dd-496f-8c2a-ad4dc28e7316", new DateTime(2026, 4, 18, 9, 2, 58, 786, DateTimeKind.Utc).AddTicks(9133), "AQAAAAIAAYagAAAAEByXRN+q3AnMNtDMFs2RbJW6UOeEODTHin3s+0JJxW7AnvMRpF4acPs3Z12CC76jUA==", "6024ba32-af08-4044-8fb5-03ae7fe9c3a2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "34f04917-44f1-4fa5-979a-fee8797d393c", new DateTime(2026, 4, 18, 9, 2, 58, 888, DateTimeKind.Utc).AddTicks(1506), "AQAAAAIAAYagAAAAEHiyjd5z9ez2kvnHG0FsmKdD0wZKHEcxZ6i+Sor8kxZgCMiVU3+JqWNSpRRtp20G6w==", "39f851a5-f402-48e0-9cea-c3ecb36031e8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c75bc762-98db-490d-9ce0-9dbc53cb7b07", new DateTime(2026, 4, 18, 9, 2, 58, 987, DateTimeKind.Utc).AddTicks(2455), "AQAAAAIAAYagAAAAEDMP8YdoSczyDZr7T4XimeAP0nlR2xhIlap00ZDml+BXJT8wMiCKFtrSvURrfzwLuw==", "e5973863-4c71-4bf8-bfb8-cb694d1f1491" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5168878-f05d-43ed-843e-5bc9728cf4fe", new DateTime(2026, 4, 18, 9, 2, 59, 87, DateTimeKind.Utc).AddTicks(150), "AQAAAAIAAYagAAAAEDqiP0gCyKzHxAxDIgvnIiJl9f0cvOhSpJj43uVGh/jBoVg1o4RM9GVkV+76taUgHQ==", "ac819388-7023-4cce-a30f-77fa5c7dcc33" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 18, 9, 2, 59, 187, DateTimeKind.Utc).AddTicks(8789));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 18, 9, 2, 59, 187, DateTimeKind.Utc).AddTicks(8829));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 18, 9, 2, 59, 187, DateTimeKind.Utc).AddTicks(8833));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 18, 9, 2, 59, 187, DateTimeKind.Utc).AddTicks(8837));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhatsAppNumber",
                table: "SellerProfiles");

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
    }
}
