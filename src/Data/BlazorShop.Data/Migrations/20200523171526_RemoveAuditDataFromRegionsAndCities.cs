namespace BlazorShop.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemoveAuditDataFromRegionsAndCities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Cities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Regions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Regions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Regions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Regions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Cities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Cities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Cities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Cities",
                type: "datetime2",
                nullable: true);
        }
    }
}
