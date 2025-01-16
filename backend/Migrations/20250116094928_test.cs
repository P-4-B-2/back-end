using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Response",
                value: "It's nice and peaceful.");

            migrationBuilder.UpdateData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Response",
                value: "Maybe more greens, vegetation would be nice.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Text",
                value: "What do you think about this neighbourhood?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Text",
                value: "What new features do you think this village need?");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Response",
                value: "My name is John.");

            migrationBuilder.UpdateData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Response",
                value: "I need help with coding.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Text",
                value: "What is your name?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Text",
                value: "How can I help you?");
        }
    }
}
