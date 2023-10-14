using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STOWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderMasterEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersMasters_Masters_MastersId",
                table: "OrdersMasters");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersMasters_Orders_OrdersId",
                table: "OrdersMasters");

            migrationBuilder.RenameColumn(
                name: "OrdersId",
                table: "OrdersMasters",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "MastersId",
                table: "OrdersMasters",
                newName: "MasterId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdersMasters_OrdersId",
                table: "OrdersMasters",
                newName: "IX_OrdersMasters_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersMasters_Masters_MasterId",
                table: "OrdersMasters",
                column: "MasterId",
                principalTable: "Masters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersMasters_Orders_OrderId",
                table: "OrdersMasters",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersMasters_Masters_MasterId",
                table: "OrdersMasters");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersMasters_Orders_OrderId",
                table: "OrdersMasters");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrdersMasters",
                newName: "OrdersId");

            migrationBuilder.RenameColumn(
                name: "MasterId",
                table: "OrdersMasters",
                newName: "MastersId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdersMasters_OrderId",
                table: "OrdersMasters",
                newName: "IX_OrdersMasters_OrdersId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersMasters_Masters_MastersId",
                table: "OrdersMasters",
                column: "MastersId",
                principalTable: "Masters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersMasters_Orders_OrdersId",
                table: "OrdersMasters",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
