using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalWRP.Migrations
{
    /// <inheritdoc />
    public partial class init_tenantDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TenantDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppKey = table.Column<string>(type: "TEXT", nullable: true),
                    AccessKey = table.Column<string>(type: "TEXT", nullable: true),
                    SecretKey = table.Column<string>(type: "TEXT", nullable: true),
                    TenantName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantDetails", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenantDetails");
        }
    }
}
