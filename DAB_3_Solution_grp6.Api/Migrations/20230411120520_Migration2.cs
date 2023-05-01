using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAB_2_Solution_grp6.Api.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JitMeals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JitMeals",
                columns: table => new
                {
                    JitMealId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CanteenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JitName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JitMeals", x => x.JitMealId);
                    table.ForeignKey(
                        name: "FK_JitMeals_Canteens_CanteenId",
                        column: x => x.CanteenId,
                        principalTable: "Canteens",
                        principalColumn: "CanteenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JitMeals_CanteenId",
                table: "JitMeals",
                column: "CanteenId");
        }
    }
}
