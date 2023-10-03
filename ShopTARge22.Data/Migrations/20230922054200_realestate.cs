using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopTARge22.Data.Migrations
{
    public partial class realestate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FileToApi",
                table: "FileToApi");

            migrationBuilder.RenameTable(
                name: "FileToApi",
                newName: "FileToApis");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileToApis",
                table: "FileToApis",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RealEstates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SizeSqrM = table.Column<float>(type: "real", nullable: false),
                    RoomCount = table.Column<int>(type: "int", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    BuildingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuiltInYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstates", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RealEstates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileToApis",
                table: "FileToApis");

            migrationBuilder.RenameTable(
                name: "FileToApis",
                newName: "FileToApi");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileToApi",
                table: "FileToApi",
                column: "Id");
        }
    }
}
