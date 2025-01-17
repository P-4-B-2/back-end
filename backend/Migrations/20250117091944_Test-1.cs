using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Conversations",
                columns: new[] { "Id", "BenchId", "end_datetime", "sentiment", "start_datetime", "summary" },
                values: new object[] { 3, 2, new DateTime(2025, 1, 2, 12, 20, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2025, 1, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), "Third conversation summary" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Text", "is_active", "made_at", "order_number" },
                values: new object[,]
                {
                    { 3, "Do you think that our townhall is modern enough?", false, new DateTime(2025, 1, 1, 9, 8, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, "Does our local park need a new extention?", false, new DateTime(2025, 1, 2, 9, 8, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 5, "What do you think of our local museum?", false, new DateTime(2025, 1, 3, 9, 8, 0, 0, DateTimeKind.Unspecified), 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Conversations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
