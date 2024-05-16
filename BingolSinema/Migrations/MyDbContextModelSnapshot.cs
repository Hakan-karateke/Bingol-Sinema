﻿// <auto-generated />
using System;
using BingolSinema.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BingolSinema.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BingolSinema.Models.Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminID"));

                    b.Property<string>("AdminAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Cinsiyet")
                        .HasColumnType("bit");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("BingolSinema.Models.Bilet", b =>
                {
                    b.Property<int>("BiletID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BiletID"));

                    b.Property<decimal>("Fiyat")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RezervasyonID")
                        .HasColumnType("int");

                    b.HasKey("BiletID");

                    b.ToTable("Bilets");
                });

            modelBuilder.Entity("BingolSinema.Models.Film", b =>
                {
                    b.Property<int>("FilmID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FilmID"));

                    b.Property<string>("FilmAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TurID")
                        .HasColumnType("int");

                    b.Property<int>("Yil")
                        .HasColumnType("int");

                    b.Property<string>("Yönetmen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FilmID");

                    b.ToTable("Films");
                });

            modelBuilder.Entity("BingolSinema.Models.FilmTur", b =>
                {
                    b.Property<int>("TurID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TurID"));

                    b.Property<string>("Tur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TurID");

                    b.ToTable("FilmTurs");
                });

            modelBuilder.Entity("BingolSinema.Models.Kullanici", b =>
                {
                    b.Property<int>("KullaniciID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KullaniciID"));

                    b.Property<string>("KullaniciAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KullaniciID");

                    b.ToTable("Kullanicis");
                });

            modelBuilder.Entity("BingolSinema.Models.Rezervasyon", b =>
                {
                    b.Property<int>("RezervasyonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RezervasyonID"));

                    b.Property<int>("KoltukNumarasi")
                        .HasColumnType("int");

                    b.Property<int>("KullaniciID")
                        .HasColumnType("int");

                    b.Property<int>("SeansID")
                        .HasColumnType("int");

                    b.HasKey("RezervasyonID");

                    b.HasIndex("SeansID");

                    b.ToTable("Rezervasyons");
                });

            modelBuilder.Entity("BingolSinema.Models.Salon", b =>
                {
                    b.Property<int>("SalonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalonID"));

                    b.Property<int>("Kapasite")
                        .HasColumnType("int");

                    b.Property<string>("SalonAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SalonID");

                    b.ToTable("Salons");
                });

            modelBuilder.Entity("BingolSinema.Models.Seans", b =>
                {
                    b.Property<int>("SeansID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SeansID"));

                    b.Property<DateTime>("BaslangicZamani")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("BitisZamani")
                        .HasColumnType("datetime2");

                    b.Property<int>("FilmID")
                        .HasColumnType("int");

                    b.Property<int>("SalonID")
                        .HasColumnType("int");

                    b.HasKey("SeansID");

                    b.HasIndex("FilmID");

                    b.HasIndex("SalonID");

                    b.ToTable("Seanss");
                });

            modelBuilder.Entity("BingolSinema.Models.Rezervasyon", b =>
                {
                    b.HasOne("BingolSinema.Models.Seans", "Seans")
                        .WithMany("Rezervasyonlar")
                        .HasForeignKey("SeansID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seans");
                });

            modelBuilder.Entity("BingolSinema.Models.Seans", b =>
                {
                    b.HasOne("BingolSinema.Models.Film", "Film")
                        .WithMany()
                        .HasForeignKey("FilmID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BingolSinema.Models.Salon", "Salon")
                        .WithMany("Seanslar")
                        .HasForeignKey("SalonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("BingolSinema.Models.Salon", b =>
                {
                    b.Navigation("Seanslar");
                });

            modelBuilder.Entity("BingolSinema.Models.Seans", b =>
                {
                    b.Navigation("Rezervasyonlar");
                });
#pragma warning restore 612, 618
        }
    }
}
