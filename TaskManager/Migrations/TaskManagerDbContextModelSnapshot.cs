﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TaskManager.Entities;

namespace TaskManager.Migrations
{
    [DbContext(typeof(TaskManagerDbContext))]
    partial class TaskManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TaskManager.Entities.TaskEntity", b =>
                {
                    b.Property<long>("TaskId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CompletionTime");

                    b.Property<DateTime>("CreationTime");

                    b.Property<DateTime>("DueDate");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsComplete");

                    b.Property<string>("TaskDetails");

                    b.Property<string>("Title");

                    b.HasKey("TaskId");

                    b.ToTable("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}