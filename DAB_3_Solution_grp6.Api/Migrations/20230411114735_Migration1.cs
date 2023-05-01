using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAB_2_Solution_grp6.Api.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Customers_Cpr",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Customers_Cpr",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "Cpr",
                table: "Reservations",
                newName: "AuId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_Cpr",
                table: "Reservations",
                newName: "IX_Reservations_AuId");

            migrationBuilder.RenameColumn(
                name: "Cpr",
                table: "Ratings",
                newName: "AuId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_Cpr",
                table: "Ratings",
                newName: "IX_Ratings_AuId");

            migrationBuilder.RenameColumn(
                name: "Cpr",
                table: "Customers",
                newName: "AuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Customers_AuId",
                table: "Ratings",
                column: "AuId",
                principalTable: "Customers",
                principalColumn: "AuId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Customers_AuId",
                table: "Reservations",
                column: "AuId",
                principalTable: "Customers",
                principalColumn: "AuId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Customers_AuId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Customers_AuId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "AuId",
                table: "Reservations",
                newName: "Cpr");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_AuId",
                table: "Reservations",
                newName: "IX_Reservations_Cpr");

            migrationBuilder.RenameColumn(
                name: "AuId",
                table: "Ratings",
                newName: "Cpr");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_AuId",
                table: "Ratings",
                newName: "IX_Ratings_Cpr");

            migrationBuilder.RenameColumn(
                name: "AuId",
                table: "Customers",
                newName: "Cpr");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Customers_Cpr",
                table: "Ratings",
                column: "Cpr",
                principalTable: "Customers",
                principalColumn: "Cpr",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Customers_Cpr",
                table: "Reservations",
                column: "Cpr",
                principalTable: "Customers",
                principalColumn: "Cpr",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
