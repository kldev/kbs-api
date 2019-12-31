using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KBS.Data.ConsoleApp.Migrations {
    public partial class Initial : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable (
                name: "books",
                columns: table => new {
                    id = table.Column<Guid> (nullable: false),
                    title = table.Column<string> (maxLength: 1024, nullable: true),
                    author = table.Column<string> (maxLength: 500, nullable: true),
                    price = table.Column<decimal> (nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey ("pk_books", x => x.id);
                });

            migrationBuilder.CreateTable (
                name: "books_sold",
                columns: table => new {
                    id = table.Column<Guid> (nullable: false),
                    user_id = table.Column<Guid> (nullable: false),
                    title = table.Column<string> (maxLength: 1024, nullable: true),
                    author = table.Column<string> (maxLength: 500, nullable: true),
                    price = table.Column<decimal> (nullable: false),
                    sold_date = table.Column<DateTime> (nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey ("pk_books_sold", x => x.id);
                });

            migrationBuilder.CreateTable (
                name: "user_profiles",
                columns: table => new {
                    id = table.Column<Guid> (nullable: false),
                    user_id = table.Column<Guid> (nullable: false),
                    email = table.Column<string> (maxLength: 200, nullable: true),
                    phone = table.Column<string> (maxLength: 32, nullable: true),
                    address = table.Column<string> (maxLength: 500, nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey ("pk_user_profiles", x => x.id);
                });

            migrationBuilder.CreateTable (
                name: "users",
                columns: table => new {
                    id = table.Column<Guid> (nullable: false),
                    username = table.Column<string> (maxLength: 500, nullable: true),
                    password = table.Column<string> (maxLength: 100, nullable: false),
                    role = table.Column<int> (nullable: false),
                    is_deleted = table.Column<int> (nullable: false, defaultValue: 0)
                },
                constraints: table => {
                    table.PrimaryKey ("pk_users", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable (
                name: "books");

            migrationBuilder.DropTable (
                name: "books_sold");

            migrationBuilder.DropTable (
                name: "user_profiles");

            migrationBuilder.DropTable (
                name: "users");
        }
    }
}
