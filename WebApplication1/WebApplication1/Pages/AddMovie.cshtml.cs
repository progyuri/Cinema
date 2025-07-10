using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    public class AddMovieModel : PageModel
    {
        private readonly IMovieService _movieService;

        public AddMovieModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [BindProperty]
        public Movie Movie { get; set; } = new Movie();

        [BindProperty]
        public List<string> SessionTimes { get; set; } = new List<string> { "" };

        [BindProperty]
        public List<decimal?> SessionPrices { get; set; } = new List<decimal?> { null };

        public void OnGet()
        {
            // Страница загружается с пустой формой
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

            // Добавляем новый фильм в базу данных
            await _movieService.AddMovieAsync(Movie);

            // Перенаправляем на страницу расписания
            return RedirectToPage("/Schedule");
        }
    }
} 