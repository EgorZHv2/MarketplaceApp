using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class DeleteIsDeletedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "StaticFileInfos");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DeliveryTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "UpdatorId",
                table: "Users",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeletorId",
                table: "Users",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Users",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatorId",
                table: "Types",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeletorId",
                table: "Types",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Types",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatorId",
                table: "StaticFileInfos",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeletorId",
                table: "StaticFileInfos",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "StaticFileInfos",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatorId",
                table: "Shops",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeletorId",
                table: "Shops",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Shops",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatorId",
                table: "Reviews",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeletorId",
                table: "Reviews",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Reviews",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatorId",
                table: "Products",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeletorId",
                table: "Products",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Products",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatorId",
                table: "PaymentMethods",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeletorId",
                table: "PaymentMethods",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "PaymentMethods",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatorId",
                table: "DeliveryTypes",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeletorId",
                table: "DeliveryTypes",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "DeliveryTypes",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatorId",
                table: "Categories",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeletorId",
                table: "Categories",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Categories",
                newName: "CreatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Users",
                newName: "UpdatorId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Users",
                newName: "DeletorId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Users",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Types",
                newName: "UpdatorId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Types",
                newName: "DeletorId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Types",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "StaticFileInfos",
                newName: "UpdatorId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "StaticFileInfos",
                newName: "DeletorId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "StaticFileInfos",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Shops",
                newName: "UpdatorId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Shops",
                newName: "DeletorId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Shops",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Reviews",
                newName: "UpdatorId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Reviews",
                newName: "DeletorId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Reviews",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Products",
                newName: "UpdatorId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Products",
                newName: "DeletorId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Products",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "PaymentMethods",
                newName: "UpdatorId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "PaymentMethods",
                newName: "DeletorId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "PaymentMethods",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "DeliveryTypes",
                newName: "UpdatorId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "DeliveryTypes",
                newName: "DeletorId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "DeliveryTypes",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Categories",
                newName: "UpdatorId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Categories",
                newName: "DeletorId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Categories",
                newName: "CreatorId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Types",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "StaticFileInfos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Shops",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Reviews",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PaymentMethods",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DeliveryTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
