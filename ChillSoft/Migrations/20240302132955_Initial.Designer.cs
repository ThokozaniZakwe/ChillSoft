﻿// <auto-generated />
using ChillSoft.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChillSoft.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240302132955_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ChillSoft.Models.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("MeetingNumber")
                        .HasColumnType("int");

                    b.Property<int>("MeetingTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MeetingTypeId");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("ChillSoft.Models.MeetingItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("MeetingItems");
                });

            modelBuilder.Entity("ChillSoft.Models.MeetingItemStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("MeetingId")
                        .HasColumnType("int");

                    b.Property<int>("MeetingItemId")
                        .HasColumnType("int");

                    b.Property<string>("ResponsiblePerson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MeetingId");

                    b.HasIndex("MeetingItemId");

                    b.ToTable("MeetingItemStatuses");
                });

            modelBuilder.Entity("ChillSoft.Models.MeetingType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("MeetingTypes");
                });

            modelBuilder.Entity("ChillSoft.Models.Meeting", b =>
                {
                    b.HasOne("ChillSoft.Models.MeetingType", "MeetingType")
                        .WithMany()
                        .HasForeignKey("MeetingTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MeetingType");
                });

            modelBuilder.Entity("ChillSoft.Models.MeetingItemStatus", b =>
                {
                    b.HasOne("ChillSoft.Models.Meeting", "Meeting")
                        .WithMany("MeetingItemStatuses")
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChillSoft.Models.MeetingItem", "MeetingItem")
                        .WithMany("MeetingItemStatuses")
                        .HasForeignKey("MeetingItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meeting");

                    b.Navigation("MeetingItem");
                });

            modelBuilder.Entity("ChillSoft.Models.Meeting", b =>
                {
                    b.Navigation("MeetingItemStatuses");
                });

            modelBuilder.Entity("ChillSoft.Models.MeetingItem", b =>
                {
                    b.Navigation("MeetingItemStatuses");
                });
#pragma warning restore 612, 618
        }
    }
}
