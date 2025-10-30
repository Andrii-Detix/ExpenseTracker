using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyEntitiesAndRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "default_currency_id",
                table: "users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "currency_id",
                table: "expense_records",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "currencies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character(3)", fixedLength: true, maxLength: 3, nullable: false),
                    name = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currencies", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_default_currency_id",
                table: "users",
                column: "default_currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_expense_records_currency_id",
                table: "expense_records",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_currencies_code",
                table: "currencies",
                column: "code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_expense_records_currencies_currency_id",
                table: "expense_records",
                column: "currency_id",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_currencies_default_currency_id",
                table: "users",
                column: "default_currency_id",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_expense_records_currencies_currency_id",
                table: "expense_records");

            migrationBuilder.DropForeignKey(
                name: "FK_users_currencies_default_currency_id",
                table: "users");

            migrationBuilder.DropTable(
                name: "currencies");

            migrationBuilder.DropIndex(
                name: "IX_users_default_currency_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_expense_records_currency_id",
                table: "expense_records");

            migrationBuilder.DropColumn(
                name: "default_currency_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "currency_id",
                table: "expense_records");
        }
    }
}
