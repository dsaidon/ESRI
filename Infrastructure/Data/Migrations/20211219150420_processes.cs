using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class processes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Process",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lastLoginId = table.Column<long>(type: "bigint", nullable: true),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    step = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: true),
                    lastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionGuid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    callNumber = table.Column<int>(type: "int", nullable: true),
                    execNumber = table.Column<int>(type: "int", nullable: true),
                    SrvManNo = table.Column<int>(type: "int", nullable: true),
                    IsSupplier = table.Column<bool>(type: "bit", nullable: true),
                    SrvManNM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SrvManPhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SbcCarNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    confirmation_isUserApproveCondiotions = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Process", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StepType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepType", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Process");

            migrationBuilder.DropTable(
                name: "StepType");
        }
    }
}
