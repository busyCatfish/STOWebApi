using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STOWebApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeleteBehaviorBtwOrdersAndCarsOnSetNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Cars_CarVincode",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "CarVincode",
                table: "Orders",
                type: "nvarchar(17)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(17)");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Cars_CarVincode",
                table: "Orders",
                column: "CarVincode",
                principalTable: "Cars",
                principalColumn: "Vincode",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Cars_CarVincode",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "CarVincode",
                table: "Orders",
                type: "nvarchar(17)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(17)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Cars_CarVincode",
                table: "Orders",
                column: "CarVincode",
                principalTable: "Cars",
                principalColumn: "Vincode");
        }
    }
}
