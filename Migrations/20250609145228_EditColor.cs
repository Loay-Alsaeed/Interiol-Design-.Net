using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_.Net.Migrations
{
    /// <inheritdoc />
    public partial class EditColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesignColors");

            migrationBuilder.AddColumn<Guid>(
                name: "DesignId",
                table: "Colors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Materials_DesignId",
                table: "Materials",
                column: "DesignId");

            migrationBuilder.CreateIndex(
                name: "IX_Colors_DesignId",
                table: "Colors",
                column: "DesignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Colors_Designs_DesignId",
                table: "Colors",
                column: "DesignId",
                principalTable: "Designs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Designs_DesignId",
                table: "Materials",
                column: "DesignId",
                principalTable: "Designs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colors_Designs_DesignId",
                table: "Colors");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Designs_DesignId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_DesignId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Colors_DesignId",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "DesignId",
                table: "Colors");

            migrationBuilder.CreateTable(
                name: "DesignColors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DesignId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignColors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignColors_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignColors_Colors_ColorId1",
                        column: x => x.ColorId1,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DesignColors_Designs_DesignId",
                        column: x => x.DesignId,
                        principalTable: "Designs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesignColors_ColorId",
                table: "DesignColors",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignColors_ColorId1",
                table: "DesignColors",
                column: "ColorId1");

            migrationBuilder.CreateIndex(
                name: "IX_DesignColors_DesignId",
                table: "DesignColors",
                column: "DesignId");
        }
    }
}
