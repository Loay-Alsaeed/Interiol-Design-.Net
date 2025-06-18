using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_.Net.Migrations
{
    /// <inheritdoc />
    public partial class updatedateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dateTime",
                table: "Comments",
                newName: "DateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Comments",
                newName: "dateTime");
        }
    }
}
