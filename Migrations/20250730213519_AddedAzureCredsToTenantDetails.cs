using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalWRP.Migrations
{
    /// <inheritdoc />
    public partial class AddedAzureCredsToTenantDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AzureTranslatorKey",
                table: "TenantDetails",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AzureTranslatorRegion",
                table: "TenantDetails",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AzureTranslatorKey",
                table: "TenantDetails");

            migrationBuilder.DropColumn(
                name: "AzureTranslatorRegion",
                table: "TenantDetails");
        }
    }
}
