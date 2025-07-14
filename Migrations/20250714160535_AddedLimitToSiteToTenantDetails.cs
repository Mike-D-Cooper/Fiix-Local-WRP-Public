using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalWRP.Migrations
{
    /// <inheritdoc />
    public partial class AddedLimitToSiteToTenantDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LimitToSite",
                table: "TenantDetails",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LimitToSite",
                table: "TenantDetails");
        }
    }
}
