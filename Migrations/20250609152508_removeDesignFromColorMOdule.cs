using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_.Net.Migrations
{
    /// <inheritdoc />
    public partial class removeDesignFromColorMOdule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colors_Designs_DesignId",
                table: "Colors");

            migrationBuilder.AddForeignKey(
                name: "FK_Colors_Designs_DesignId",
                table: "Colors",
                column: "DesignId",
                principalTable: "Designs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colors_Designs_DesignId",
                table: "Colors");

            migrationBuilder.AddForeignKey(
                name: "FK_Colors_Designs_DesignId",
                table: "Colors",
                column: "DesignId",
                principalTable: "Designs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
