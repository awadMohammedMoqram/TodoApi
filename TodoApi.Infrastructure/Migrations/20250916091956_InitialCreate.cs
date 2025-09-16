using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[] { "Id", "Category", "CompletedAt", "CreatedAt", "Description", "IsCompleted", "Priority", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Setup", null, new DateTime(2025, 9, 16, 9, 19, 53, 893, DateTimeKind.Utc).AddTicks(5251), "Install and configure PostgreSQL for the Todo application", false, 3, "Setup PostgreSQL Database", null },
                    { 2, "Development", null, new DateTime(2025, 9, 16, 9, 19, 53, 893, DateTimeKind.Utc).AddTicks(5255), "Implement CRUD operations for Todo items", false, 3, "Create API Endpoints", null },
                    { 3, "Documentation", null, new DateTime(2025, 9, 16, 9, 19, 53, 893, DateTimeKind.Utc).AddTicks(5257), "Configure Swagger for API documentation", false, 2, "Add Swagger Documentation", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_Category",
                table: "TodoItems",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_CreatedAt",
                table: "TodoItems",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_IsCompleted",
                table: "TodoItems",
                column: "IsCompleted");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_Priority",
                table: "TodoItems",
                column: "Priority");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItems");
        }
    }
}
