using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class RelationClassSubjProf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassDepartmentSubjectProfessors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassDepartmentID = table.Column<long>(type: "bigint", nullable: true),
                    SubjectID = table.Column<long>(type: "bigint", nullable: false),
                    UserProfessorId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Deleted = table.Column<short>(type: "smallint", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedById = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassDepartmentSubjectProfessors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassDepartmentSubjectProfessors_ClassDepartments_ClassDepartmentID",
                        column: x => x.ClassDepartmentID,
                        principalTable: "ClassDepartments",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ClassDepartmentSubjectProfessors_Users_UserProfessorId",
                        column: x => x.UserProfessorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassDepartmentSubjectProfessors_ClassDepartmentID",
                table: "ClassDepartmentSubjectProfessors",
                column: "ClassDepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassDepartmentSubjectProfessors_UserProfessorId",
                table: "ClassDepartmentSubjectProfessors",
                column: "UserProfessorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassDepartmentSubjectProfessors");
        }
    }
}
