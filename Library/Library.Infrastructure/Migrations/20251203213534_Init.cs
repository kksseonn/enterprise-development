using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "edition_type",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_edition_type", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "edition_type",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("a0000000-0000-0000-0000-000000000001"), "Учебник" },
                    { new Guid("a0000000-0000-0000-0000-000000000002"), "Монография" },
                    { new Guid("a0000000-0000-0000-0000-000000000003"), "Справочник" },
                    { new Guid("a0000000-0000-0000-0000-000000000004"), "Художественная литература" },
                    { new Guid("a0000000-0000-0000-0000-000000000005"), "Фантастика" },
                    { new Guid("a0000000-0000-0000-0000-000000000006"), "Поэзия" },
                    { new Guid("a0000000-0000-0000-0000-000000000007"), "Научная статья" },
                    { new Guid("a0000000-0000-0000-0000-000000000008"), "Энциклопедия" },
                    { new Guid("a0000000-0000-0000-0000-000000000009"), "Доклад" },
                    { new Guid("a0000000-0000-0000-0000-000000000010"), "Комикс" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "edition_type");
        }
    }
}
