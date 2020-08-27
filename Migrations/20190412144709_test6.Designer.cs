﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SurveySystem.Data;

namespace SurveySystem.Migrations
{
    [DbContext(typeof(SurveyContext))]
    [Migration("20190412144709_test6")]
    partial class test6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SurveySystem.Models.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("SurveySystem.Models.Survey", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Question");

                    b.Property<Guid>("TypeId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("SurveySystem.Models.SurveyAnswerTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<Guid?>("SurveyId");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyAnswerTemplates");
                });

            modelBuilder.Entity("SurveySystem.Models.SurveyGroup", b =>
                {
                    b.Property<Guid>("SurveyId");

                    b.Property<Guid>("GroupId");

                    b.HasKey("SurveyId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("SurveyGroups");
                });

            modelBuilder.Entity("SurveySystem.Models.SurveyType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("SurveyTypes");
                });

            modelBuilder.Entity("SurveySystem.Models.Survey", b =>
                {
                    b.HasOne("SurveySystem.Models.SurveyType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SurveySystem.Models.SurveyAnswerTemplate", b =>
                {
                    b.HasOne("SurveySystem.Models.Survey", "Survey")
                        .WithMany("Templates")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SurveySystem.Models.SurveyGroup", b =>
                {
                    b.HasOne("SurveySystem.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SurveySystem.Models.Survey", "Survey")
                        .WithMany("Groups")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
