using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class CreateGrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentsGrades",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<long>(type: "bigint", nullable: true),
                    Grade = table.Column<short>(type: "smallint", nullable: false),
                    ClassDepartmentSubjectProfessorId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Validated = table.Column<short>(type: "smallint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Deleted = table.Column<short>(type: "smallint", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedById = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsGrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentsGrades_ClassDepartmentSubjectProfessors_ClassDepartmentSubjectProfessorId",
                        column: x => x.ClassDepartmentSubjectProfessorId,
                        principalTable: "ClassDepartmentSubjectProfessors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentsGrades_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsGrades_ClassDepartmentSubjectProfessorId",
                table: "StudentsGrades",
                column: "ClassDepartmentSubjectProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsGrades_StudentId",
                table: "StudentsGrades",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsGrades");
        }
    }
}
