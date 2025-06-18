using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_.Net.Migrations
{
    /// <inheritdoc />
    public partial class updateLayoutTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consideration_Designs_DesignId",
                table: "Consideration");

            migrationBuilder.DropForeignKey(
                name: "FK_Designs_LayoutImages_LayoutImageId",
                table: "Designs");

            migrationBuilder.DropIndex(
                name: "IX_Designs_LayoutImageId",
                table: "Designs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Consideration",
                table: "Consideration");

            migrationBuilder.DropColumn(
                name: "LayoutImageId",
                table: "Designs");

            migrationBuilder.RenameTable(
                name: "Consideration",
                newName: "Considerations");

            migrationBuilder.RenameIndex(
                name: "IX_Consideration_DesignId",
                table: "Considerations",
                newName: "IX_Considerations_DesignId");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "DesignId",
                table: "Materials",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "LayoutImageUrl",
                table: "Designs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Considerations",
                table: "Considerations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LayoutImages_DesignId",
                table: "LayoutImages",
                column: "DesignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Considerations_Designs_DesignId",
                table: "Considerations",
                column: "DesignId",
                principalTable: "Designs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LayoutImages_Designs_DesignId",
                table: "LayoutImages",
                column: "DesignId",
                principalTable: "Designs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Considerations_Designs_DesignId",
                table: "Considerations");

            migrationBuilder.DropForeignKey(
                name: "FK_LayoutImages_Designs_DesignId",
                table: "LayoutImages");

            migrationBuilder.DropIndex(
                name: "IX_LayoutImages_DesignId",
                table: "LayoutImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Considerations",
                table: "Considerations");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LayoutImageUrl",
                table: "Designs");

            migrationBuilder.RenameTable(
                name: "Considerations",
                newName: "Consideration");

            migrationBuilder.RenameIndex(
                name: "IX_Considerations_DesignId",
                table: "Consideration",
                newName: "IX_Consideration_DesignId");

            migrationBuilder.AlterColumn<Guid>(
                name: "DesignId",
                table: "Materials",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LayoutImageId",
                table: "Designs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Consideration",
                table: "Consideration",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Designs_LayoutImageId",
                table: "Designs",
                column: "LayoutImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Consideration_Designs_DesignId",
                table: "Consideration",
                column: "DesignId",
                principalTable: "Designs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Designs_LayoutImages_LayoutImageId",
                table: "Designs",
                column: "LayoutImageId",
                principalTable: "LayoutImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
