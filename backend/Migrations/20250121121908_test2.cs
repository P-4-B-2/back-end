using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "made_at",
                table: "Questions",
                newName: "MadeAt");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "Questions",
                newName: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MadeAt",
                table: "Questions",
                newName: "made_at");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Questions",
                newName: "is_active");
        }
    }
}
