using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalWRP.Migrations
{
    /// <inheritdoc />
    public partial class addedTranslationsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "TenantDetails",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FromPhrase = table.Column<string>(type: "TEXT", nullable: true),
                    ToPhrase = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDateUTC = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FromLanguage = table.Column<string>(type: "TEXT", nullable: true),
                    ToLanguage = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "TenantDetails");
        }
    }
}
