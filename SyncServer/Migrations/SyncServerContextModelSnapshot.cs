﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SyncServer.Models;

namespace SyncServer.Migrations
{
    [DbContext(typeof(SyncServerContext))]
    partial class SyncServerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085");

            modelBuilder.Entity("SyncServer.Models.Project", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("SyncServer.Models.ProjectTable", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProjectId");

                    b.HasKey("Name");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectTable");
                });

            modelBuilder.Entity("SyncServer.Models.SchemaDefinition", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Columns");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ProjectTableName");

                    b.Property<int>("RowVersion");

                    b.HasKey("Id");

                    b.HasIndex("ProjectTableName");

                    b.ToTable("SchemaDefinitions");
                });

            modelBuilder.Entity("SyncServer.Models.ProjectTable", b =>
                {
                    b.HasOne("SyncServer.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("SyncServer.Models.SchemaDefinition", b =>
                {
                    b.HasOne("SyncServer.Models.ProjectTable", "ProjectTable")
                        .WithMany()
                        .HasForeignKey("ProjectTableName");
                });
#pragma warning restore 612, 618
        }
    }
}
