using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class CreateStudentsDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentsDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<long>(type: "bigint", nullable: true),
                    ClassDepartmentID = table.Column<long>(type: "bigint", nullable: true),
                    StudentDiscipline = table.Column<short>(type: "smallint", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Deleted = table.Column<short>(type: "smallint", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedById = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentsDetails_ClassDepartments_ClassDepartmentID",
                        column: x => x.ClassDepartmentID,
                        principalTable: "ClassDepartments",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StudentsDetails_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsDetails_ClassDepartmentID",
                table: "StudentsDetails",
                column: "ClassDepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsDetails_StudentId",
                table: "StudentsDetails",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsDetails");
        }
    }
}
