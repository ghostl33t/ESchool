using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations.DBRegistriesMigrations
{
    /// <inheritdoc />
    public partial class FixUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchoolType",
                table: "Subjects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "SchoolType",
                table: "Subjects",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
