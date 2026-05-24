using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductionSystem.Infrastructure.Data.Migrations
{
    public partial class addPermissionSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "PermissionKey",
                table: "RolePermissions");

            migrationBuilder.AddColumn<int>(
                name: "PermissionId",
                table: "RolePermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "ParentId", "Title" },
                values: new object[,]
                {
                    { 1, null, "Home" },
                    { 11, 2, "Product" },
                    { 33, 11, "CreateProduct" },
                    { 34, 11, "EditProduct" },
                    { 35, 11, "DeleteProduct" },
                    { 10, 2, "Parameter" },
                    { 30, 10, "CreateParameter" },
                    { 31, 10, "EditParameter" },
                    { 32, 10, "DeleteParameter" },
                    { 42, 10, "ParameterValue" },
                    { 43, 42, "CreateParameterValue" },
                    { 44, 42, "EditParameterValue" },
                    { 45, 42, "DeleteParameterValue" },
                    { 12, 3, "Order" },
                    { 36, 12, "CreateOrder" },
                    { 37, 12, "EditOrder" },
                    { 38, 12, "DeleteOrder" },
                    { 13, 3, "ProductionReceipt" },
                    { 39, 3, "CreateProductionReceipt" },
                    { 40, 3, "EditProductionReceipt" },
                    { 23, 7, "DeleteUnit" },
                    { 41, 3, "DeleteProductionReceipt" },
                    { 22, 7, "EditUnit" },
                    { 7, 2, "Unit" },
                    { 2, null, "BasicInformation" },
                    { 3, null, "Production" },
                    { 4, null, "MyHome" },
                    { 5, 2, "User" },
                    { 15, 5, "CreateUser" },
                    { 16, 5, "EditUser" },
                    { 17, 5, "DeleteUser" },
                    { 6, 2, "Role" },
                    { 18, 6, "CreateRole" },
                    { 19, 6, "EditRole" },
                    { 20, 6, "DeleteRole" },
                    { 8, 2, "Personnel" },
                    { 24, 8, "CreatePersonnel" },
                    { 25, 8, "EditPersonnel" },
                    { 26, 8, "DeletePersonnel" },
                    { 9, 2, "Customer" },
                    { 27, 9, "CreateCustomer" },
                    { 28, 9, "EditCustomer" }
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "ParentId", "Title" },
                values: new object[] { 29, 9, "DeleteCustomer" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "ParentId", "Title" },
                values: new object[] { 21, 7, "CreateUnit" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "ParentId", "Title" },
                values: new object[] { 14, 3, "Report" });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Permission_PermissionId",
                table: "RolePermissions",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Permission_PermissionId",
                table: "RolePermissions");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                table: "RolePermissions");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "RolePermissions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "RolePermissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RolePermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PermissionKey",
                table: "RolePermissions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
