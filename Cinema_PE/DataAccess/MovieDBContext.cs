using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace Cinema_PE.DataAccess
{
    public partial class MovieDBContext : DbContext
    {
        public MovieDBContext()
        {
        }

        public MovieDBContext(DbContextOptions<MovieDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyDb"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.Image)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.HasKey(e => new { e.MovieId, e.MemberId });

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.Time).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
