using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TopeyPay.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreditCardNumber = table.Column<string>(type: "TEXT", unicode: false, maxLength: 40, nullable: false),
                    CardHolder = table.Column<string>(type: "TEXT", unicode: false, maxLength: 200, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SecurityCode = table.Column<string>(type: "TEXT", unicode: false, maxLength: 3, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentStatus",
                columns: table => new
                {
                    PaymentStatusId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PaymentId = table.Column<long>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStatus", x => x.PaymentStatusId);
                    table.ForeignKey(
                        name: "FK_PaymentStatus_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentStatus_PaymentId",
                table: "PaymentStatus",
                column: "PaymentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentStatus");

            migrationBuilder.DropTable(
                name: "Payment");
        }
    }
}
