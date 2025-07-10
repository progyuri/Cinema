using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class MovieService : IMovieService
    {
        private readonly CinemaDbContext _context;

        public MovieService(CinemaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies
                .Include(m => m.Sessions)
                .OrderBy(m => m.Title)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> SearchMoviesAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllMoviesAsync();

            var searchLower = searchTerm.ToLower();
            return await _context.Movies
                .Include(m => m.Sessions)
                .Where(movie =>
                    movie.Title.ToLower().Contains(searchLower) ||
                    movie.Director.ToLower().Contains(searchLower) ||
                    movie.Genre.ToLower().Contains(searchLower) ||
                    movie.Description.ToLower().Contains(searchLower) ||
                    movie.Sessions.Any(session => session.StartTime.ToString().Contains(searchLower))
                )
                .OrderBy(m => m.Title)
                .ToListAsync();
        }

        public async Task<Movie?> GetMovieByIdAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.Sessions)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            movie.CreatedDate = DateTime.UtcNow;
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie> UpdateMovieAsync(Movie movie)
        {
            var existingMovie = await _context.Movies
                .Include(m => m.Sessions)
                .FirstOrDefaultAsync(m => m.Id == movie.Id);

            if (existingMovie == null)
                throw new ArgumentException("Фильм не найден");

            existingMovie.Title = movie.Title;
            existingMovie.Director = movie.Director;
            existingMovie.Genre = movie.Genre;
            existingMovie.Description = movie.Description;

            // Обновляем сеансы
            _context.Sessions.RemoveRange(existingMovie.Sessions);
            existingMovie.Sessions = movie.Sessions;

            await _context.SaveChangesAsync();
            return existingMovie;
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return false;

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Session>> GetSessionsByMovieIdAsync(int movieId)
        {
            return await _context.Sessions
                .Where(s => s.MovieId == movieId)
                .OrderBy(s => s.StartTime)
                .ToListAsync();
        }
    }
} 