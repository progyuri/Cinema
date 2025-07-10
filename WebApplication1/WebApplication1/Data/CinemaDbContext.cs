using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфигурация для Movie
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Director).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Genre).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Конфигурация для Session
            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.MovieId).IsRequired();
                entity.Property(e => e.StartTime).IsRequired();
                entity.Property(e => e.Price).HasColumnType("decimal(10,2)");

                // Связь с Movie
                entity.HasOne(e => e.Movie)
                      .WithMany(m => m.Sessions)
                      .HasForeignKey(e => e.MovieId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Добавляем начальные данные
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Добавляем фильмы
            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Дюна: Часть вторая", Director = "Дени Вильнёв", Genre = "Фантастика", Description = "Продолжение эпической саги о пустынной планете Арракис.", CreatedDate = DateTime.UtcNow },
                new Movie { Id = 2, Title = "Кунг-фу Панда 4", Director = "Майк Митчелл", Genre = "Мультфильм", Description = "Продолжение приключений По и его друзей.", CreatedDate = DateTime.UtcNow },
                new Movie { Id = 3, Title = "Безумный Макс: Фуриоса", Director = "Джордж Миллер", Genre = "Боевик", Description = "Приквел к культовой франшизе о постапокалиптическом мире.", CreatedDate = DateTime.UtcNow },
                new Movie { Id = 4, Title = "Интерстеллар", Director = "Кристофер Нолан", Genre = "Научная фантастика", Description = "Эпическая история о космическом путешествии для спасения человечества.", CreatedDate = DateTime.UtcNow },
                new Movie { Id = 5, Title = "Титаник", Director = "Джеймс Кэмерон", Genre = "Драма", Description = "История любви на фоне трагического крушения легендарного лайнера.", CreatedDate = DateTime.UtcNow }
            );

            // Добавляем сеансы
            modelBuilder.Entity<Session>().HasData(
                new Session { Id = 1, MovieId = 1, StartTime = new TimeSpan(12, 0, 0), HallNumber = 1, Price = 500 },
                new Session { Id = 2, MovieId = 1, StartTime = new TimeSpan(15, 30, 0), HallNumber = 1, Price = 500 },
                new Session { Id = 3, MovieId = 1, StartTime = new TimeSpan(19, 0, 0), HallNumber = 1, Price = 600 },
                new Session { Id = 4, MovieId = 2, StartTime = new TimeSpan(10, 0, 0), HallNumber = 2, Price = 400 },
                new Session { Id = 5, MovieId = 2, StartTime = new TimeSpan(13, 0, 0), HallNumber = 2, Price = 400 },
                new Session { Id = 6, MovieId = 2, StartTime = new TimeSpan(17, 0, 0), HallNumber = 2, Price = 450 },
                new Session { Id = 7, MovieId = 3, StartTime = new TimeSpan(14, 0, 0), HallNumber = 3, Price = 550 },
                new Session { Id = 8, MovieId = 3, StartTime = new TimeSpan(18, 30, 0), HallNumber = 3, Price = 550 },
                new Session { Id = 9, MovieId = 3, StartTime = new TimeSpan(21, 0, 0), HallNumber = 3, Price = 600 },
                new Session { Id = 10, MovieId = 4, StartTime = new TimeSpan(11, 0, 0), HallNumber = 1, Price = 500 },
                new Session { Id = 11, MovieId = 4, StartTime = new TimeSpan(16, 0, 0), HallNumber = 1, Price = 500 },
                new Session { Id = 12, MovieId = 4, StartTime = new TimeSpan(20, 30, 0), HallNumber = 1, Price = 600 },
                new Session { Id = 13, MovieId = 5, StartTime = new TimeSpan(13, 30, 0), HallNumber = 2, Price = 450 },
                new Session { Id = 14, MovieId = 5, StartTime = new TimeSpan(17, 30, 0), HallNumber = 2, Price = 450 },
                new Session { Id = 15, MovieId = 5, StartTime = new TimeSpan(21, 30, 0), HallNumber = 2, Price = 500 }
            );
        }
    }
} 