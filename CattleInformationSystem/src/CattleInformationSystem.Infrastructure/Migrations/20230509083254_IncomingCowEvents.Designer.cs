﻿// <auto-generated />
using System;
using CattleInformationSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CattleInformationSystem.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230509083254_IncomingCowEvents")]
    partial class IncomingCowEvents
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CattleInformationSystem.Domain.AnimalCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AgeInDays")
                        .HasColumnType("integer");

                    b.Property<bool>("Calved")
                        .HasColumnType("boolean");

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<int>("FarmType")
                        .HasColumnType("integer");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("AnimalCategories");
                });

            modelBuilder.Entity("CattleInformationSystem.Domain.Cow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateFirstCalved")
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

                    b.HasKey("Id");

                    b.HasIndex("Id", "LifeNumber")
                        .IsUnique();

                    b.ToTable("Cows");
                });

            modelBuilder.Entity("CattleInformationSystem.Domain.CowEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<int>("CowId")
                        .HasColumnType("integer");

                    b.Property<int>("FarmId")
                        .HasColumnType("integer");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<int>("Reason")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CowId");

                    b.HasIndex("FarmId");

                    b.ToTable("CowEvents");
                });

            modelBuilder.Entity("CattleInformationSystem.Domain.Farm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FarmType")
                        .HasColumnType("integer");

                    b.Property<string>("UBN")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UBN")
                        .IsUnique();

                    b.ToTable("Farms");
                });

            modelBuilder.Entity("CattleInformationSystem.Domain.FarmCow", b =>
                {
                    b.Property<int>("CowId")
                        .HasColumnType("integer");

                    b.Property<int>("FarmId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("CowId", "FarmId");

                    b.HasIndex("FarmId");

                    b.ToTable("FarmCows");
                });

            modelBuilder.Entity("CattleInformationSystem.Domain.IncomingCowEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("LifeNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Reason")
                        .HasColumnType("integer");

                    b.Property<string>("UBN_1")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UBN_2")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("IncomingCowEvents");
                });

            modelBuilder.Entity("CattleInformationSystem.Domain.CowEvent", b =>
                {
                    b.HasOne("CattleInformationSystem.Domain.Cow", "Cow")
                        .WithMany()
                        .HasForeignKey("CowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CattleInformationSystem.Domain.Farm", "Farm")
                        .WithMany()
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cow");

                    b.Navigation("Farm");
                });

            modelBuilder.Entity("CattleInformationSystem.Domain.FarmCow", b =>
                {
                    b.HasOne("CattleInformationSystem.Domain.Cow", "Cow")
                        .WithMany("FarmCows")
                        .HasForeignKey("CowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CattleInformationSystem.Domain.Farm", "Farm")
                        .WithMany("FarmCows")
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cow");

                    b.Navigation("Farm");
                });

            modelBuilder.Entity("CattleInformationSystem.Domain.Cow", b =>
                {
                    b.Navigation("FarmCows");
                });

            modelBuilder.Entity("CattleInformationSystem.Domain.Farm", b =>
                {
                    b.Navigation("FarmCows");
                });
#pragma warning restore 612, 618
        }
    }
}
