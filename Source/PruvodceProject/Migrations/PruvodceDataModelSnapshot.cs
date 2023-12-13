﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PruvodceProject.Data;

#nullable disable

namespace PruvodceProject.Migrations
{
    [DbContext(typeof(PruvodceData))]
    partial class PruvodceDataModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PruvodceProject.Models.AutomatyModel", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("bagety")
                        .HasColumnType("bit");

                    b.Property<string>("budova")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("budovaIDIdBudovy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("patro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("typ")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("budovaIDIdBudovy");

                    b.ToTable("Automaty");
                });

            modelBuilder.Entity("PruvodceProject.Models.BudovyModel", b =>
                {
                    b.Property<Guid>("IdBudovy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("kodoveOznaceni")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdBudovy");

                    b.ToTable("Budovy");
                });

            modelBuilder.Entity("PruvodceProject.Models.ClanekModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DatumVytvoreni")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ID_autora")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NadpisClanku")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ObsahClanku")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clanek");
                });

            modelBuilder.Entity("PruvodceProject.Models.CrowdSourceModel", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("existujici")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mailUzivatele")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nadpis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("odpovedAmina")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("stav")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("CrowdSource");
                });

            modelBuilder.Entity("PruvodceProject.Models.KafeModel", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nazev")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("odkazNaMenu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("popis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Kafarny");
                });

            modelBuilder.Entity("PruvodceProject.Models.ObchodModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatumVytvoreni")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nazev")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ObsahClanku")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Obchody");
                });

            modelBuilder.Entity("PruvodceProject.Models.PhotoModelBudovy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BudovaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cesta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdBudovy1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nazev")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pripona")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdBudovy1");

                    b.ToTable("PhotoBudovy");
                });

            modelBuilder.Entity("PruvodceProject.Models.PhotoModelUcebny", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cesta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nazev")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pripona")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UcebnaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UcebnaIdId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UcebnaIdId");

                    b.ToTable("PhotoUcebny");
                });

            modelBuilder.Entity("PruvodceProject.Models.StravovaciZarizeniModel", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nazev")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("odkazNaMenu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("popis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("StravovaciZarizeni");
                });

            modelBuilder.Entity("PruvodceProject.Models.UcebnaModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("budovaIDIdBudovy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("budovuID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("druh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("idUcebny")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("patro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("budovaIDIdBudovy");

                    b.ToTable("Ucebna");
                });

            modelBuilder.Entity("PruvodceProject.Models.UserModel", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("heslo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("jeAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trida")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PrihlasovaciUdaje");
                });

            modelBuilder.Entity("PruvodceProject.Models.UserVerify", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("expirace")
                        .HasColumnType("datetime2");

                    b.Property<string>("heslo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("kod")
                        .HasColumnType("int");

                    b.Property<string>("mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trida")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("OverovaciUdaje");
                });

            modelBuilder.Entity("PruvodceProject.Models.AutomatyModel", b =>
                {
                    b.HasOne("PruvodceProject.Models.BudovyModel", "budovaID")
                        .WithMany("Automaty")
                        .HasForeignKey("budovaIDIdBudovy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("budovaID");
                });

            modelBuilder.Entity("PruvodceProject.Models.PhotoModelBudovy", b =>
                {
                    b.HasOne("PruvodceProject.Models.BudovyModel", "IdBudovy")
                        .WithMany("fotky")
                        .HasForeignKey("IdBudovy1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdBudovy");
                });

            modelBuilder.Entity("PruvodceProject.Models.PhotoModelUcebny", b =>
                {
                    b.HasOne("PruvodceProject.Models.UcebnaModel", "UcebnaId")
                        .WithMany("fotky")
                        .HasForeignKey("UcebnaIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UcebnaId");
                });

            modelBuilder.Entity("PruvodceProject.Models.UcebnaModel", b =>
                {
                    b.HasOne("PruvodceProject.Models.BudovyModel", "budovaID")
                        .WithMany("Ucebny")
                        .HasForeignKey("budovaIDIdBudovy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("budovaID");
                });

            modelBuilder.Entity("PruvodceProject.Models.BudovyModel", b =>
                {
                    b.Navigation("Automaty");

                    b.Navigation("Ucebny");

                    b.Navigation("fotky");
                });

            modelBuilder.Entity("PruvodceProject.Models.UcebnaModel", b =>
                {
                    b.Navigation("fotky");
                });
#pragma warning restore 612, 618
        }
    }
}
