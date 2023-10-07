﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OriginalLanguage.Context;

#nullable disable

namespace OriginalLanguage.Context.MigrationsPostgreSQL.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20231002225923_Configure1")]
    partial class Configure1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OriginalLanguage.Context.Entities.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTimeAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsLessonTheory")
                        .HasColumnType("boolean");

                    b.Property<string>("MetaDescription")
                        .HasColumnType("text");

                    b.Property<string>("MetaKeywords")
                        .HasColumnType("text");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("articles", (string)null);
                });

            modelBuilder.Entity("OriginalLanguage.Context.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateTimeAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("LanguageId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("OriginalLanguage.Context.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateTimeAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsConlang")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("NativeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("languages", (string)null);
                });

            modelBuilder.Entity("OriginalLanguage.Context.Entities.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<int?>("TheoryArticleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("TheoryArticleId")
                        .IsUnique();

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("OriginalLanguage.Context.Entities.LessonProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("LessonId")
                        .HasColumnType("integer");

                    b.Property<int>("ProgressLevel")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.ToTable("LessonProgress");
                });

            modelBuilder.Entity("OriginalLanguage.Context.Entities.LessonSamples", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("LessonId")
                        .HasColumnType("integer");

                    b.Property<int>("MainSentenceVariantId")
                        .HasColumnType("integer");

                    b.Property<int>("MinimalProgressLevel")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.HasIndex("MainSentenceVariantId")
                        .IsUnique();

                    b.ToTable("LessonSamples");
                });

            modelBuilder.Entity("OriginalLanguage.Context.Entities.Sentence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Glosses")
                        .HasColumnType("text");

                    b.Property<int>("LessonSampleId")
                        .HasColumnType("integer");

                    b.Property<string>("LiteralTranslation")
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<string>("Transcription")
                        .HasColumnType("text");

                    b.Property<string>("Translation")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LessonSampleId");

                    b.ToTable("Sentences");
                });

            modelBuilder.Entity("OriginalLanguage.Context.Entities.StaticPage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTimeAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MetaDescription")
                        .HasColumnType("text");

                    b.Property<string>("MetaKeywords")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("StaticPages");
                });

            modelBuilder.Entity("OriginalLanguage.Context.Entities.Course", b =>
                {
                    b.HasOne("OriginalLanguage.Context.Entities.Language", "Language")
                        .WithMany("Courses")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Language");
                });

            modelBuilder.Entity("OriginalLanguage.Context.Entities.Lesson", b =>
                {
                    b.HasOne("OriginalLanguage.Context.Entities.Course", "Course")
                        .WithMany("Lessons")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OriginalLanguage.Context.Entities.Article", "TheoryArticle")
                        .WithOne("Lesson")
                        .HasForeignKey("OriginalLanguage.Context.Entities.Lesson", "TheoryArticleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Course");

                    b.Navigation("TheoryArticle");
                });

            modelBuilder.Entity("OriginalLanguage.Context.Entities.LessonProgress", b =>
                {
                    b.HasOne("OriginalLanguage.Context.Entities.Lesson", "Lesson")
                        .WithMany("LessonProgresses")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("OriginalLanguage.Context.Entities.LessonSamples", b =>
                {
                    b.HasOne("OriginalLanguage.Context.Entities.Lesson", "Lesson")
                        .WithMany("LessonSamples")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OriginalLanguage.Context.Entities.Sentence", "MainSentenceVariant")
                        .WithOne()
                        .HasForeignKey("OriginalLanguage.Context.Entities.LessonSamples", "MainSentenceVariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("MainSentenceVariant");
                });

            modelBuilder.Entity("OriginalLanguage.Context.Entities.Sentence", b =>
                {
                    b.HasOne("OriginalLanguage.Context.Entities.LessonSamples", "LessonSamples")
                        .WithMany("SentenceVariants")
                        .HasForeignKey("LessonSampleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LessonSamples");
                });

            modelBuilder.Entity("OriginalLanguage.Context.Entities.Article", b =>
                {
                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("OriginalLanguage.Context.Entities.Course", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("OriginalLanguage.Context.Entities.Language", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("OriginalLanguage.Context.Entities.Lesson", b =>
                {
                    b.Navigation("LessonProgresses");

                    b.Navigation("LessonSamples");
                });

            modelBuilder.Entity("OriginalLanguage.Context.Entities.LessonSamples", b =>
                {
                    b.Navigation("SentenceVariants");
                });
#pragma warning restore 612, 618
        }
    }
}
