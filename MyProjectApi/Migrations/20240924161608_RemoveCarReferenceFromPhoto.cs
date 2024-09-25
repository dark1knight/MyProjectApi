using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProjectApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCarReferenceFromPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Photos_PhotoID",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_PhotoID",
                table: "Cars");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_PhotoID",
                table: "Cars",
                column: "PhotoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Photos_PhotoID",
                table: "Cars",
                column: "PhotoID",
                principalTable: "Photos",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Photos_PhotoID",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_PhotoID",
                table: "Cars");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_PhotoID",
                table: "Cars",
                column: "PhotoID",
                unique: true,
                filter: "[PhotoID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Photos_PhotoID",
                table: "Cars",
                column: "PhotoID",
                principalTable: "Photos",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
