using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STOWebApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeleteBehaviorBtwWorkersAndMastertOnCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Masters_Workers_WorkerId",
                table: "Masters");

            migrationBuilder.AddForeignKey(
                name: "FK_Masters_Workers_WorkerId",
                table: "Masters",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Masters_Workers_WorkerId",
                table: "Masters");

            migrationBuilder.AddForeignKey(
                name: "FK_Masters_Workers_WorkerId",
                table: "Masters",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id");
        }
    }
}
