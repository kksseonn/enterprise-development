using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
                    Type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_edition_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "publisher",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publisher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "reader",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Surname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Patronymic = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    RegistrationDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "book",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InventoryNumber = table.Column<int>(type: "integer", nullable: false),
                    CatalogCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Authors = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    EditionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PublisherId = table.Column<Guid>(type: "uuid", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_book_edition_type_EditionTypeId",
                        column: x => x.EditionTypeId,
                        principalTable: "edition_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_book_publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "publisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "borrow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReaderId = table.Column<Guid>(type: "uuid", nullable: false),
                    BorrowDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Days = table.Column<int>(type: "integer", nullable: false),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_borrow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_borrow_book_BookId",
                        column: x => x.BookId,
                        principalTable: "book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_borrow_reader_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "reader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_book_EditionTypeId",
                table: "book",
                column: "EditionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_book_PublisherId",
                table: "book",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_borrow_BookId",
                table: "borrow",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_borrow_ReaderId",
                table: "borrow",
                column: "ReaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "borrow");

            migrationBuilder.DropTable(
                name: "book");

            migrationBuilder.DropTable(
                name: "reader");

            migrationBuilder.DropTable(
                name: "edition_type");

            migrationBuilder.DropTable(
                name: "publisher");
        }
    }
}
