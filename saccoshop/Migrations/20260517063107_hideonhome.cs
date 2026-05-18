using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class hideonhome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HiddenOnHome",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d12487d8-0391-4e0b-b3e4-f45264411a6e", new DateTime(2026, 5, 17, 6, 31, 5, 822, DateTimeKind.Utc).AddTicks(1860), "AQAAAAIAAYagAAAAEAAcRv3o6qgjtmqOzeikDrjTpXRhjBi1djsi1bj51sk1wK446xlakj3pUGbihEHJDg==", "b7034fd3-c7f9-42d2-b842-43a0df205717" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0415aa84-7c77-408f-a8a0-1e9b2960651a", new DateTime(2026, 5, 17, 6, 31, 5, 925, DateTimeKind.Utc).AddTicks(3691), "AQAAAAIAAYagAAAAEOsKq/LEbH9mxxEwGELAjTfHR6HEx6nJAIx4DGmaBjSBISW+OInWxwupwmNwpcw9Ag==", "cfcd88b9-ced5-4d28-81a2-baaa134bd922" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3558294-a591-42b8-bc7c-ca6e9f1e81fd", new DateTime(2026, 5, 17, 6, 31, 6, 35, DateTimeKind.Utc).AddTicks(1203), "AQAAAAIAAYagAAAAEF78WzCk8CwcVY+f3b9h1r1yUtHBBwe7LWc+dD7jPeL80p/I8hedEsWQBsIpp3+oeg==", "bbd74c2e-38f9-46b1-9a12-ed7a372151f4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "feade83b-fa02-43a5-b9bf-f5425bb57be2", new DateTime(2026, 5, 17, 6, 31, 6, 141, DateTimeKind.Utc).AddTicks(6904), "AQAAAAIAAYagAAAAEOLjUeDOgVHt9wkWET4YFdKeool1BdoWKdOMbJuhWISXGL3F8YUCUgqvlzMVzqwyPQ==", "e59e2656-00d5-418d-a3b9-2bbc992208f9" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 17, 6, 31, 6, 243, DateTimeKind.Utc).AddTicks(6617));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 17, 6, 31, 6, 243, DateTimeKind.Utc).AddTicks(6664));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 17, 6, 31, 6, 243, DateTimeKind.Utc).AddTicks(6670));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 17, 6, 31, 6, 243, DateTimeKind.Utc).AddTicks(6673));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HiddenOnHome",
                table: "Products");

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
    }
}
