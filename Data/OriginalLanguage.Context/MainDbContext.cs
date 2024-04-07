using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OriginalLanguage.Context.Entities;
using OriginalLanguage.Context.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context;
public class MainDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<ConlangData> ConlangDataEntities { get; set; }
    public DbSet<StaticPage> StaticPages { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<LessonProgress> LessonProgresses { get; set; }
    public DbSet<LessonSample> LessonSamples { get; set; }
    public DbSet<Sentence> Sentences { get; set; }

    public DbSet<ChatMessage> ChatMessages { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppUser>().ToTable("users");
        modelBuilder.Entity<IdentityRole<Guid>>().ToTable("user_roles");
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("user_role_owners");
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("user_role_claims");
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");

        // Todo: move into entity type configurations

        modelBuilder.Entity<Article>(entity =>
        {
            entity.ToTable("articles");
            entity.Property(x => x.Title).IsRequired();

            //entity.Property(x => x.AuthorId).IsRequired();
            entity
                .HasOne(x => x.Author)
                .WithMany(author => author.Articles)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
                //.IsRequired();

            entity
                .HasOne(x => x.Language)
                .WithMany(lang => lang.Articles)
                .HasForeignKey(a => a.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.ToTable("languages");
            entity.Property(x => x.Name).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(50);
            entity.Property(x => x.NativeName).HasMaxLength(50);
            entity.Property(x => x.About).HasMaxLength(3000);
            entity.Property(x => x.AboutNativeSpeakers).HasMaxLength(3000);
            //entity.Property(x => x.IsConlang).IsRequired();

            //entity.Property(x => x.AuthorId).IsRequired();
            entity
                .HasOne(x => x.Author)
                .WithMany(x => x.Languages)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
                //.IsRequired();

            entity
                .HasOne(lang => lang.ConlangData)
                .WithOne(x => x.Language)
                //.HasPrincipalKey<ConlangData>(x => x.Id)
                .HasForeignKey<Language>(lang => lang.ConlangDataId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ConlangData>(entity =>
        {
            entity.ToTable("conlang_data");
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


            //entity.Property(x => x.AuthorId).IsRequired();
            entity
                .HasOne(x => x.Author)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.AuthorId)
                //.IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
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
                .HasForeignKey(lesson => lesson.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<LessonProgress>(entity =>
        {
            entity.ToTable("lesson_progresses");
            entity.Property(x => x.LessonId).IsRequired();
            entity.Property(x => x.ProgressLevel).IsRequired();

            // Unique constraint to ensure that each user
            // has only one progress per lesson
            entity
                .HasAlternateKey(lp => new { lp.LessonId, lp.UserId });
                //.HasKey(x => new { x.UserId, x.LessonId });

            entity
                .HasOne(x => x.Lesson)
                .WithMany(lesson => lesson.LessonProgresses)
                .HasForeignKey(x => x.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            //entity.Property(x => x.UserId).IsRequired();
            entity
                .HasOne(x => x.User)
                .WithMany(x => x.LessonProgresses)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                //.IsRequired();
        });

        modelBuilder.Entity<LessonSample>(entity =>
        {
            entity.ToTable("lesson_samples");
            entity.Property(x => x.MinimalProgressLevel).IsRequired();
            entity.HasMany(l => l.SentenceVariants)
                .WithOne(v => v.LessonSample)
                .HasForeignKey(v => v.LessonSampleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasOne(ls => ls.Lesson)
                .WithMany(lesson => lesson.LessonSamples)
                .HasForeignKey(ls => ls.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Sentence>(entity =>
        {
            entity.ToTable("sentences");
        });

        modelBuilder.Entity<ChatMessage>(chatMessage =>
        {
            chatMessage.ToTable("chat_messages");
        });
    }
}
