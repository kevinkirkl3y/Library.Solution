﻿// <auto-generated />
using System;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Library.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20210112223403_CheckoutsLibrary")]
    partial class CheckoutsLibrary
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Library.Models.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("MidIn");

                    b.Property<string>("Name");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Library.Models.AuthorBook", b =>
                {
                    b.Property<int>("AuthorBookId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<int>("BookId");

                    b.HasKey("AuthorBookId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BookId");

                    b.ToTable("AuthorBooks");
                });

            modelBuilder.Entity("Library.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("LibraryBranchId");

                    b.Property<string>("Title");

                    b.HasKey("BookId");

                    b.HasIndex("LibraryBranchId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Library.Models.BookLibrary", b =>
                {
                    b.Property<int>("BookLibraryId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookId");

                    b.Property<int>("CheckoutId");

                    b.Property<int>("LibraryBranchId");

                    b.Property<int>("Num_Copies");

                    b.HasKey("BookLibraryId");

                    b.HasIndex("BookId");

                    b.HasIndex("CheckoutId");

                    b.HasIndex("LibraryBranchId");

                    b.ToTable("BookLibraries");
                });

            modelBuilder.Entity("Library.Models.Checkout", b =>
                {
                    b.Property<int>("CheckoutId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookLibraryId");

                    b.Property<DateTime>("CheckoutDate");

                    b.Property<DateTime>("DueDate");

                    b.Property<int>("PatronId");

                    b.HasKey("CheckoutId");

                    b.HasIndex("PatronId");

                    b.ToTable("Checkouts");
                });

            modelBuilder.Entity("Library.Models.LibraryBranch", b =>
                {
                    b.Property<int>("LibraryBranchId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("LibraryName");

                    b.HasKey("LibraryBranchId");

                    b.ToTable("LibraryBranches");
                });

            modelBuilder.Entity("Library.Models.Patron", b =>
                {
                    b.Property<int>("PatronId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("MidIn");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.HasKey("PatronId");

                    b.ToTable("Patrons");
                });

            modelBuilder.Entity("Library.Models.AuthorBook", b =>
                {
                    b.HasOne("Library.Models.Author", "Author")
                        .WithMany("AuthorBooks")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Library.Models.Book", "Book")
                        .WithMany("AuthorBooks")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Library.Models.Book", b =>
                {
                    b.HasOne("Library.Models.LibraryBranch")
                        .WithMany("Books")
                        .HasForeignKey("LibraryBranchId");
                });

            modelBuilder.Entity("Library.Models.BookLibrary", b =>
                {
                    b.HasOne("Library.Models.Book", "Book")
                        .WithMany("BookLibraries")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Library.Models.Checkout", "Checkout")
                        .WithMany("BookLibraries")
                        .HasForeignKey("CheckoutId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Library.Models.LibraryBranch", "LibraryBranch")
                        .WithMany("BookLibraries")
                        .HasForeignKey("LibraryBranchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Library.Models.Checkout", b =>
                {
                    b.HasOne("Library.Models.Patron")
                        .WithMany("Checkouts")
                        .HasForeignKey("PatronId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
