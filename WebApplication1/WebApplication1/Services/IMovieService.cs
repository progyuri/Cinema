using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<IEnumerable<Movie>> SearchMoviesAsync(string searchTerm);
        Task<Movie?> GetMovieByIdAsync(int id);
        Task<Movie> AddMovieAsync(Movie movie);
        Task<Movie> UpdateMovieAsync(Movie movie);
        Task<bool> DeleteMovieAsync(int id);
        Task<IEnumerable<Session>> GetSessionsByMovieIdAsync(int movieId);
    }
} 