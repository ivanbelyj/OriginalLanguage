using Microsoft.EntityFrameworkCore;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context;
public class MainDbContext : DbContext
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<StaticPage> StaticPages { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<LessonProgress> LessonProgress { get; set; }
    public DbSet<LessonSample> LessonSamples { get; set; }
    public DbSet<Sentence> Sentences { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Article>(entity =>
        {
            entity.ToTable("articles");
            entity.Property(x => x.Title).IsRequired();
            //entity.Property(x => x.IsLessonTheory).IsRequired();
            entity.Property(x => x.AuthorId).IsRequired();
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.ToTable("languages");
            entity.Property(x => x.Name).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(50);
            entity.HasIndex(x => x.Name).IsUnique();
            entity.Property(x => x.NativeName).HasMaxLength(50);
            entity.Property(x => x.IsConlang).IsRequired();
            // Todo: author id
        });

        modelBuilder.Entity<StaticPage>(entity =>
        {
            entity.ToTable("static_pages");
            entity.Property(x => x.Name).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(50);
            entity.Property(x => x.Title).IsRequired();
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("courses");
            entity
                .HasOne(x => x.Language)
                .WithMany(lang => lang.Courses)
                .HasForeignKey(course => course.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.Property(x => x.Title).HasMaxLength(50);
            // Todo: author id
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.ToTable("lessons");
            entity.Property(x => x.Number).IsRequired();
            entity
                .HasOne(x => x.TheoryArticle)
                .WithOne(article => article.Lesson)
                .HasForeignKey<Lesson>(lesson => lesson.TheoryArticleId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(lesson => lesson.Course)
                .WithMany(course => course.Lessons)
                .HasForeignKey(lesson => lesson.CourseId);
        });


        modelBuilder.Entity<LessonProgress>(entity =>
        {
            entity.ToTable("lesson_progresses");
            entity.Property(x => x.LessonId).IsRequired();
            entity.Property(x => x.ProgressLevel).IsRequired();
            // Todo: user id
            entity
                .HasOne(x => x.Lesson)
                .WithMany(lesson => lesson.LessonProgresses)
                .HasForeignKey(x => x.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        });
            

        modelBuilder.Entity<LessonSample>(entity =>
        {
            entity.ToTable("lesson_samples");
            entity.Property(x => x.MinimalProgressLevel).IsRequired();
            entity.HasMany(l => l.SentenceVariants)
                .WithOne(v => v.LessonSample)
                .HasForeignKey(v => v.LessonSampleId);

            entity
                .HasOne(l => l.MainSentenceVariant)
                .WithOne()
                .HasForeignKey<LessonSample>(l => l.MainSentenceVariantId)
                .IsRequired(false);

            entity
                .HasOne(ls => ls.Lesson)
                .WithMany(lesson => lesson.LessonSamples)
                .HasForeignKey(ls => ls.LessonId);
        });

        modelBuilder.Entity<Sentence>(entity =>
        {
            entity.ToTable("sentences");
        });
    }
}
