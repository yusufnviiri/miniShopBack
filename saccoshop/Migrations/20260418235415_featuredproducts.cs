using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class featuredproducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupFeaturedProducts",
                columns: table => new
                {
                    GroupFeaturedProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupFeaturedProducts", x => x.GroupFeaturedProductId);
                    table.ForeignKey(
                        name: "FK_GroupFeaturedProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupFeaturedProducts_UserGroups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroups",
                        principalColumn: "UserGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "61355150-0887-456d-8b1c-a66e39bf41ae", new DateTime(2026, 4, 18, 23, 54, 12, 850, DateTimeKind.Utc).AddTicks(285), "AQAAAAIAAYagAAAAEJPP9a9mZoi42BLgd5BV3keN+ihkZdSW0diSdhc4nX3DAjY9sV8TQqIPUOLEDotEcA==", "cbc9f8e3-474d-4139-9e04-6c3427d03050" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4633dc9e-c8e8-4b63-bf7b-331124d3c9c9", new DateTime(2026, 4, 18, 23, 54, 12, 944, DateTimeKind.Utc).AddTicks(9714), "AQAAAAIAAYagAAAAEAcg280SLBPCiAzYZMMx3xUYGGRNteTj+i8MwAP9Z0Evtgni4XTeH7kqmKYlZlOoMA==", "d67d8ab6-f652-42d2-9f98-c9d813c72d2f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2ce360be-6e10-4630-9650-7349c8602cb4", new DateTime(2026, 4, 18, 23, 54, 13, 59, DateTimeKind.Utc).AddTicks(4334), "AQAAAAIAAYagAAAAED7HVobgztM6geWNaM+rWbw74xN+cHU98NTWl56svsEHjn7fLSh6rdILEtVDLqJQxg==", "53b71f52-b483-48eb-89f1-7511c8574780" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3224fa9f-0b44-43f1-b614-02bee79979d3", new DateTime(2026, 4, 18, 23, 54, 13, 177, DateTimeKind.Utc).AddTicks(4423), "AQAAAAIAAYagAAAAEGEMA2SSpXeyhP73VSd8jWQ8Mgbb9c2yDywf/pjXGLgkNSL+EJkuDW5PqHZnNjXUvQ==", "c986a224-609f-444f-8542-e7c54dd02585" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 18, 23, 54, 13, 281, DateTimeKind.Utc).AddTicks(7840));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 18, 23, 54, 13, 281, DateTimeKind.Utc).AddTicks(7884));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 18, 23, 54, 13, 281, DateTimeKind.Utc).AddTicks(7889));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 18, 23, 54, 13, 281, DateTimeKind.Utc).AddTicks(7893));

            migrationBuilder.CreateIndex(
                name: "IX_GroupFeaturedProducts_ProductId",
                table: "GroupFeaturedProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupFeaturedProducts_UserGroupId",
                table: "GroupFeaturedProducts",
                column: "UserGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupFeaturedProducts");

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
    }
}
