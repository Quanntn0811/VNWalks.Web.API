﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VNWalks.Infrastructure.Data;

#nullable disable

namespace VNWalks.Infrastructure.Migrations
{
    [DbContext(typeof(VNWalksDbContext))]
    partial class VNWalksDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VNWalks.Shared.EntityModels.SqlServer.EntityModels.Difficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Difficulties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5daca6f3-3e6d-40fa-9c54-e51e2fc60b62"),
                            Name = "Easy"
                        },
                        new
                        {
                            Id = new Guid("5223651c-9784-4ad7-a596-bd8cf69a5725"),
                            Name = "Medium"
                        },
                        new
                        {
                            Id = new Guid("747cf14c-a757-4f16-9170-2a2069e78541"),
                            Name = "Hard"
                        });
                });

            modelBuilder.Entity("VNWalks.Shared.EntityModels.SqlServer.EntityModels.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FileSizeInBytes")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("VNWalks.Shared.EntityModels.SqlServer.EntityModels.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("72bae868-bc5c-48f8-a8ab-e29ca99e5dfc"),
                            Code = "29",
                            Name = "Nghe An"
                        },
                        new
                        {
                            Id = new Guid("d43c4ba3-c173-4e35-8f59-a98c3adc496c"),
                            Code = "30",
                            Name = "Ha Tinh"
                        },
                        new
                        {
                            Id = new Guid("2459c408-89c1-4947-86d9-d2cebff3352b"),
                            Code = "42",
                            Name = "Lam Dong"
                        },
                        new
                        {
                            Id = new Guid("42a416b2-36df-4f89-82d7-d77873208da7"),
                            Code = "53",
                            Name = "Tien Giang"
                        });
                });

            modelBuilder.Entity("VNWalks.Shared.EntityModels.SqlServer.EntityModels.Walk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WalkImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("RegionId");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("VNWalks.Shared.EntityModels.SqlServer.EntityModels.Walk", b =>
                {
                    b.HasOne("VNWalks.Shared.EntityModels.SqlServer.EntityModels.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VNWalks.Shared.EntityModels.SqlServer.EntityModels.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");

                    b.Navigation("Region");
                });
#pragma warning restore 612, 618
        }
    }
}
