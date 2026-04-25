using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce12.DAL.Migrations
{
    /// <inheritdoc />
    public partial class paymentstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "paymentStatus",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "paymentStatus",
                table: "orders");
        }
    }
}
