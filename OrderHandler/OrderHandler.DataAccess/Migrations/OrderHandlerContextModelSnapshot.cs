﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderHandler.DataAccess.Contexts;

#nullable disable

namespace OrderHandler.DataAccess.Migrations
{
    [DbContext(typeof(OrderHandlerContext))]
    partial class OrderHandlerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OrderHandler.DomainCommons.DataModels.ArticleModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ArticleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ArticleNumber")
                        .HasColumnType("int");

                    b.Property<Guid?>("ColorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("UnitPrice")
                        .HasPrecision(11, 2)
                        .HasColumnType("decimal(11,2)");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("OrderHandler.DomainCommons.DataModels.ColorModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("OrderHandler.DomainCommons.DataModels.OrderModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OrderHandler.DomainCommons.DataModels.OrderRowModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AmountOfArticles")
                        .HasColumnType("int");

                    b.Property<Guid>("ArticleIdId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderIdId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RowNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArticleIdId");

                    b.HasIndex("OrderIdId");

                    b.ToTable("OrderRows");
                });

            modelBuilder.Entity("OrderHandler.DomainCommons.DataModels.ArticleModel", b =>
                {
                    b.HasOne("OrderHandler.DomainCommons.DataModels.ColorModel", "Color")
                        .WithMany()
                        .HasForeignKey("ColorId");

                    b.Navigation("Color");
                });

            modelBuilder.Entity("OrderHandler.DomainCommons.DataModels.OrderRowModel", b =>
                {
                    b.HasOne("OrderHandler.DomainCommons.DataModels.ArticleModel", "ArticleId")
                        .WithMany()
                        .HasForeignKey("ArticleIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderHandler.DomainCommons.DataModels.OrderModel", "OrderId")
                        .WithMany()
                        .HasForeignKey("OrderIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArticleId");

                    b.Navigation("OrderId");
                });
#pragma warning restore 612, 618
        }
    }
}
