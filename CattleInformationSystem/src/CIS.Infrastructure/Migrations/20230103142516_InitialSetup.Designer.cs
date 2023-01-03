﻿// <auto-generated />
using CIS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CIS.Infrastructure.Migrations
{
    [DbContext(typeof(CattleInformationDatabaseContext))]
    [Migration("20230103142516_InitialSetup")]
    partial class InitialSetup
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CIS.Domain.Cow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("LifeNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LifeNumber")
                        .IsUnique();

                    b.ToTable("Cows");
                });

            modelBuilder.Entity("CIS.Domain.RawCowData", b =>
                {
                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LifeNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("RawCowData");
                });
#pragma warning restore 612, 618
        }
    }
}
