﻿// <auto-generated />
using System;
using KBS.Data.ConsoleApp.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace KBS.Data.ConsoleApp.Migrations
{
    [DbContext(typeof(BookStoreContext))]
    [Migration("20190817133327_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("KBS.Data.ConsoleApp.Model.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Author")
                        .HasColumnName("author")
                        .HasMaxLength(500);

                    b.Property<decimal>("Price")
                        .HasColumnName("price");

                    b.Property<string>("Title")
                        .HasColumnName("title")
                        .HasMaxLength(1024);

                    b.HasKey("Id")
                        .HasName("pk_books");

                    b.ToTable("books");
                });

            modelBuilder.Entity("KBS.Data.ConsoleApp.Model.BookSold", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Author")
                        .HasColumnName("author")
                        .HasMaxLength(500);

                    b.Property<decimal>("Price")
                        .HasColumnName("price");

                    b.Property<DateTime>("SoldDate")
                        .HasColumnName("sold_date");

                    b.Property<string>("Title")
                        .HasColumnName("title")
                        .HasMaxLength(1024);

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_books_sold");

                    b.ToTable("books_sold");
                });

            modelBuilder.Entity("KBS.Data.ConsoleApp.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("is_deleted")
                        .HasDefaultValue(0);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasMaxLength(100);

                    b.Property<int>("Role")
                        .HasColumnName("role");

                    b.Property<string>("Username")
                        .HasColumnName("username")
                        .HasMaxLength(500);

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users");
                });

            modelBuilder.Entity("KBS.Data.ConsoleApp.Model.UserProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasColumnName("address")
                        .HasMaxLength(500);

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasMaxLength(200);

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasMaxLength(32);

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_profiles");

                    b.ToTable("user_profiles");
                });
#pragma warning restore 612, 618
        }
    }
}
