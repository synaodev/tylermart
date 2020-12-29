﻿#pragma warning disable 1591

// <auto-generated />
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

            modelBuilder.Entity("TylerMart.Domain.Models.Customer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Address = "Nowhere",
                            Email = "admin.admin@revature.net",
                            FirstName = "Admin",
                            LastName = "Admin",
                            Password = "administrator"
                        });
                });

            modelBuilder.Entity("TylerMart.Domain.Models.Location", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Dreamland"
                        },
                        new
                        {
                            ID = 2,
                            Name = "California"
                        },
                        new
                        {
                            ID = 3,
                            Name = "Washington"
                        },
                        new
                        {
                            ID = 4,
                            Name = "Oregon"
                        },
                        new
                        {
                            ID = 5,
                            Name = "Texas"
                        },
                        new
                        {
                            ID = 6,
                            Name = "New York"
                        },
                        new
                        {
                            ID = 7,
                            Name = "Virginia"
                        });
                });

            modelBuilder.Entity("TylerMart.Domain.Models.LocationProduct", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("LocationID")
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("LocationID");

                    b.HasIndex("ProductID");

                    b.ToTable("LocationProducts");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            LocationID = 1,
                            ProductID = 1
                        },
                        new
                        {
                            ID = 2,
                            LocationID = 2,
                            ProductID = 2
                        },
                        new
                        {
                            ID = 3,
                            LocationID = 2,
                            ProductID = 2
                        },
                        new
                        {
                            ID = 4,
                            LocationID = 2,
                            ProductID = 3
                        },
                        new
                        {
                            ID = 5,
                            LocationID = 2,
                            ProductID = 3
                        },
                        new
                        {
                            ID = 6,
                            LocationID = 2,
                            ProductID = 3
                        },
                        new
                        {
                            ID = 7,
                            LocationID = 3,
                            ProductID = 4
                        },
                        new
                        {
                            ID = 8,
                            LocationID = 3,
                            ProductID = 4
                        },
                        new
                        {
                            ID = 9,
                            LocationID = 3,
                            ProductID = 4
                        },
                        new
                        {
                            ID = 10,
                            LocationID = 4,
                            ProductID = 5
                        },
                        new
                        {
                            ID = 11,
                            LocationID = 4,
                            ProductID = 6
                        },
                        new
                        {
                            ID = 12,
                            LocationID = 4,
                            ProductID = 7
                        },
                        new
                        {
                            ID = 13,
                            LocationID = 5,
                            ProductID = 2
                        },
                        new
                        {
                            ID = 14,
                            LocationID = 5,
                            ProductID = 2
                        },
                        new
                        {
                            ID = 15,
                            LocationID = 5,
                            ProductID = 2
                        },
                        new
                        {
                            ID = 16,
                            LocationID = 5,
                            ProductID = 2
                        },
                        new
                        {
                            ID = 17,
                            LocationID = 6,
                            ProductID = 3
                        },
                        new
                        {
                            ID = 18,
                            LocationID = 6,
                            ProductID = 3
                        },
                        new
                        {
                            ID = 19,
                            LocationID = 6,
                            ProductID = 7
                        },
                        new
                        {
                            ID = 20,
                            LocationID = 6,
                            ProductID = 7
                        },
                        new
                        {
                            ID = 21,
                            LocationID = 6,
                            ProductID = 5
                        },
                        new
                        {
                            ID = 22,
                            LocationID = 7,
                            ProductID = 2
                        },
                        new
                        {
                            ID = 23,
                            LocationID = 7,
                            ProductID = 3
                        },
                        new
                        {
                            ID = 24,
                            LocationID = 7,
                            ProductID = 4
                        },
                        new
                        {
                            ID = 25,
                            LocationID = 7,
                            ProductID = 5
                        },
                        new
                        {
                            ID = 26,
                            LocationID = 7,
                            ProductID = 6
                        },
                        new
                        {
                            ID = 27,
                            LocationID = 7,
                            ProductID = 7
                        });
                });

            modelBuilder.Entity("TylerMart.Domain.Models.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Complete")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<int>("LocationID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("LocationID");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Complete = true,
                            CreatedAt = new DateTime(2020, 12, 29, 13, 49, 2, 975, DateTimeKind.Local).AddTicks(6245),
                            CustomerID = 1,
                            LocationID = 1
                        });
                });

            modelBuilder.Entity("TylerMart.Domain.Models.OrderProduct", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("OrderProducts");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            OrderID = 1,
                            ProductID = 1
                        });
                });

            modelBuilder.Entity("TylerMart.Domain.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Description = "This is a nightmare!",
                            Name = "Nightmare",
                            Price = 180.50m
                        },
                        new
                        {
                            ID = 2,
                            Description = "Delicious eggs",
                            Name = "Eggs",
                            Price = 4.40m
                        },
                        new
                        {
                            ID = 3,
                            Description = "For storing books",
                            Name = "Bookshelf",
                            Price = 68.99m
                        },
                        new
                        {
                            ID = 4,
                            Description = "For when it's chilly outside",
                            Name = "Jacket",
                            Price = 18.99m
                        },
                        new
                        {
                            ID = 5,
                            Description = "A bunch of oranges",
                            Name = "Oranges",
                            Price = 4.0m
                        },
                        new
                        {
                            ID = 6,
                            Description = "It's a purple-ish color",
                            Name = "Lipstick",
                            Price = 5.99m
                        },
                        new
                        {
                            ID = 7,
                            Description = "An excellent game",
                            Name = "Cave Story Switch",
                            Price = 35.99m
                        });
                });

            modelBuilder.Entity("TylerMart.Domain.Models.LocationProduct", b =>
                {
                    b.HasOne("TylerMart.Domain.Models.Location", "Location")
                        .WithMany("LocationProducts")
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TylerMart.Domain.Models.Product", "Product")
                        .WithMany("LocationProducts")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TylerMart.Domain.Models.Order", b =>
                {
                    b.HasOne("TylerMart.Domain.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TylerMart.Domain.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("TylerMart.Domain.Models.OrderProduct", b =>
                {
                    b.HasOne("TylerMart.Domain.Models.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TylerMart.Domain.Models.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TylerMart.Domain.Models.Location", b =>
                {
                    b.Navigation("LocationProducts");
                });

            modelBuilder.Entity("TylerMart.Domain.Models.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("TylerMart.Domain.Models.Product", b =>
                {
                    b.Navigation("LocationProducts");

                    b.Navigation("OrderProducts");
                });
#pragma warning restore 612, 618
        }
    }
}

#pragma warning restore 1591
