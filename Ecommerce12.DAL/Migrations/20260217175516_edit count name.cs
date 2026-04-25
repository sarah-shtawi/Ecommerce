using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce12.DAL.Migrations
{
    /// <inheritdoc />
    public partial class editcountname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "count",
                table: "carts",
                newName: "Count");

            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "carts",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Count",
                table: "carts",
                newName: "count");

            migrationBuilder.AlterColumn<int>(
                name: "count",
                table: "carts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);
        }
    }
}
