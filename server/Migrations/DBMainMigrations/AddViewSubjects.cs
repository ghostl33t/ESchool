using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations.DBMainMigrations
{
    /// <inheritdoc />
    public partial class AddViewSubjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE VIEW Subjects AS 
            SELECT [Id]
                  ,[SerialNumber]
                  ,[Name]
                  ,[CreatedById]
                  ,[CreatedDate]
                  ,[Deleted]
                  ,[DeletedDate]
                  ,[DeletedById]
            FROM ESCHOOL_REGISTRIES_.[dbo].[Subjects]
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
