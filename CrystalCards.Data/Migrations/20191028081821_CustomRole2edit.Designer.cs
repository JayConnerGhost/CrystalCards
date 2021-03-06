﻿// <auto-generated />
using System;
using CrystalCards.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CrystalCards.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191028081821_CustomRole2edit")]
    partial class CustomRole2edit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CrystalCards.Models.ActionPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CardId");

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("ActionPoint");
                });

            modelBuilder.Entity("CrystalCards.Models.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int>("Order");

                    b.Property<string>("Title");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("CrystalCards.Models.CustomRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CustomRole");
                });

            modelBuilder.Entity("CrystalCards.Models.Link", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CardId");

                    b.Property<string>("Description");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("Link");
                });

            modelBuilder.Entity("CrystalCards.Models.NPPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CardId");

                    b.Property<string>("Description");

                    b.Property<int>("Direction");

                    b.Property<int>("Order");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("NPPoint");
                });

            modelBuilder.Entity("CrystalCards.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CrystalCards.Models.ActionPoint", b =>
                {
                    b.HasOne("CrystalCards.Models.Card")
                        .WithMany("ActionPoints")
                        .HasForeignKey("CardId");
                });

            modelBuilder.Entity("CrystalCards.Models.Card", b =>
                {
                    b.HasOne("CrystalCards.Models.User")
                        .WithMany("Cards")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CrystalCards.Models.CustomRole", b =>
                {
                    b.HasOne("CrystalCards.Models.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CrystalCards.Models.Link", b =>
                {
                    b.HasOne("CrystalCards.Models.Card")
                        .WithMany("Links")
                        .HasForeignKey("CardId");
                });

            modelBuilder.Entity("CrystalCards.Models.NPPoint", b =>
                {
                    b.HasOne("CrystalCards.Models.Card")
                        .WithMany("Points")
                        .HasForeignKey("CardId");
                });
#pragma warning restore 612, 618
        }
    }
}
