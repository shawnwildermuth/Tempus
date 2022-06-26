﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tempus.Customers.Data;

#nullable disable

namespace Tempus.Customers.Migrations
{
    [DbContext(typeof(CustomerContext))]
    [Migration("20220626030445_SampleData")]
    partial class SampleData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Tempus.Customers.Data.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CompanyPhone")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Customers", "Tempus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompanyName = "Smith Cleaning",
                            CompanyPhone = "(404) 555-2121"
                        });
                });

            modelBuilder.Entity("Tempus.Customers.Data.Customer", b =>
                {
                    b.OwnsMany("Tempus.Customers.Data.Contact", "Contacts", b1 =>
                        {
                            b1.Property<int>("CustomerId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"), 1L, 1);

                            b1.Property<string>("Email")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("MiddleName")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Phone")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.HasKey("CustomerId", "Id");

                            b1.ToTable("Contacts", "Tempus");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");

                            b1.HasData(
                                new
                                {
                                    CustomerId = 1,
                                    Id = 1,
                                    FirstName = "Sarah",
                                    LastName = "Smith",
                                    Title = "President"
                                });
                        });

                    b.OwnsOne("Tempus.Customers.Data.Location", "Location", b1 =>
                        {
                            b1.Property<int>("CustomerId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("Country")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<int>("Id")
                                .HasColumnType("int");

                            b1.Property<string>("LineOne")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("nvarchar(255)");

                            b1.Property<string>("LineThree")
                                .HasMaxLength(255)
                                .HasColumnType("nvarchar(255)");

                            b1.Property<string>("LineTwo")
                                .HasMaxLength(255)
                                .HasColumnType("nvarchar(255)");

                            b1.Property<string>("PostalCode")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("StateProvince")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Locations", "Tempus");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");

                            b1.HasData(
                                new
                                {
                                    CustomerId = 1,
                                    City = "Austell",
                                    Country = "USA",
                                    Id = 1,
                                    LineOne = "123 Main Street",
                                    LineTwo = "Suite 400",
                                    PostalCode = "30303",
                                    StateProvince = "GA"
                                });
                        });

                    b.Navigation("Contacts");

                    b.Navigation("Location")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
