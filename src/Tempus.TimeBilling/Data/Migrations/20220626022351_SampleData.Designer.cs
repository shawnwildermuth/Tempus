﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tempus.TimeBilling.Data;

#nullable disable

namespace Tempus.TimeBilling.Migrations
{
    [DbContext(typeof(TimeBillingContext))]
    [Migration("20220626022351_SampleData")]
    partial class SampleData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Tempus.TimeBilling.Data.TimeBill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<decimal>("Hours")
                        .HasPrecision(8, 1)
                        .HasColumnType("decimal(8,1)");

                    b.Property<string>("Notes")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<decimal>("Rate")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("WorkPerformed")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TimeBills", "Tempus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerId = 1,
                            Hours = 1.3m,
                            Notes = "Worked with the client on architecture.",
                            Rate = 135m,
                            WorkPerformed = "Consulting",
                            WorkerId = 1
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
