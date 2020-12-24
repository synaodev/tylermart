﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TylerMart.Storage.Contexts;

namespace TylerMart.Storage.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("TylerMart.Storage.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerID = 1,
                            EmailAddress = "tyler.cadena@revature.net",
                            FirstName = "Tyler",
                            LastName = "Cadena",
                            Password = "synaodev"
                        },
                        new
                        {
                            CustomerID = 2,
                            EmailAddress = "george.bumble@revature.net",
                            FirstName = "George",
                            LastName = "Bumble",
                            Password = "onionbutt"
                        });
                });

            modelBuilder.Entity("TylerMart.Storage.Models.Location", b =>
                {
                    b.Property<int>("LocationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationID");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            LocationID = 1,
                            Name = "New Jersey"
                        },
                        new
                        {
                            LocationID = 2,
                            Name = "Florida"
                        });
                });

            modelBuilder.Entity("TylerMart.Storage.Models.LocationProduct", b =>
                {
                    b.Property<int>("LocationProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("LocationID")
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("LocationProductID");

                    b.HasIndex("LocationID");

                    b.HasIndex("ProductID");

                    b.ToTable("LocationProducts");

                    b.HasData(
                        new
                        {
                            LocationProductID = 1,
                            LocationID = 1,
                            ProductID = 1
                        },
                        new
                        {
                            LocationProductID = 2,
                            LocationID = 1,
                            ProductID = 1
                        },
                        new
                        {
                            LocationProductID = 3,
                            LocationID = 1,
                            ProductID = 2
                        },
                        new
                        {
                            LocationProductID = 4,
                            LocationID = 1,
                            ProductID = 2
                        },
                        new
                        {
                            LocationProductID = 5,
                            LocationID = 2,
                            ProductID = 1
                        },
                        new
                        {
                            LocationProductID = 6,
                            LocationID = 2,
                            ProductID = 1
                        },
                        new
                        {
                            LocationProductID = 7,
                            LocationID = 2,
                            ProductID = 2
                        },
                        new
                        {
                            LocationProductID = 8,
                            LocationID = 2,
                            ProductID = 2
                        });
                });

            modelBuilder.Entity("TylerMart.Storage.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<int>("LocationID")
                        .HasColumnType("int");

                    b.Property<DateTime>("PlacedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("LocationID");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderID = 1,
                            CustomerID = 1,
                            LocationID = 1,
                            PlacedAt = new DateTime(2020, 12, 23, 19, 10, 35, 274, DateTimeKind.Local).AddTicks(9182)
                        },
                        new
                        {
                            OrderID = 2,
                            CustomerID = 2,
                            LocationID = 2,
                            PlacedAt = new DateTime(2020, 12, 23, 19, 10, 35, 279, DateTimeKind.Local).AddTicks(5242)
                        });
                });

            modelBuilder.Entity("TylerMart.Storage.Models.OrderProduct", b =>
                {
                    b.Property<int>("OrderProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("OrderProductID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("OrderProducts");

                    b.HasData(
                        new
                        {
                            OrderProductID = 1,
                            OrderID = 1,
                            ProductID = 1
                        },
                        new
                        {
                            OrderProductID = 2,
                            OrderID = 1,
                            ProductID = 2
                        },
                        new
                        {
                            OrderProductID = 3,
                            OrderID = 2,
                            ProductID = 1
                        },
                        new
                        {
                            OrderProductID = 4,
                            OrderID = 2,
                            ProductID = 2
                        });
                });

            modelBuilder.Entity("TylerMart.Storage.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductID");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductID = 1,
                            Description = "You can carry stuff around",
                            Name = "Bag",
                            Price = 3.50m
                        },
                        new
                        {
                            ProductID = 2,
                            Description = "Helpful for those with poor eyesight",
                            Name = "Glasses",
                            Price = 20.00m
                        });
                });

            modelBuilder.Entity("TylerMart.Storage.Models.LocationProduct", b =>
                {
                    b.HasOne("TylerMart.Storage.Models.Location", "Location")
                        .WithMany("LocationProducts")
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TylerMart.Storage.Models.Product", "Product")
                        .WithMany("LocationProducts")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TylerMart.Storage.Models.Order", b =>
                {
                    b.HasOne("TylerMart.Storage.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TylerMart.Storage.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("TylerMart.Storage.Models.OrderProduct", b =>
                {
                    b.HasOne("TylerMart.Storage.Models.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TylerMart.Storage.Models.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TylerMart.Storage.Models.Location", b =>
                {
                    b.Navigation("LocationProducts");
                });

            modelBuilder.Entity("TylerMart.Storage.Models.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("TylerMart.Storage.Models.Product", b =>
                {
                    b.Navigation("LocationProducts");

                    b.Navigation("OrderProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
