using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PensionManagementPensionerService.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PensionPlanDetails",
                columns: table => new
                {
                    PensionPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PensionPlanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PensionDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PensionPlanDetails", x => x.PensionPlanId);
                });

            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "PensionerDetails",
                columns: table => new
                {
                    PensionerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AadharNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PensionPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PensionerDetails", x => x.PensionerId);
                    table.ForeignKey(
                        name: "FK_PensionerDetails_PensionPlanDetails_PensionPlanId",
                        column: x => x.PensionPlanId,
                        principalTable: "PensionPlanDetails",
                        principalColumn: "PensionPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PensionerDetails_UserDetails_UserId",
                        column: x => x.UserId,
                        principalTable: "UserDetails",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuardianDetails",
                columns: table => new
                {
                    GuardianId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GuardianName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Relation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PensionerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuardianDetails", x => x.GuardianId);
                    table.ForeignKey(
                        name: "FK_GuardianDetails_PensionerDetails_PensionerId",
                        column: x => x.PensionerId,
                        principalTable: "PensionerDetails",
                        principalColumn: "PensionerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuardianDetails_PensionerId",
                table: "GuardianDetails",
                column: "PensionerId");

            migrationBuilder.CreateIndex(
                name: "IX_PensionerDetails_PensionPlanId",
                table: "PensionerDetails",
                column: "PensionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PensionerDetails_UserId",
                table: "PensionerDetails",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuardianDetails");

            migrationBuilder.DropTable(
                name: "PensionerDetails");

            migrationBuilder.DropTable(
                name: "PensionPlanDetails");

            migrationBuilder.DropTable(
                name: "UserDetails");
        }
    }
}
