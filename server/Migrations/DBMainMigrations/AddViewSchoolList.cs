using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations.DBMainMigrations
{
    /// <inheritdoc />
    public partial class AddViewSchoolList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE VIEW SchoolList AS
            SELECT [Id]
                  ,[SerialNumber]
                  ,[Name]
                  ,[SchoolType]
                  ,[CreatedById]
                  ,[CreatedDate]
                  ,[Deleted]
                  ,[DeletedDate]
                  ,[DeletedById]
            FROM ESCHOOL_REGISTRIES_.[dbo].[SchoolList] ;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
