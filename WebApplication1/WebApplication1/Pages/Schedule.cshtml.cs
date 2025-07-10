using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    public class ScheduleModel : PageModel
    {
        private readonly IMovieService _movieService;

        public ScheduleModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public IEnumerable<Movie> Movies { get; set; } = new List<Movie>();
        public string SearchTerm { get; set; } = string.Empty;

        public async Task OnGetAsync(string searchTerm)
        {
            SearchTerm = searchTerm ?? string.Empty;
            
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                Movies = await _movieService.GetAllMoviesAsync();
            }
            else
            {
                Movies = await _movieService.SearchMoviesAsync(SearchTerm);
            }
        }
    }
} 