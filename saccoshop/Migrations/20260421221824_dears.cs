using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class dears : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductImpressions",
                columns: table => new
                {
                    ProductImpressionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImpressions", x => x.ProductImpressionId);
                    table.ForeignKey(
                        name: "FK_ProductImpressions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductImpressions_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "UserProfileId");
                });

            migrationBuilder.CreateTable(
                name: "TradeImpressions",
                columns: table => new
                {
                    TradeImpressionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeImpressions", x => x.TradeImpressionId);
                    table.ForeignKey(
                        name: "FK_TradeImpressions_Trades_TradeId",
                        column: x => x.TradeId,
                        principalTable: "Trades",
                        principalColumn: "TradeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TradeImpressions_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "UserProfileId");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductImpressions_ProductId",
                table: "ProductImpressions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImpressions_UserProfileId",
                table: "ProductImpressions",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeImpressions_TradeId",
                table: "TradeImpressions",
                column: "TradeId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeImpressions_UserProfileId",
                table: "TradeImpressions",
                column: "UserProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImpressions");

            migrationBuilder.DropTable(
                name: "TradeImpressions");

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
        }
    }
}
