using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    public class EditMovieModel : PageModel
    {
        private readonly IMovieService _movieService;

        public EditMovieModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [BindProperty]
        public Movie Movie { get; set; } = new Movie();

        [BindProperty]
        public List<string> SessionTimes { get; set; } = new List<string>();

        [BindProperty]
        public List<decimal?> SessionPrices { get; set; } = new List<decimal?>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Получаем фильм по ID из базы данных
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return RedirectToPage("/Schedule");
            }

            Movie = movie;
            SessionTimes = movie.Sessions.Select(s => s.StartTime.ToString(@"hh\:mm")).ToList();
            SessionPrices = movie.Sessions.Select(s => s.Price).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Создаем сеансы из времени и цены
            Movie.Sessions = SessionTimes
                .Select((time, i) => new Session
                {
                    StartTime = TimeSpan.Parse(time),
                    HallNumber = 1, // По умолчанию зал 1
                    Price = (SessionPrices != null && i < SessionPrices.Count) ? SessionPrices[i] : 500
                })
                .ToList();

            // Обновляем фильм в базе данных
            await _movieService.UpdateMovieAsync(Movie);

            // Перенаправляем на страницу расписания
            return RedirectToPage("/Schedule");
        }
    }
} 