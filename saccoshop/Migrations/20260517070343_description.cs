using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class description : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ee278cf-f9d8-4c93-b95f-4a4e96bbf717", new DateTime(2026, 5, 17, 7, 3, 42, 34, DateTimeKind.Utc).AddTicks(7540), "AQAAAAIAAYagAAAAEDadBkIzR270hivhxDSiF4IPJFlRaNhkohB+xUgD0oNaxOq7PJnNuI6RPxVT062O9Q==", "9387818d-9b37-49b7-8266-c6510404a4ac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6563d0f4-a2b6-45cc-87cb-f633d5f5b57d", new DateTime(2026, 5, 17, 7, 3, 42, 133, DateTimeKind.Utc).AddTicks(2503), "AQAAAAIAAYagAAAAEPnSkFegeujQdW0wA9Yy0Tb0gyDWzJ9ftZcvqGrZ4VL9fDzcK8Albtyn7ra866HGEQ==", "4ef554da-a085-4e98-95d9-a214de7d6eca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff726111-ce00-4879-b4ac-ea57b8d71954", new DateTime(2026, 5, 17, 7, 3, 42, 228, DateTimeKind.Utc).AddTicks(9175), "AQAAAAIAAYagAAAAEGXDp8G8XHZPmof+n6rncD6KMgKrLWu67a2tX0pOcbU7xvax01PkAyYWQZMD6j7mxA==", "ebbdf6ba-db87-4bac-a3b2-e2dc3d40abe7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "731f2876-0e2c-4a12-aa52-a85e77341b22", new DateTime(2026, 5, 17, 7, 3, 42, 325, DateTimeKind.Utc).AddTicks(1384), "AQAAAAIAAYagAAAAEP1p03Kk5dSXNpRgBhJn/8w+LdGy0y7r/sbmUtWMY0iKb7pcvMo4KaYJzK4Gwx77Jw==", "cd558e55-cbeb-4cbf-b0c2-8b91e93f56af" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 17, 7, 3, 42, 426, DateTimeKind.Utc).AddTicks(1067));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 17, 7, 3, 42, 426, DateTimeKind.Utc).AddTicks(1108));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 17, 7, 3, 42, 426, DateTimeKind.Utc).AddTicks(1112));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 17, 7, 3, 42, 426, DateTimeKind.Utc).AddTicks(1116));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Products");

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
    }
}
