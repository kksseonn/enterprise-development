using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchema : Migration
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_book_publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "publisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
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

            migrationBuilder.InsertData(
                table: "publisher",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("b0000000-0000-0000-0000-000000000001"), "Эксмо" },
                    { new Guid("b0000000-0000-0000-0000-000000000002"), "АСТ" },
                    { new Guid("b0000000-0000-0000-0000-000000000003"), "Просвещение" },
                    { new Guid("b0000000-0000-0000-0000-000000000004"), "Наука" },
                    { new Guid("b0000000-0000-0000-0000-000000000005"), "Олимп-Бизнес" },
                    { new Guid("b0000000-0000-0000-0000-000000000006"), "МИФ" },
                    { new Guid("b0000000-0000-0000-0000-000000000007"), "Дрофа" },
                    { new Guid("b0000000-0000-0000-0000-000000000008"), "Азбука" },
                    { new Guid("b0000000-0000-0000-0000-000000000009"), "Росмэн" },
                    { new Guid("b0000000-0000-0000-0000-000000000010"), "Питер" }
                });

            migrationBuilder.InsertData(
                table: "reader",
                columns: new[] { "Id", "Address", "Name", "Patronymic", "Phone", "RegistrationDate", "Surname" },
                values: new object[,]
                {
                    { new Guid("c0000000-0000-0000-0000-000000000001"), "ул. Ленина, 1", "Иван", "Иванович", "+79001234567", new DateOnly(2023, 12, 3), "Иванов" },
                    { new Guid("c0000000-0000-0000-0000-000000000002"), "ул. Гагарина, 2", "Петр", "Петрович", "+79012345678", new DateOnly(2024, 6, 22), "Петров" },
                    { new Guid("c0000000-0000-0000-0000-000000000003"), "ул. Чехова, 3", "Анна", "Павловна", "+79023456789", new DateOnly(2025, 2, 15), "Сидорова" },
                    { new Guid("c0000000-0000-0000-0000-000000000004"), "ул. Пушкина, 4", "Алексей", "Николаевич", "+79034567890", new DateOnly(2023, 8, 10), "Кузнецов" },
                    { new Guid("c0000000-0000-0000-0000-000000000005"), "ул. Кирова, 5", "Ольга", "Викторовна", "+79045678901", new DateOnly(2024, 4, 5), "Смирнова" },
                    { new Guid("c0000000-0000-0000-0000-000000000006"), "ул. Мира, 6", "Дмитрий", "Сергеевич", "+79056789012", new DateOnly(2025, 11, 14), "Попов" },
                    { new Guid("c0000000-0000-0000-0000-000000000007"), "ул. Победы, 7", "Елена", "Андреевна", "+79067890123", new DateOnly(2023, 2, 22), "Морозова" },
                    { new Guid("c0000000-0000-0000-0000-000000000008"), "ул. Советская, 8", "Николай", "Петрович", "+79078901234", new DateOnly(2024, 9, 19), "Соколов" },
                    { new Guid("c0000000-0000-0000-0000-000000000009"), "ул. Школьная, 9", "Мария", "Ивановна", "+79089012345", new DateOnly(2025, 5, 3), "Васильева" },
                    { new Guid("c0000000-0000-0000-0000-000000000010"), "ул. Центральная, 10", "Андрей", "Владимирович", "+79090123456", new DateOnly(2023, 1, 17), "Федоров" }
                });

            migrationBuilder.InsertData(
                table: "book",
                columns: new[] { "Id", "Authors", "CatalogCode", "EditionTypeId", "InventoryNumber", "PublisherId", "Title", "Year" },
                values: new object[,]
                {
                    { new Guid("d0000000-0000-0000-0000-000000000001"), "Л.Н. Толстой", "A-01", new Guid("a0000000-0000-0000-0000-000000000004"), 1001, new Guid("b0000000-0000-0000-0000-000000000001"), "Война и мир", 1869 },
                    { new Guid("d0000000-0000-0000-0000-000000000002"), "Л.Н. Толстой", "A-01", new Guid("a0000000-0000-0000-0000-000000000004"), 1002, new Guid("b0000000-0000-0000-0000-000000000001"), "Война и мир", 1869 },
                    { new Guid("d0000000-0000-0000-0000-000000000003"), "Ф.М. Достоевский", "A-02", new Guid("a0000000-0000-0000-0000-000000000004"), 1003, new Guid("b0000000-0000-0000-0000-000000000002"), "Преступление и наказание", 1866 },
                    { new Guid("d0000000-0000-0000-0000-000000000004"), "Ф.М. Достоевский", "A-02", new Guid("a0000000-0000-0000-0000-000000000004"), 1004, new Guid("b0000000-0000-0000-0000-000000000002"), "Преступление и наказание", 1866 },
                    { new Guid("d0000000-0000-0000-0000-000000000005"), "А.С. Пушкин", "B-01", new Guid("a0000000-0000-0000-0000-000000000004"), 1005, new Guid("b0000000-0000-0000-0000-000000000008"), "Капитанская дочка", 1836 },
                    { new Guid("d0000000-0000-0000-0000-000000000006"), "А.С. Пушкин", "B-01", new Guid("a0000000-0000-0000-0000-000000000004"), 1006, new Guid("b0000000-0000-0000-0000-000000000008"), "Капитанская дочка", 1836 },
                    { new Guid("d0000000-0000-0000-0000-000000000007"), "Л.Н. Толстой", "B-02", new Guid("a0000000-0000-0000-0000-000000000004"), 1007, new Guid("b0000000-0000-0000-0000-000000000001"), "Анна Каренина", 1877 },
                    { new Guid("d0000000-0000-0000-0000-000000000008"), "Л.Н. Толстой", "B-02", new Guid("a0000000-0000-0000-0000-000000000004"), 1008, new Guid("b0000000-0000-0000-0000-000000000001"), "Анна Каренина", 1877 },
                    { new Guid("d0000000-0000-0000-0000-000000000009"), "И.С. Тургенев", "C-01", new Guid("a0000000-0000-0000-0000-000000000004"), 1009, new Guid("b0000000-0000-0000-0000-000000000003"), "Отцы и дети", 1862 },
                    { new Guid("d0000000-0000-0000-0000-000000000010"), "И.С. Тургенев", "C-01", new Guid("a0000000-0000-0000-0000-000000000004"), 1010, new Guid("b0000000-0000-0000-0000-000000000003"), "Отцы и дети", 1862 },
                    { new Guid("d0000000-0000-0000-0000-000000000011"), "Ф.М. Достоевский", "C-02", new Guid("a0000000-0000-0000-0000-000000000004"), 1011, new Guid("b0000000-0000-0000-0000-000000000002"), "Идиот", 1869 },
                    { new Guid("d0000000-0000-0000-0000-000000000012"), "Ф.М. Достоевский", "C-02", new Guid("a0000000-0000-0000-0000-000000000004"), 1012, new Guid("b0000000-0000-0000-0000-000000000002"), "Идиот", 1869 },
                    { new Guid("d0000000-0000-0000-0000-000000000013"), "М.А. Булгаков", "D-01", new Guid("a0000000-0000-0000-0000-000000000005"), 1013, new Guid("b0000000-0000-0000-0000-000000000006"), "Мастер и Маргарита", 1967 },
                    { new Guid("d0000000-0000-0000-0000-000000000014"), "М.А. Булгаков", "D-01", new Guid("a0000000-0000-0000-0000-000000000005"), 1014, new Guid("b0000000-0000-0000-0000-000000000006"), "Мастер и Маргарита", 1967 },
                    { new Guid("d0000000-0000-0000-0000-000000000015"), "А.С. Пушкин", "D-02", new Guid("a0000000-0000-0000-0000-000000000006"), 1015, new Guid("b0000000-0000-0000-0000-000000000008"), "Евгений Онегин", 1833 },
                    { new Guid("d0000000-0000-0000-0000-000000000016"), "А.С. Пушкин", "D-02", new Guid("a0000000-0000-0000-0000-000000000006"), 1016, new Guid("b0000000-0000-0000-0000-000000000008"), "Евгений Онегин", 1833 },
                    { new Guid("d0000000-0000-0000-0000-000000000017"), "М.А. Булгаков", "E-01", new Guid("a0000000-0000-0000-0000-000000000005"), 1017, new Guid("b0000000-0000-0000-0000-000000000007"), "Собачье сердце", 1925 },
                    { new Guid("d0000000-0000-0000-0000-000000000018"), "М.А. Булгаков", "E-01", new Guid("a0000000-0000-0000-0000-000000000005"), 1018, new Guid("b0000000-0000-0000-0000-000000000007"), "Собачье сердце", 1925 },
                    { new Guid("d0000000-0000-0000-0000-000000000019"), "Б.Л. Пастернак", "E-02", new Guid("a0000000-0000-0000-0000-000000000005"), 1019, new Guid("b0000000-0000-0000-0000-000000000009"), "Доктор Живаго", 1957 },
                    { new Guid("d0000000-0000-0000-0000-000000000020"), "Б.Л. Пастернак", "E-02", new Guid("a0000000-0000-0000-0000-000000000005"), 1020, new Guid("b0000000-0000-0000-0000-000000000009"), "Доктор Живаго", 1957 }
                });

            migrationBuilder.InsertData(
                table: "borrow",
                columns: new[] { "Id", "BookId", "BorrowDate", "Days", "DueDate", "ReaderId", "ReturnDate" },
                values: new object[,]
                {
                    { new Guid("e0000000-0000-0000-0000-000000000001"), new Guid("d0000000-0000-0000-0000-000000000001"), new DateOnly(2024, 1, 10), 14, new DateOnly(1, 1, 1), new Guid("c0000000-0000-0000-0000-000000000001"), new DateOnly(2024, 1, 24) },
                    { new Guid("e0000000-0000-0000-0000-000000000002"), new Guid("d0000000-0000-0000-0000-000000000002"), new DateOnly(2024, 7, 1), 10, new DateOnly(1, 1, 1), new Guid("c0000000-0000-0000-0000-000000000002"), new DateOnly(2024, 7, 11) },
                    { new Guid("e0000000-0000-0000-0000-000000000003"), new Guid("d0000000-0000-0000-0000-000000000003"), new DateOnly(2024, 8, 5), 7, new DateOnly(1, 1, 1), new Guid("c0000000-0000-0000-0000-000000000004"), new DateOnly(2024, 8, 12) },
                    { new Guid("e0000000-0000-0000-0000-000000000004"), new Guid("d0000000-0000-0000-0000-000000000004"), new DateOnly(2024, 5, 1), 20, new DateOnly(1, 1, 1), new Guid("c0000000-0000-0000-0000-000000000005"), new DateOnly(2024, 5, 21) },
                    { new Guid("e0000000-0000-0000-0000-000000000005"), new Guid("d0000000-0000-0000-0000-000000000005"), new DateOnly(2024, 3, 1), 14, new DateOnly(1, 1, 1), new Guid("c0000000-0000-0000-0000-000000000007"), new DateOnly(2024, 3, 15) },
                    { new Guid("e0000000-0000-0000-0000-000000000006"), new Guid("d0000000-0000-0000-0000-000000000006"), new DateOnly(2025, 9, 25), 30, new DateOnly(1, 1, 1), new Guid("c0000000-0000-0000-0000-000000000001"), null },
                    { new Guid("e0000000-0000-0000-0000-000000000007"), new Guid("d0000000-0000-0000-0000-000000000007"), new DateOnly(2025, 9, 30), 20, new DateOnly(1, 1, 1), new Guid("c0000000-0000-0000-0000-000000000002"), null },
                    { new Guid("e0000000-0000-0000-0000-000000000008"), new Guid("d0000000-0000-0000-0000-000000000008"), new DateOnly(2025, 10, 1), 15, new DateOnly(1, 1, 1), new Guid("c0000000-0000-0000-0000-000000000003"), null },
                    { new Guid("e0000000-0000-0000-0000-000000000009"), new Guid("d0000000-0000-0000-0000-000000000009"), new DateOnly(2025, 10, 3), 14, new DateOnly(1, 1, 1), new Guid("c0000000-0000-0000-0000-000000000004"), null },
                    { new Guid("e0000000-0000-0000-0000-000000000010"), new Guid("d0000000-0000-0000-0000-000000000010"), new DateOnly(2025, 10, 5), 21, new DateOnly(1, 1, 1), new Guid("c0000000-0000-0000-0000-000000000005"), null },
                    { new Guid("e0000000-0000-0000-0000-000000000011"), new Guid("d0000000-0000-0000-0000-000000000001"), new DateOnly(2025, 9, 28), 30, new DateOnly(1, 1, 1), new Guid("c0000000-0000-0000-0000-000000000002"), null },
                    { new Guid("e0000000-0000-0000-0000-000000000012"), new Guid("d0000000-0000-0000-0000-000000000003"), new DateOnly(2025, 10, 7), 10, new DateOnly(1, 1, 1), new Guid("c0000000-0000-0000-0000-000000000004"), null },
                    { new Guid("e0000000-0000-0000-0000-000000000013"), new Guid("d0000000-0000-0000-0000-000000000005"), new DateOnly(2025, 10, 8), 14, new DateOnly(1, 1, 1), new Guid("c0000000-0000-0000-0000-000000000010"), null },
                    { new Guid("e0000000-0000-0000-0000-000000000014"), new Guid("d0000000-0000-0000-0000-000000000002"), new DateOnly(2025, 10, 9), 30, new DateOnly(1, 1, 1), new Guid("c0000000-0000-0000-0000-000000000008"), null },
                    { new Guid("e0000000-0000-0000-0000-000000000016"), new Guid("d0000000-0000-0000-0000-000000000011"), new DateOnly(2025, 6, 1), 14, new DateOnly(1, 1, 1), new Guid("c0000000-0000-0000-0000-000000000010"), new DateOnly(2025, 6, 15) },
                    { new Guid("e0000000-0000-0000-0000-000000000017"), new Guid("d0000000-0000-0000-0000-000000000012"), new DateOnly(2025, 3, 1), 10, new DateOnly(1, 1, 1), new Guid("c0000000-0000-0000-0000-000000000003"), new DateOnly(2025, 3, 11) },
                    { new Guid("e0000000-0000-0000-0000-000000000018"), new Guid("d0000000-0000-0000-0000-000000000013"), new DateOnly(2025, 4, 1), 21, new DateOnly(1, 1, 1), new Guid("c0000000-0000-0000-0000-000000000001"), new DateOnly(2025, 4, 22) },
                    { new Guid("e0000000-0000-0000-0000-000000000019"), new Guid("d0000000-0000-0000-0000-000000000014"), new DateOnly(2025, 5, 5), 10, new DateOnly(1, 1, 1), new Guid("c0000000-0000-0000-0000-000000000007"), new DateOnly(2025, 5, 15) },
                    { new Guid("e0000000-0000-0000-0000-000000000020"), new Guid("d0000000-0000-0000-0000-000000000015"), new DateOnly(2025, 6, 5), 14, new DateOnly(1, 1, 1), new Guid("c0000000-0000-0000-0000-000000000009"), new DateOnly(2025, 6, 19) }
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
