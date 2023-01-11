﻿// <auto-generated />
using System;
using BackGaming.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BackGaming.Migrations
{
    [DbContext(typeof(GamingApiDbContext))]
    [Migration("20230111210719_adminCreation")]
    partial class adminCreation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BackGaming.Models.AchatService", b =>
                {
                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("ClientId", "ServiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("AchatService");
                });

            modelBuilder.Entity("BackGaming.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("BackGaming.Models.Coach", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Coach");
                });

            modelBuilder.Entity("BackGaming.Models.Demande", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<int?>("CoachId")
                        .HasColumnType("int");

                    b.Property<string>("DiscordId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Game")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdInGame")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RankInGame")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Team")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("etat")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique()
                        .HasFilter("[ClientId] IS NOT NULL");

                    b.HasIndex("CoachId");

                    b.ToTable("Demande");
                });

            modelBuilder.Entity("BackGaming.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CoachId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCeation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("BackGaming.Models.AchatService", b =>
                {
                    b.HasOne("BackGaming.Models.Client", "Client")
                        .WithMany("AchatServices")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackGaming.Models.Service", "Service")
                        .WithMany("AchatServices")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("BackGaming.Models.Demande", b =>
                {
                    b.HasOne("BackGaming.Models.Client", "Client")
                        .WithOne("Demande")
                        .HasForeignKey("BackGaming.Models.Demande", "ClientId");

                    b.HasOne("BackGaming.Models.Coach", "Coach")
                        .WithMany("Demandes")
                        .HasForeignKey("CoachId");

                    b.Navigation("Client");

                    b.Navigation("Coach");
                });

            modelBuilder.Entity("BackGaming.Models.Service", b =>
                {
                    b.HasOne("BackGaming.Models.Coach", "Coach")
                        .WithMany("Services")
                        .HasForeignKey("CoachId");

                    b.Navigation("Coach");
                });

            modelBuilder.Entity("BackGaming.Models.Client", b =>
                {
                    b.Navigation("AchatServices");

                    b.Navigation("Demande");
                });

            modelBuilder.Entity("BackGaming.Models.Coach", b =>
                {
                    b.Navigation("Demandes");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("BackGaming.Models.Service", b =>
                {
                    b.Navigation("AchatServices");
                });
#pragma warning restore 612, 618
        }
    }
}
