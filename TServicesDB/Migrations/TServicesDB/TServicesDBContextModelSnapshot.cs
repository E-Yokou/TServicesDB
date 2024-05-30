﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TServicesDB.Models;

#nullable disable

namespace TServicesDB.Migrations.TServicesDB
{
    [DbContext(typeof(TServicesDBContext))]
    partial class TServicesDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TServicesDB.Models.TSBus", b =>
                {
                    b.Property<int>("TSBusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TSBusID"));

                    b.Property<int?>("TSRouteId")
                        .HasColumnType("int");

                    b.Property<string>("brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("goverment_number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("place")
                        .HasColumnType("int");

                    b.Property<string>("type_bus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TSBusID");

                    b.HasIndex("TSRouteId");

                    b.ToTable("TSBus");
                });

            modelBuilder.Entity("TServicesDB.Models.TSDriver", b =>
                {
                    b.Property<int>("TSDriverID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TSDriverID"));

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TSBusId")
                        .HasColumnType("int");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("middleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TSDriverID");

                    b.HasIndex("TSBusId");

                    b.ToTable("TSDrivers");
                });

            modelBuilder.Entity("TServicesDB.Models.TSRoute", b =>
                {
                    b.Property<int>("TSRouteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TSRouteID"));

                    b.Property<string>("firstCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("numberRoute")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TSRouteID");

                    b.ToTable("TSRoutes");
                });

            modelBuilder.Entity("TServicesDB.Models.TSStopover", b =>
                {
                    b.Property<int>("TSStopoverID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TSStopoverID"));

                    b.Property<int>("TSBusId")
                        .HasColumnType("int");

                    b.Property<int?>("TSRouteId")
                        .HasColumnType("int");

                    b.Property<string>("end_city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name_stopover")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<string>("start_city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TSStopoverID");

                    b.HasIndex("TSBusId");

                    b.HasIndex("TSRouteId");

                    b.ToTable("TSStopovers");
                });

            modelBuilder.Entity("TServicesDB.Models.TSTicket", b =>
                {
                    b.Property<int>("TSTicketID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TSTicketID"));

                    b.Property<int?>("TSBusID")
                        .HasColumnType("int");

                    b.Property<int>("TSBusSecondId")
                        .HasColumnType("int");

                    b.Property<string>("date_route")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("date_sale")
                        .HasColumnType("datetime2");

                    b.Property<string>("mail_client")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("number_route")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<string>("stopover")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TSTicketID");

                    b.HasIndex("TSBusID");

                    b.ToTable("TSTickets");
                });

            modelBuilder.Entity("TServicesDB.Models.TSBus", b =>
                {
                    b.HasOne("TServicesDB.Models.TSRoute", "TSRoute")
                        .WithMany("TSBus")
                        .HasForeignKey("TSRouteId");

                    b.Navigation("TSRoute");
                });

            modelBuilder.Entity("TServicesDB.Models.TSDriver", b =>
                {
                    b.HasOne("TServicesDB.Models.TSBus", "TSBus")
                        .WithMany("TSDriver")
                        .HasForeignKey("TSBusId");

                    b.Navigation("TSBus");
                });

            modelBuilder.Entity("TServicesDB.Models.TSStopover", b =>
                {
                    b.HasOne("TServicesDB.Models.TSBus", "TSBus")
                        .WithMany()
                        .HasForeignKey("TSBusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TServicesDB.Models.TSRoute", "TSRoute")
                        .WithMany("TSStopovers")
                        .HasForeignKey("TSRouteId");

                    b.Navigation("TSBus");

                    b.Navigation("TSRoute");
                });

            modelBuilder.Entity("TServicesDB.Models.TSTicket", b =>
                {
                    b.HasOne("TServicesDB.Models.TSBus", "TSBus")
                        .WithMany()
                        .HasForeignKey("TSBusID");

                    b.Navigation("TSBus");
                });

            modelBuilder.Entity("TServicesDB.Models.TSBus", b =>
                {
                    b.Navigation("TSDriver");
                });

            modelBuilder.Entity("TServicesDB.Models.TSRoute", b =>
                {
                    b.Navigation("TSBus");

                    b.Navigation("TSStopovers");
                });
#pragma warning restore 612, 618
        }
    }
}
