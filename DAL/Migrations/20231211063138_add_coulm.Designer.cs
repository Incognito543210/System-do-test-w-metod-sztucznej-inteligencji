﻿// <auto-generated />
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231211063138_add_coulm")]
    partial class add_coulm
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("Model.ParamInfo", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("LowerBoundary")
                        .HasColumnType("REAL");

                    b.Property<double>("Step")
                        .HasColumnType("REAL");

                    b.Property<double>("UpperBoundary")
                        .HasColumnType("REAL");

                    b.HasKey("Name");

                    b.ToTable("ParamaInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
