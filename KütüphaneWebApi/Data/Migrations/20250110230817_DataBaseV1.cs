using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KütüphaneWebApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class DataBaseV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Destek",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TelefonNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kitap",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    kitapAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kitapYazari = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kitapYazimTarihi = table.Column<DateOnly>(type: "date", nullable: false),
                    kitapImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kitapAlimTarihi = table.Column<DateOnly>(type: "date", nullable: false),
                    aktiflik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    basımNumarasi = table.Column<int>(type: "int", nullable: false),
                    alanKişiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kitap", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Destek");

            migrationBuilder.DropTable(
                name: "Kitap");
        }
    }
}
