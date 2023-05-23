﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project_Appointments.Contexts;

#nullable disable

namespace Project_Appointments.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230521165514_setup")]
    partial class setup
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Project_Appointments.Models.Appointment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<string>("PatientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ScheduleId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Appointments");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Description = "Cleaning",
                            End = new DateTime(2023, 1, 2, 9, 15, 0, 0, DateTimeKind.Unspecified),
                            PatientName = "Bob Brown",
                            ScheduleId = 1L,
                            Start = new DateTime(2023, 1, 2, 9, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2L,
                            Description = "Cleaning",
                            End = new DateTime(2023, 1, 3, 9, 15, 0, 0, DateTimeKind.Unspecified),
                            PatientName = "Anna Red",
                            ScheduleId = 2L,
                            Start = new DateTime(2023, 1, 3, 9, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3L,
                            Description = "Treatment",
                            End = new DateTime(2023, 1, 4, 21, 15, 0, 0, DateTimeKind.Unspecified),
                            PatientName = "Peter Green",
                            ScheduleId = 3L,
                            Start = new DateTime(2023, 1, 4, 21, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4L,
                            Description = "Treatment",
                            End = new DateTime(2023, 1, 5, 21, 15, 0, 0, DateTimeKind.Unspecified),
                            PatientName = "Lara White",
                            ScheduleId = 4L,
                            Start = new DateTime(2023, 1, 5, 21, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Project_Appointments.Models.BreakTime", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("EndDay")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ScheduleId")
                        .HasColumnType("bigint");

                    b.Property<int>("StartDay")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("BreakTimes");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            EndDay = 1,
                            EndTime = new TimeSpan(0, 13, 0, 0, 0),
                            Name = "Lunch",
                            ScheduleId = 1L,
                            StartDay = 1,
                            StartTime = new TimeSpan(0, 12, 0, 0, 0)
                        },
                        new
                        {
                            Id = 2L,
                            EndDay = 2,
                            EndTime = new TimeSpan(0, 13, 0, 0, 0),
                            Name = "Lunch",
                            ScheduleId = 2L,
                            StartDay = 2,
                            StartTime = new TimeSpan(0, 12, 0, 0, 0)
                        },
                        new
                        {
                            Id = 3L,
                            EndDay = 4,
                            EndTime = new TimeSpan(0, 1, 0, 0, 0),
                            Name = "Dinner",
                            ScheduleId = 3L,
                            StartDay = 4,
                            StartTime = new TimeSpan(0, 0, 0, 0, 0)
                        },
                        new
                        {
                            Id = 4L,
                            EndDay = 5,
                            EndTime = new TimeSpan(0, 1, 0, 0, 0),
                            Name = "Dinner",
                            ScheduleId = 4L,
                            StartDay = 5,
                            StartTime = new TimeSpan(0, 0, 0, 0, 0)
                        });
                });

            modelBuilder.Entity("Project_Appointments.Models.Odontologist", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Odontologists");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Email = "john@blue.com",
                            Name = "John Blue",
                            Phone = "(011) 91111-1111"
                        },
                        new
                        {
                            Id = 2L,
                            Email = "maria@yellow.com",
                            Name = "Maria Yellow",
                            Phone = "(011) 92222-2222"
                        });
                });

            modelBuilder.Entity("Project_Appointments.Models.Schedule", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("EndDay")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("OdontologistId")
                        .HasColumnType("bigint");

                    b.Property<int>("StartDay")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("OdontologistId");

                    b.ToTable("Schedules");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            EndDay = 1,
                            EndTime = new TimeSpan(0, 18, 0, 0, 0),
                            Name = "Monday",
                            OdontologistId = 1L,
                            StartDay = 1,
                            StartTime = new TimeSpan(0, 9, 0, 0, 0)
                        },
                        new
                        {
                            Id = 2L,
                            EndDay = 2,
                            EndTime = new TimeSpan(0, 18, 0, 0, 0),
                            Name = "Tuesday",
                            OdontologistId = 1L,
                            StartDay = 2,
                            StartTime = new TimeSpan(0, 9, 0, 0, 0)
                        },
                        new
                        {
                            Id = 3L,
                            EndDay = 4,
                            EndTime = new TimeSpan(0, 6, 0, 0, 0),
                            Name = "Wednesday-Thursday",
                            OdontologistId = 2L,
                            StartDay = 3,
                            StartTime = new TimeSpan(0, 21, 0, 0, 0)
                        },
                        new
                        {
                            Id = 4L,
                            EndDay = 5,
                            EndTime = new TimeSpan(0, 6, 0, 0, 0),
                            Name = "Thursday-Friday",
                            OdontologistId = 2L,
                            StartDay = 4,
                            StartTime = new TimeSpan(0, 21, 0, 0, 0)
                        });
                });

            modelBuilder.Entity("Project_Appointments.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OdontologistId")
                        .HasColumnType("int");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Project_Appointments.Models.Appointment", b =>
                {
                    b.HasOne("Project_Appointments.Models.Schedule", null)
                        .WithMany()
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Project_Appointments.Models.BreakTime", b =>
                {
                    b.HasOne("Project_Appointments.Models.Schedule", null)
                        .WithMany()
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Project_Appointments.Models.Schedule", b =>
                {
                    b.HasOne("Project_Appointments.Models.Odontologist", null)
                        .WithMany()
                        .HasForeignKey("OdontologistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}