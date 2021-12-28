﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PS.Notification.Infrastructure.Data;

namespace PS.Notification.Infrastructure.Migrations
{
    [DbContext(typeof(NotificationDbContext))]
    [Migration("20211227081359_Starter")]
    partial class Starter
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("PSN")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("PS.Notification.Domain.Entities.MsgMail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ApplicationName")
                        .HasColumnType("text");

                    b.Property<string>("Body")
                        .HasColumnType("text");

                    b.Property<string>("Cc")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValue(new DateTime(2021, 12, 27, 11, 13, 59, 381, DateTimeKind.Local).AddTicks(6417));

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("text");

                    b.Property<string>("ExternalId")
                        .HasColumnType("text");

                    b.Property<string>("From")
                        .HasColumnType("text");

                    b.Property<string>("FromDisplayName")
                        .HasColumnType("text");

                    b.Property<bool>("IsSend")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("SendTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Subject")
                        .HasColumnType("text");

                    b.Property<string>("To")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MsgMails");
                });

            modelBuilder.Entity("PS.Notification.Domain.Entities.MsgMailAttachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<byte[]>("Content")
                        .HasColumnType("bytea");

                    b.Property<int>("MailId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MailId");

                    b.ToTable("MsgMailAttachments");
                });

            modelBuilder.Entity("PS.Notification.Domain.Entities.MsgMailAttachment", b =>
                {
                    b.HasOne("PS.Notification.Domain.Entities.MsgMail", "Mail")
                        .WithMany("MailAttachments")
                        .HasForeignKey("MailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mail");
                });

            modelBuilder.Entity("PS.Notification.Domain.Entities.MsgMail", b =>
                {
                    b.Navigation("MailAttachments");
                });
#pragma warning restore 612, 618
        }
    }
}
