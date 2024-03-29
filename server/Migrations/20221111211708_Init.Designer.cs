﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using server.Database;

#nullable disable

namespace server.Migrations
{
    [DbContext(typeof(DBMain))]
    [Migration("20221111211708_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("server.Models.Domain.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<short?>("Deleted")
                        .HasColumnType("smallint");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("LastName")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar");

                    b.Property<string>("Name")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar");

                    b.Property<string>("OIB")
                        .HasMaxLength(13)
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

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
