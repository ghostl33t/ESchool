﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using server.Database;

#nullable disable

namespace server.Migrations
{
    [DbContext(typeof(DBMain))]
    partial class DBMainModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("server.Models.Domain.ClassDepartment", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<short?>("Deleted")
                        .HasColumnType("smallint");

                    b.Property<long>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LeaderProfessorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar");

                    b.Property<long>("SchoolListId")
                        .HasColumnType("bigint");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CreatedById");

                    b.HasIndex("LeaderProfessorId");

                    b.ToTable("ClassDepartments");
                });

            modelBuilder.Entity("server.Models.Domain.ClassDepartmentSubjectProfessor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("ClassDepartmentID")
                        .HasColumnType("bigint");

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<short>("Deleted")
                        .HasColumnType("smallint");

                    b.Property<long>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime");

                    b.Property<long>("SubjectID")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserProfessorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ClassDepartmentID");

                    b.HasIndex("UserProfessorId");

                    b.ToTable("ClassDepartmentSubjectProfessors");
                });

            modelBuilder.Entity("server.Models.Domain.StudentDetails", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("ClassDepartmentID")
                        .HasColumnType("bigint");

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<short>("Deleted")
                        .HasColumnType("smallint");

                    b.Property<long>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime");

                    b.Property<short?>("StudentDiscipline")
                        .HasColumnType("smallint");

                    b.Property<long?>("StudentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ClassDepartmentID");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentsDetails");
                });

            modelBuilder.Entity("server.Models.Domain.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<short?>("Deleted")
                        .HasColumnType("smallint");

                    b.Property<long>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar");

                    b.Property<string>("Name")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar");

                    b.Property<string>("OIB")
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar");

                    b.Property<string>("Password")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar");

                    b.Property<string>("Phone")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar");

                    b.Property<string>("UserName")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar");

                    b.Property<short>("UserType")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("server.Models.Domain.ClassDepartment", b =>
                {
                    b.HasOne("server.Models.Domain.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("server.Models.Domain.User", "LeaderProfessor")
                        .WithMany()
                        .HasForeignKey("LeaderProfessorId");

                    b.Navigation("CreatedBy");

                    b.Navigation("LeaderProfessor");
                });

            modelBuilder.Entity("server.Models.Domain.ClassDepartmentSubjectProfessor", b =>
                {
                    b.HasOne("server.Models.Domain.ClassDepartment", "ClassDepartment")
                        .WithMany()
                        .HasForeignKey("ClassDepartmentID");

                    b.HasOne("server.Models.Domain.User", "UserProfessor")
                        .WithMany()
                        .HasForeignKey("UserProfessorId");

                    b.Navigation("ClassDepartment");

                    b.Navigation("UserProfessor");
                });

            modelBuilder.Entity("server.Models.Domain.StudentDetails", b =>
                {
                    b.HasOne("server.Models.Domain.ClassDepartment", "ClassDepartment")
                        .WithMany()
                        .HasForeignKey("ClassDepartmentID");

                    b.HasOne("server.Models.Domain.User", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");

                    b.Navigation("ClassDepartment");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("server.Models.Domain.User", b =>
                {
                    b.HasOne("server.Models.Domain.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.Navigation("CreatedBy");
                });
#pragma warning restore 612, 618
        }
    }
}
