﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bakalaurinis.Infrastructure.Database;

namespace bakalaurinis.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200423202946_UpdateMessageTemplates")]
    partial class UpdateMessageTemplates
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("bakalaurinis.Infrastructure.Database.Models.Invitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("InvitationStatus")
                        .HasColumnType("int");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<int>("WorkId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.HasIndex("WorkId");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("bakalaurinis.Infrastructure.Database.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("bakalaurinis.Infrastructure.Database.Models.MessageTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TextTemplate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleTemplate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MessageTemplates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TextTemplate = "You created new work [work]!",
                            TitleTemplate = "Work created"
                        },
                        new
                        {
                            Id = 2,
                            TextTemplate = "You deleted work [work]!",
                            TitleTemplate = "Work deleted"
                        },
                        new
                        {
                            Id = 3,
                            TextTemplate = "The system performed a new schedule generation!",
                            TitleTemplate = "Schedule generation complete"
                        },
                        new
                        {
                            Id = 4,
                            TextTemplate = "Vartotojas [user] pakvietė jus i veiką [work]!",
                            TitleTemplate = "New invitation received"
                        },
                        new
                        {
                            Id = 5,
                            TextTemplate = "Vartotojas [user] atmetė jūsų pakvietimą į renginį [work]!",
                            TitleTemplate = "Invitation declined"
                        },
                        new
                        {
                            Id = 6,
                            TextTemplate = "Vartotojas [user] priėmė jūsų pakvietimą į renginį [work]!",
                            TitleTemplate = "Invitation accepted"
                        },
                        new
                        {
                            Id = 7,
                            TextTemplate = "Jūs atmetėte kvietimą vartotojo [user] pakvietimą į renginį [work]!",
                            TitleTemplate = "You have declined invitation"
                        },
                        new
                        {
                            Id = 8,
                            TextTemplate = "You have accepted [user]'s invitation to work [work]!",
                            TitleTemplate = "You have accepted invitation"
                        },
                        new
                        {
                            Id = 9,
                            TextTemplate = "Jūs pakvietėtę [user] į veiką [work]!",
                            TitleTemplate = "Invitation sent"
                        });
                });

            modelBuilder.Entity("bakalaurinis.Infrastructure.Database.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ScheduleStatus")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("bakalaurinis.Infrastructure.Database.Models.UserSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EndTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(22);

                    b.Property<int>("ItemsPerPage")
                        .HasColumnType("int");

                    b.Property<int>("StartTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(8);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("bakalaurinis.Infrastructure.Database.Models.Work", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActivityPriority")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("DurationInMinutes")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAuthor")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("WillBeParticipant")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Works");
                });

            modelBuilder.Entity("bakalaurinis.Infrastructure.Database.Models.Invitation", b =>
                {
                    b.HasOne("bakalaurinis.Infrastructure.Database.Models.User", "Sender")
                        .WithMany("Invitations")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("bakalaurinis.Infrastructure.Database.Models.Work", null)
                        .WithMany("Invitations")
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("bakalaurinis.Infrastructure.Database.Models.Message", b =>
                {
                    b.HasOne("bakalaurinis.Infrastructure.Database.Models.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("bakalaurinis.Infrastructure.Database.Models.UserSettings", b =>
                {
                    b.HasOne("bakalaurinis.Infrastructure.Database.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("bakalaurinis.Infrastructure.Database.Models.Work", b =>
                {
                    b.HasOne("bakalaurinis.Infrastructure.Database.Models.User", "User")
                        .WithMany("Works")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
