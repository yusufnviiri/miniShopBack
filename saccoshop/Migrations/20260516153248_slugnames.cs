using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class slugnames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "ProductSlugSeq");

            migrationBuilder.CreateSequence(
                name: "SellerProfileSlugSeq");

            migrationBuilder.CreateSequence(
                name: "TradeSlugSeq");

            migrationBuilder.CreateSequence(
                name: "UserGroupSlugSeq");

            migrationBuilder.CreateSequence(
                name: "UserSlugSeq");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0b48378-6606-45ad-93ad-5efd0106ebe7", new DateTime(2026, 5, 16, 15, 32, 46, 818, DateTimeKind.Utc).AddTicks(6253), "AQAAAAIAAYagAAAAELzK9Pwj8IcpNNW6lDVWmbAXO0h3yh2bgXZEJMFdkX/C4rLPzhi8BOn7a7G/fusugQ==", "ca867ccb-1582-4818-92aa-7918836bad3e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "df0690fb-75f8-4dcf-8f1d-7ad9a57428f5", new DateTime(2026, 5, 16, 15, 32, 46, 910, DateTimeKind.Utc).AddTicks(3731), "AQAAAAIAAYagAAAAEHRol9PvgnHDh2TidPJqdI4dcphOpKrMv0bv8TkhE7+ig5xLwXuHQpRlmUKHuO8yfg==", "623c6e3c-0c1b-4735-b389-79f85c5b8683" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d751a512-96c9-40fa-bb6d-68ed5f6bf498", new DateTime(2026, 5, 16, 15, 32, 47, 0, DateTimeKind.Utc).AddTicks(7151), "AQAAAAIAAYagAAAAEBiwQywVk8nftKFur31OWtq08FVn7DfSx6QFpQQqemqOFNljCLKfqVHOdlxYzoG7bg==", "2af12e87-1c2a-4ba1-a675-25ff7061147a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "badb372f-ad4b-40b8-b601-3b5a525bfb27", new DateTime(2026, 5, 16, 15, 32, 47, 96, DateTimeKind.Utc).AddTicks(2234), "AQAAAAIAAYagAAAAEKsIIECBc01nbP07TcYNFq5i10KbcTURD0cBrXcmCXj5jVJQaujnyDOwPnw0HFuDhg==", "81f5b04b-bee4-4429-b4ee-136474b4f71a" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 16, 15, 32, 47, 191, DateTimeKind.Utc).AddTicks(1990));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 16, 15, 32, 47, 191, DateTimeKind.Utc).AddTicks(2029));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 16, 15, 32, 47, 191, DateTimeKind.Utc).AddTicks(2034));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 16, 15, 32, 47, 191, DateTimeKind.Utc).AddTicks(2038));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "ProductSlugSeq");

            migrationBuilder.DropSequence(
                name: "SellerProfileSlugSeq");

            migrationBuilder.DropSequence(
                name: "TradeSlugSeq");

            migrationBuilder.DropSequence(
                name: "UserGroupSlugSeq");

            migrationBuilder.DropSequence(
                name: "UserSlugSeq");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "47e4fc1f-397c-4c46-ba5b-dee34aa0183d", new DateTime(2026, 5, 12, 13, 18, 49, 395, DateTimeKind.Utc).AddTicks(9996), "AQAAAAIAAYagAAAAEGAjliLRjUZJNp8F894W3iIlih3HDMUNxABvgLkSW8xIKBPtr3NNC9o9slx0cXmUQQ==", "430e3f87-afe6-4cce-987e-28f7b3950b8e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c37416f8-b56c-47d2-9f18-0e73baef02ae", new DateTime(2026, 5, 12, 13, 18, 49, 497, DateTimeKind.Utc).AddTicks(7387), "AQAAAAIAAYagAAAAEKOD2x9ps1o/7XmBWRKLkon66t1dsWeQhjJTGK3FerT1wuiqqoJp7RCGKpC2mRrCZw==", "8fb039c5-ef8b-415e-bf69-4d88f886d1cd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cd2bd50a-1549-4589-b232-0dd513a14a3d", new DateTime(2026, 5, 12, 13, 18, 49, 611, DateTimeKind.Utc).AddTicks(5204), "AQAAAAIAAYagAAAAEHtXXlggDpDkeOOlCzjBkBLfCBrJgTgFNxHvLXto8ogMFhNEZLcSnhoa5PgaZIGxZw==", "1c877195-839b-4664-b572-fef69d58b143" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4be12e79-3bf2-4943-8f58-2a893e4f4599", new DateTime(2026, 5, 12, 13, 18, 49, 710, DateTimeKind.Utc).AddTicks(9776), "AQAAAAIAAYagAAAAEH7ZVWq/xnUXWOlEjQwRzerapksWqOltjAdGye5RjuWOfC0PSW3LFpuQXDnRekjEgg==", "25e24921-f19b-4224-b709-4b23fd6a3cc0" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 13, 18, 49, 813, DateTimeKind.Utc).AddTicks(6707));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 13, 18, 49, 813, DateTimeKind.Utc).AddTicks(6759));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 13, 18, 49, 813, DateTimeKind.Utc).AddTicks(6763));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 13, 18, 49, 813, DateTimeKind.Utc).AddTicks(6767));
        }
    }
}
