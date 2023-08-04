using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DomainLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlatRate = table.Column<double>(type: "float", nullable: false),
                    ReducingBalance = table.Column<double>(type: "float", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanCalculator",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoanType = table.Column<double>(type: "float", nullable: false),
                    LoanAmount = table.Column<double>(type: "float", nullable: false),
                    LoanPeriod = table.Column<double>(type: "float", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentFreq = table.Column<int>(type: "int", nullable: false),
                    ProcessingFee = table.Column<double>(type: "float", nullable: false),
                    ExcessDuty = table.Column<double>(type: "float", nullable: false),
                    LegalFees = table.Column<double>(type: "float", nullable: false),
                    MonthlyPayment = table.Column<double>(type: "float", nullable: false),
                    TakeHome = table.Column<double>(type: "float", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanCalculator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanScheduleViewModel",
                columns: table => new
                {
                    LoanId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoanType = table.Column<double>(type: "float", nullable: false),
                    LoanPeriod = table.Column<double>(type: "float", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoanAmount = table.Column<double>(type: "float", nullable: false),
                    PaymentFreq = table.Column<int>(type: "int", nullable: false),
                    Interest = table.Column<double>(type: "float", nullable: false),
                    PrincipalAmount = table.Column<double>(type: "float", nullable: false),
                    Installment = table.Column<double>(type: "float", nullable: false),
                    LoanBalance = table.Column<double>(type: "float", nullable: false),
                    LoansId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanScheduleViewModel", x => x.LoanId);
                    table.ForeignKey(
                        name: "FK_LoanScheduleViewModel_LoanCalculator_LoansId",
                        column: x => x.LoansId,
                        principalTable: "LoanCalculator",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Bank",
                columns: new[] { "Id", "FlatRate", "ModifiedDate", "Name", "ReducingBalance" },
                values: new object[,]
                {
                    { 1, 20.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bank A", 22.0 },
                    { 2, 18.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bank B", 25.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanScheduleViewModel_LoansId",
                table: "LoanScheduleViewModel",
                column: "LoansId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "LoanScheduleViewModel");

            migrationBuilder.DropTable(
                name: "LoanCalculator");
        }
    }
}
