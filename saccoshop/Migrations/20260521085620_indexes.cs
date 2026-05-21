using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saccoshop.Migrations
{
    /// <inheritdoc />
    public partial class indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_SellerProfileId",
                table: "UserProfiles");

            migrationBuilder.RenameIndex(
                name: "IX_GroupMembers_UserGroupId",
                table: "GroupMembers",
                newName: "IX_GroupMemberships_GroupId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42cd3af3-f319-4118-a604-4442d487b923",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a4eae02-d747-4cd0-9374-2a3cebad7f85", new DateTime(2026, 5, 21, 8, 56, 18, 331, DateTimeKind.Utc).AddTicks(7607), "AQAAAAIAAYagAAAAEAt2y+sO5376FNVqCB5+Xg3RA5mLYPqn290DBtyDkqQLg2g7A+FnGspZngeAddSngg==", "219c9adb-41a1-4042-a808-148c780a5acb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f2b8a40-d899-4345-aa3e-7b98712bc112",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3d438743-5053-4e05-8dff-706c3be8f6b5", new DateTime(2026, 5, 21, 8, 56, 18, 429, DateTimeKind.Utc).AddTicks(4707), "AQAAAAIAAYagAAAAEE3iqRD7PFJoIrI5vDEbepUR7Stv5cH/ShabnrYwId2es0WFkp7YIc0Ka8xnUBVv2A==", "9daf3d7c-843b-44d7-9148-dcb103270cd4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd363936-63d0-4ace-bc46-a2f1348cb61e",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b024ca52-698c-497a-98c2-739f08aabc8c", new DateTime(2026, 5, 21, 8, 56, 18, 522, DateTimeKind.Utc).AddTicks(9688), "AQAAAAIAAYagAAAAEENqJk9DmcjP/Soh9cvX6qYLV1EdtYqQcHCeZJRDyhmQsXOenB4y6NrNcmaBc/fcIw==", "7bc8240b-5aef-49d8-9777-e0ac4500d09d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9c8e5b0-4dec-4f8e-9a2b-c41c725a1513",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4134050-b57c-4bf5-9d46-1666ea76fcb6", new DateTime(2026, 5, 21, 8, 56, 18, 618, DateTimeKind.Utc).AddTicks(9601), "AQAAAAIAAYagAAAAELyhdy1gYnUFQAZgFxoescvCvsIF/3Z6jvwuAlucTQhgTfdQ1z7OzqTI6stL8Q7Buw==", "231b2377-dce8-4318-8b8e-eba9544dfd2c" });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("220cc6eb-238a-424d-962b-2dca18f731d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 21, 8, 56, 18, 726, DateTimeKind.Utc).AddTicks(5824));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("440a45a7-dd82-4586-883a-6b2b1205dfad"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 21, 8, 56, 18, 726, DateTimeKind.Utc).AddTicks(5868));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("4970c779-c767-4eb8-8ae4-12f38514fd5e"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 21, 8, 56, 18, 726, DateTimeKind.Utc).AddTicks(5873));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "UserProfileId",
                keyValue: new Guid("bb69367c-b5e1-4090-d8f4-08de930d2678"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 21, 8, 56, 18, 726, DateTimeKind.Utc).AddTicks(5876));

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_CreatedAt",
                table: "UserProfiles",
                column: "CreatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_SellerProfileId_ActiveGroupId",
                table: "UserProfiles",
                columns: new[] { "SellerProfileId", "ActiveGroupId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_CreatedAt",
                table: "UserGroups",
                column: "CreatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUsers_FirstName_LastName",
                table: "AspNetUsers",
                columns: new[] { "FirstName", "LastName" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_CreatedAt",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_SellerProfileId_ActiveGroupId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserGroups_CreatedAt",
                table: "UserGroups");

            migrationBuilder.DropIndex(
                name: "IX_IdentityUsers_FirstName_LastName",
                table: "AspNetUsers");

            migrationBuilder.RenameIndex(
                name: "IX_GroupMemberships_GroupId",
                table: "GroupMembers",
                newName: "IX_GroupMembers_UserGroupId");

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

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_SellerProfileId",
                table: "UserProfiles",
                column: "SellerProfileId");
        }
    }
}
