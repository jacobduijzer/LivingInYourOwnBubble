﻿// <auto-generated />
using System;
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
    [Migration("20230118074028_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CIS.Domain.AnimalCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AgeInDays")
                        .HasColumnType("integer");

                    b.Property<int>("AgeInMonths")
                        .HasColumnType("integer");

                    b.Property<int>("AgeInYears")
                        .HasColumnType("integer");

                    b.Property<bool>("Calved")
                        .HasColumnType("boolean");

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<int>("ProductionTarget")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("AnimalCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AgeInDays = 0,
                            AgeInMonths = 0,
                            AgeInYears = 0,
                            Calved = true,
                            Category = 100,
                            Gender = 0,
                            ProductionTarget = 1
                        },
                        new
                        {
                            Id = 2,
                            AgeInDays = 0,
                            AgeInMonths = 0,
                            AgeInYears = 0,
                            Calved = false,
                            Category = 101,
                            Gender = 0,
                            ProductionTarget = 1
                        },
                        new
                        {
                            Id = 3,
                            AgeInDays = 0,
                            AgeInMonths = 0,
                            AgeInYears = 0,
                            Calved = false,
                            Category = 101,
                            Gender = 1,
                            ProductionTarget = 1
                        },
                        new
                        {
                            Id = 4,
                            AgeInDays = 0,
                            AgeInMonths = 0,
                            AgeInYears = 0,
                            Calved = false,
                            Category = 101,
                            Gender = 0,
                            ProductionTarget = 0
                        },
                        new
                        {
                            Id = 5,
                            AgeInDays = 0,
                            AgeInMonths = 0,
                            AgeInYears = 1,
                            Calved = false,
                            Category = 102,
                            Gender = 0,
                            ProductionTarget = 1
                        },
                        new
                        {
                            Id = 6,
                            AgeInDays = 0,
                            AgeInMonths = 0,
                            AgeInYears = 1,
                            Calved = false,
                            Category = 102,
                            Gender = 0,
                            ProductionTarget = 0
                        },
                        new
                        {
                            Id = 7,
                            AgeInDays = 0,
                            AgeInMonths = 0,
                            AgeInYears = 1,
                            Calved = false,
                            Category = 104,
                            Gender = 1,
                            ProductionTarget = 2
                        },
                        new
                        {
                            Id = 8,
                            AgeInDays = 14,
                            AgeInMonths = 0,
                            AgeInYears = 0,
                            Calved = false,
                            Category = 112,
                            Gender = 1,
                            ProductionTarget = 0
                        },
                        new
                        {
                            Id = 9,
                            AgeInDays = 14,
                            AgeInMonths = 0,
                            AgeInYears = 0,
                            Calved = false,
                            Category = 112,
                            Gender = 0,
                            ProductionTarget = 0
                        },
                        new
                        {
                            Id = 10,
                            AgeInDays = 0,
                            AgeInMonths = 8,
                            AgeInYears = 0,
                            Calved = false,
                            Category = 116,
                            Gender = 1,
                            ProductionTarget = 0
                        },
                        new
                        {
                            Id = 11,
                            AgeInDays = 0,
                            AgeInMonths = 8,
                            AgeInYears = 0,
                            Calved = false,
                            Category = 116,
                            Gender = 0,
                            ProductionTarget = 0
                        });
                });

            modelBuilder.Entity("CIS.Domain.Cow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateCalved")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateOfDeath")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("LifeNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LifeNumberOfMother")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LifeNumber")
                        .IsUnique();

                    b.ToTable("Cows");
                });

            modelBuilder.Entity("CIS.Domain.CowEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AnimalCategoryId")
                        .HasColumnType("integer");

                    b.Property<int>("CowId")
                        .HasColumnType("integer");

                    b.Property<int?>("FarmLocationId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("OccuredAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<int>("Reason")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AnimalCategoryId");

                    b.HasIndex("CowId");

                    b.HasIndex("FarmLocationId");

                    b.ToTable("CowEvent");
                });

            modelBuilder.Entity("CIS.Domain.FarmLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("LocationNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProductionTarget")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("FarmLocations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            LocationNumber = "00000001",
                            ProductionTarget = 2
                        },
                        new
                        {
                            Id = 2,
                            LocationNumber = "00000002",
                            ProductionTarget = 1
                        },
                        new
                        {
                            Id = 3,
                            LocationNumber = "00000003",
                            ProductionTarget = 1
                        });
                });

            modelBuilder.Entity("CIS.Domain.RawCowData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateCalved")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateOfDeath")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("LifeNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LifeNumberOfMother")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LifeNumber")
                        .IsUnique();

                    b.ToTable("RawCowData");
                });

            modelBuilder.Entity("CIS.Domain.RawCowEventData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("LocationNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("OccuredAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RawCowDataId")
                        .HasColumnType("integer");

                    b.Property<int>("Reason")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RawCowDataId");

                    b.ToTable("RawCowEventData");
                });

            modelBuilder.Entity("CIS.Domain.CowEvent", b =>
                {
                    b.HasOne("CIS.Domain.AnimalCategory", "AnimalCategory")
                        .WithMany()
                        .HasForeignKey("AnimalCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CIS.Domain.Cow", "Cow")
                        .WithMany("CowEvents")
                        .HasForeignKey("CowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CIS.Domain.FarmLocation", "FarmLocation")
                        .WithMany()
                        .HasForeignKey("FarmLocationId");

                    b.Navigation("AnimalCategory");

                    b.Navigation("Cow");

                    b.Navigation("FarmLocation");
                });

            modelBuilder.Entity("CIS.Domain.RawCowEventData", b =>
                {
                    b.HasOne("CIS.Domain.RawCowData", "RawCowData")
                        .WithMany("Events")
                        .HasForeignKey("RawCowDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RawCowData");
                });

            modelBuilder.Entity("CIS.Domain.Cow", b =>
                {
                    b.Navigation("CowEvents");
                });

            modelBuilder.Entity("CIS.Domain.RawCowData", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
