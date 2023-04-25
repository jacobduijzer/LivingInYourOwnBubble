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
    [Migration("20230425121311_AddExtraParametersToRelation")]
    partial class AddExtraParametersToRelation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CattleInformationSystem.Domain.Cow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

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
                    b.Property<int>("FarmId")
                        .HasColumnType("integer");

                    b.Property<int>("CowId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("FarmId", "CowId");

                    b.HasIndex("CowId");

                    b.ToTable("FarmCows");
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
