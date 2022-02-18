﻿// <auto-generated />
using Chunya5.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Chunya5.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Chunya5.Models.Bonds", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BondsCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("BondsName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CNBD")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDel")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("ParValue")
                        .HasColumnType("decimal(65,30)");

                    b.Property<double>("Rate")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Bonds");
                });
#pragma warning restore 612, 618
        }
    }
}