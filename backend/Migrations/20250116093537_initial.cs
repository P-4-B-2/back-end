using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Benches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    made_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    order_number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    First_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    start_datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sentiment = table.Column<int>(type: "int", nullable: false),
                    summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BenchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversations_Benches_BenchId",
                        column: x => x.BenchId,
                        principalTable: "Benches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BenchId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Histories_Benches_BenchId",
                        column: x => x.BenchId,
                        principalTable: "Benches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Histories_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Histories_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Benches",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bench A" },
                    { 2, "Bench B" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Latitude", "Longitude" },
                values: new object[,]
                {
                    { 1, "-74.005974", "40.712776" },
                    { 2, "-118.243683", "34.052235" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Text", "is_active", "made_at", "order_number" },
                values: new object[,]
                {
                    { 1, "What is your name?", true, new DateTime(2025, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "How can I help you?", true, new DateTime(2025, 1, 1, 9, 5, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Inactive" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "First_name", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "Doe", "John", "hashedpassword1" },
                    { 2, "jane.smith@example.com", "Smith", "Jane", "hashedpassword2" }
                });

            migrationBuilder.InsertData(
                table: "Conversations",
                columns: new[] { "Id", "BenchId", "end_datetime", "sentiment", "start_datetime", "summary" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "First conversation summary" },
                    { 2, 2, new DateTime(2025, 1, 2, 11, 20, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2025, 1, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), "Second conversation summary" }
                });

            migrationBuilder.InsertData(
                table: "Histories",
                columns: new[] { "Id", "BenchId", "LocationId", "StatusId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "ConversationId", "QuestionId", "Response" },
                values: new object[,]
                {
                    { 1, 1, 1, "My name is John." },
                    { 2, 1, 2, "I need help with coding." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_ConversationId",
                table: "Answers",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_BenchId",
                table: "Conversations",
                column: "BenchId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_BenchId",
                table: "Histories",
                column: "BenchId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_LocationId",
                table: "Histories",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_StatusId",
                table: "Histories",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Benches");
        }
    }
}
