using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Добавляем поддержку переменных окружения
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddRazorPages();

// Добавляем контекст базы данных PostgreSQL с поддержкой переменных окружения
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Всегда создаем строку подключения из переменных окружения для Railway
var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
var database = Environment.GetEnvironmentVariable("DB_NAME") ?? "cinema_db";
var username = Environment.GetEnvironmentVariable("DB_USER") ?? "postgres";
var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";
var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";

// Выводим отладочную информацию
Console.WriteLine($"DB_HOST: {host}");
Console.WriteLine($"DB_NAME: {database}");
Console.WriteLine($"DB_USER: {username}");
Console.WriteLine($"DB_PASSWORD: {(string.IsNullOrEmpty(password) ? "empty" : "set")}");
Console.WriteLine($"DB_PORT: {port}");

connectionString = $"Host={host};Database={database};Username={username};Password={password};Port={port}";
Console.WriteLine($"Connection String: {connectionString}");

builder.Services.AddDbContext<CinemaDbContext>(options =>
    options.UseNpgsql(connectionString));

// Добавляем сервис для работы с фильмами
builder.Services.AddScoped<IMovieService, MovieService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Для Railway - используем HTTP вместо HTTPS в продакшене
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// Применяем миграции при запуске
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<CinemaDbContext>();
        Console.WriteLine("Attempting to apply database migrations...");
        context.Database.Migrate();
        Console.WriteLine("Database migrations applied successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error applying migrations: {ex.Message}");
        Console.WriteLine($"Stack trace: {ex.StackTrace}");
        // Не прерываем запуск приложения при ошибке миграции
    }
}

app.Run();
