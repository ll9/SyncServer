﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
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
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SyncServer.Models.ProjectTable", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Json");

                    b.Property<int>("RowVersion");

                    b.Property<bool>("SyncStatus");

                    b.HasKey("Name");

                    b.ToTable("ProjectTable");
                });

            modelBuilder.Entity("SyncServer.Models.ProjectTableChangeSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Json");

                    b.Property<string>("Name");

                    b.Property<int>("RowVersion");

                    b.Property<bool>("SyncStatus");

                    b.HasKey("Id");

                    b.ToTable("ProjectTableChangeSet");
                });
#pragma warning restore 612, 618
        }
    }
}
