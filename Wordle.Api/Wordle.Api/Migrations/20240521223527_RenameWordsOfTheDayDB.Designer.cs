﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wordle.Api.Models;

#nullable disable

namespace Wordle.Api.Migrations
{
    [DbContext(typeof(WordleDbContext))]
    [Migration("20240521223527_RenameWordsOfTheDayDB")]
    partial class RenameWordsOfTheDayDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Wordle.Api.Models.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GameId"));

                    b.Property<int>("Attempts")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAttempted")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsWin")
                        .HasColumnType("bit");

                    b.Property<double>("Time")
                        .HasColumnType("float");

                    b.Property<int>("WordId")
                        .HasColumnType("int");

                    b.Property<int?>("WordOfTheDayId")
                        .HasColumnType("int");

                    b.HasKey("GameId");

                    b.HasIndex("WordId");

                    b.HasIndex("WordOfTheDayId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Wordle.Api.Models.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlayerId"));

                    b.Property<double>("AverageAttempts")
                        .HasColumnType("float");

                    b.Property<double>("AverageSecondsPerGame")
                        .HasColumnType("float");

                    b.Property<int>("GameCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlayerId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Wordle.Api.Models.Word", b =>
                {
                    b.Property<int>("WordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WordId"));

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WordId");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("Wordle.Api.Models.WordOfTheDay", b =>
                {
                    b.Property<int>("WordOfTheDayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WordOfTheDayId"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("WordId")
                        .HasColumnType("int");

                    b.HasKey("WordOfTheDayId");

                    b.HasIndex("WordId");

                    b.ToTable("WordsOfTheDays");
                });

            modelBuilder.Entity("Wordle.Api.Models.Game", b =>
                {
                    b.HasOne("Wordle.Api.Models.Word", "Word")
                        .WithMany("Games")
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wordle.Api.Models.WordOfTheDay", "WordOfTheDay")
                        .WithMany("Games")
                        .HasForeignKey("WordOfTheDayId");

                    b.Navigation("Word");

                    b.Navigation("WordOfTheDay");
                });

            modelBuilder.Entity("Wordle.Api.Models.WordOfTheDay", b =>
                {
                    b.HasOne("Wordle.Api.Models.Word", "Word")
                        .WithMany("WordsOfTheDays")
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Word");
                });

            modelBuilder.Entity("Wordle.Api.Models.Word", b =>
                {
                    b.Navigation("Games");

                    b.Navigation("WordsOfTheDays");
                });

            modelBuilder.Entity("Wordle.Api.Models.WordOfTheDay", b =>
                {
                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
