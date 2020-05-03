namespace SheryLady.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddOrderProductStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "OrdersProducts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrdersProducts");
        }
    }
}
