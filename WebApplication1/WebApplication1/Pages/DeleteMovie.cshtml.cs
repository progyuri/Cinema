using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    public class DeleteMovieModel : PageModel
    {
        private readonly IMovieService _movieService;

        public DeleteMovieModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public Movie? Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Получаем фильм по ID из базы данных
            Movie = await _movieService.GetMovieByIdAsync(id);
            
            if (Movie == null)
            {
                // Если фильм не найден, перенаправляем на расписание
                return RedirectToPage("/Schedule");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            // Удаляем фильм из базы данных
            await _movieService.DeleteMovieAsync(id);

            // Перенаправляем на страницу расписания
            return RedirectToPage("/Schedule");
        }
    }
} 