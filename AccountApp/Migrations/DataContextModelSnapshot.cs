﻿// <auto-generated />
using System;
using AccountApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AccountApp.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.3");

            modelBuilder.Entity("AccountApp.Models.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AreaName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("AccountApp.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("AccountApp.Models.GLTran", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Credit")
                        .HasColumnType("REAL");

                    b.Property<int>("CustomerID")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Debit")
                        .HasColumnType("REAL");

                    b.Property<double>("TranAmount")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("TranDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("TranDateTimeStamp")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TranDetail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TranType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CustomerID");

                    b.ToTable("GLTrans");
                });

            modelBuilder.Entity("AccountApp.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ChalanNumber")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Posted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductCode")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Sale")
                        .HasColumnType("REAL");

                    b.Property<int>("SaleBookId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("SaleRate")
                        .HasColumnType("REAL");

                    b.Property<double>("SaleWithoutCommission")
                        .HasColumnType("REAL");

                    b.Property<int>("SoldQuantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProductCode");

                    b.HasIndex("SaleBookId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("AccountApp.Models.OrderDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Commission")
                        .HasColumnType("REAL");

                    b.Property<int>("CustomerID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("OrderDetailDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderNum")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Posted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Rate")
                        .HasColumnType("REAL");

                    b.Property<int>("SaleBookId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("CustomerID");

                    b.HasIndex("OrderNum");

                    b.HasIndex("ProductID");

                    b.HasIndex("SaleBookId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("AccountApp.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("AccountApp.Models.SaleBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SaleBooks");
                });

            modelBuilder.Entity("AccountApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AccountApp.Models.GLTran", b =>
                {
                    b.HasOne("AccountApp.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("AccountApp.Models.Order", b =>
                {
                    b.HasOne("AccountApp.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccountApp.Models.SaleBook", "SaleBook")
                        .WithMany()
                        .HasForeignKey("SaleBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("SaleBook");
                });

            modelBuilder.Entity("AccountApp.Models.OrderDetails", b =>
                {
                    b.HasOne("AccountApp.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccountApp.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderNum")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccountApp.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccountApp.Models.SaleBook", "SaleBook")
                        .WithMany()
                        .HasForeignKey("SaleBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Order");

                    b.Navigation("Product");

                    b.Navigation("SaleBook");
                });
#pragma warning restore 612, 618
        }
    }
}
