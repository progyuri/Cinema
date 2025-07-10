# Настройка PostgreSQL для проекта кинотеатра

## 1. Установка PostgreSQL

### Windows:
1. Скачайте PostgreSQL с официального сайта: https://www.postgresql.org/download/windows/
2. Запустите установщик и следуйте инструкциям
3. Запомните пароль для пользователя `postgres` - "pos123"

### macOS:
```bash
brew install postgresql
brew services start postgresql
```

### Linux (Ubuntu/Debian):
```bash
sudo apt update
sudo apt install postgresql postgresql-contrib
sudo systemctl start postgresql
sudo systemctl enable postgresql
```

## 2. Создание базы данных

1. Подключитесь к PostgreSQL:
```bash
psql -U postgres
```

2. Создайте базу данных:
```sql
CREATE DATABASE cinema_db;
```

3. Выйдите из psql:
```sql
\q
```

## 3. Настройка строки подключения

Отредактируйте файл `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=cinema_db;Username=postgres;Password=pos123"
  }
}
```

Замените `ВАШ_ПАРОЛЬ` на пароль, который вы указали при установке PostgreSQL.

## 4. Создание миграций и обновление базы данных

Выполните следующие команды в терминале в папке проекта:

```bash
# Создание миграции
dotnet ef migrations add InitialCreate

# Применение миграции к базе данных
dotnet ef database update
```

## 5. Проверка подключения

Запустите приложение:
```bash
dotnet run
```

Если все настроено правильно, приложение запустится и будет использовать PostgreSQL для хранения данных.

## 6. Полезные команды PostgreSQL

```sql
-- Подключение к базе данных
\c cinema_db

-- Просмотр таблиц
\dt

-- Просмотр данных в таблице movies
SELECT * FROM movies;

-- Просмотр данных в таблице sessions
SELECT * FROM sessions;

-- Просмотр фильмов с сеансами
SELECT m.title, s.start_time, s.hall_number, s.price 
FROM movies m 
JOIN sessions s ON m.id = s.movie_id;
```

## 7. Устранение неполадок

### Ошибка подключения:
- Проверьте, что PostgreSQL запущен
- Проверьте правильность пароля в строке подключения
- Убедитесь, что база данных `cinema_db` создана

### Ошибка миграции:
- Удалите папку `Migrations` и создайте миграцию заново
- Проверьте, что все модели правильно настроены 