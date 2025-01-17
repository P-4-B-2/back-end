﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend.DAL.Data;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250116133059_testtest")]
    partial class testtest
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("backend.DAL.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ConversationId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Response")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConversationId = 1,
                            QuestionId = 1,
                            Response = "It's nice and peaceful."
                        },
                        new
                        {
                            Id = 2,
                            ConversationId = 1,
                            QuestionId = 2,
                            Response = "Maybe more greens, vegetation would be nice."
                        });
                });

            modelBuilder.Entity("backend.DAL.Models.Bench", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Benches");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Bench A"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Bench B"
                        });
                });

            modelBuilder.Entity("backend.DAL.Models.Conversation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BenchId")
                        .HasColumnType("int");

                    b.Property<DateTime>("end_datetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("sentiment")
                        .HasColumnType("int");

                    b.Property<DateTime>("start_datetime")
                        .HasColumnType("datetime2");

                    b.Property<string>("summary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BenchId");

                    b.ToTable("Conversations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BenchId = 1,
                            end_datetime = new DateTime(2025, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            sentiment = 1,
                            start_datetime = new DateTime(2025, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            summary = "First conversation summary"
                        },
                        new
                        {
                            Id = 2,
                            BenchId = 2,
                            end_datetime = new DateTime(2025, 1, 2, 11, 20, 0, 0, DateTimeKind.Unspecified),
                            sentiment = 0,
                            start_datetime = new DateTime(2025, 1, 2, 11, 0, 0, 0, DateTimeKind.Unspecified),
                            summary = "Second conversation summary"
                        });
                });

            modelBuilder.Entity("backend.DAL.Models.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BenchId")
                        .HasColumnType("int");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BenchId");

                    b.HasIndex("LocationId");

                    b.HasIndex("StatusId");

                    b.ToTable("Histories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BenchId = 1,
                            LocationId = 1,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 2,
                            BenchId = 2,
                            LocationId = 2,
                            StatusId = 2
                        });
                });

            modelBuilder.Entity("backend.DAL.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Latitude = "-74.005974",
                            Longitude = "40.712776"
                        },
                        new
                        {
                            Id = 2,
                            Latitude = "-118.243683",
                            Longitude = "34.052235"
                        });
                });

            modelBuilder.Entity("backend.DAL.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("is_active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("made_at")
                        .HasColumnType("datetime2");

                    b.Property<int>("order_number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Questions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Text = "What do you think about this neighbourhood?",
                            is_active = true,
                            made_at = new DateTime(2025, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            order_number = 1
                        },
                        new
                        {
                            Id = 2,
                            Text = "What new features do you think this village need?",
                            is_active = true,
                            made_at = new DateTime(2025, 1, 1, 9, 5, 0, 0, DateTimeKind.Unspecified),
                            order_number = 2
                        });
                });

            modelBuilder.Entity("backend.DAL.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Type = "Active"
                        },
                        new
                        {
                            Id = 2,
                            Type = "Inactive"
                        });
                });

            modelBuilder.Entity("backend.DAL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("First_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "john.doe@example.com",
                            First_name = "Doe",
                            Name = "John",
                            Password = "hashedpassword1"
                        },
                        new
                        {
                            Id = 2,
                            Email = "jane.smith@example.com",
                            First_name = "Smith",
                            Name = "Jane",
                            Password = "hashedpassword2"
                        });
                });

            modelBuilder.Entity("backend.DAL.Models.Answer", b =>
                {
                    b.HasOne("backend.DAL.Models.Conversation", "Conversation")
                        .WithMany("Answers")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.DAL.Models.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conversation");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("backend.DAL.Models.Conversation", b =>
                {
                    b.HasOne("backend.DAL.Models.Bench", "Bench")
                        .WithMany("Conversations")
                        .HasForeignKey("BenchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bench");
                });

            modelBuilder.Entity("backend.DAL.Models.History", b =>
                {
                    b.HasOne("backend.DAL.Models.Bench", "Bench")
                        .WithMany()
                        .HasForeignKey("BenchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.DAL.Models.Location", "Location")
                        .WithMany("Histories")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.DAL.Models.Status", "Status")
                        .WithMany("Histories")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bench");

                    b.Navigation("Location");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("backend.DAL.Models.Bench", b =>
                {
                    b.Navigation("Conversations");
                });

            modelBuilder.Entity("backend.DAL.Models.Conversation", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("backend.DAL.Models.Location", b =>
                {
                    b.Navigation("Histories");
                });

            modelBuilder.Entity("backend.DAL.Models.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("backend.DAL.Models.Status", b =>
                {
                    b.Navigation("Histories");
                });
#pragma warning restore 612, 618
        }
    }
}
