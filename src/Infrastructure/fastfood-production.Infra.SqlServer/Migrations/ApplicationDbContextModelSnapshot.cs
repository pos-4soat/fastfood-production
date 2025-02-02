﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using fastfood_production.Infra.SqlServer.Context;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace fastfood_production.Infra.SqlServer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [ExcludeFromCodeCoverage]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("FastFood")
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("fastfood_production.Domain.Entity.ProductionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreationDate");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderId");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("Status");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateDate");

                    b.HasKey("Id")
                        .HasName("ProductionId");

                    b.ToTable("Production", "FastFood");
                });

            modelBuilder.Entity("fastfood_production.Domain.Entity.ProductionItemEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<int>("ProductionId")
                        .HasColumnType("int")
                        .HasColumnName("ProductionId");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("Quantity");

                    b.HasKey("Id")
                        .HasName("ProductionItemId");

                    b.HasIndex("ProductionId");

                    b.ToTable("ProductionItem", "FastFood");
                });

            modelBuilder.Entity("fastfood_production.Domain.Entity.ProductionItemEntity", b =>
                {
                    b.HasOne("fastfood_production.Domain.Entity.ProductionEntity", "Production")
                        .WithMany("ProductionItems")
                        .HasForeignKey("ProductionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Production");
                });

            modelBuilder.Entity("fastfood_production.Domain.Entity.ProductionEntity", b =>
                {
                    b.Navigation("ProductionItems");
                });
#pragma warning restore 612, 618
        }
    }
}
