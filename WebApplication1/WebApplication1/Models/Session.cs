using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("sessions")]
    public class Session
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("movie_id")]
        public int MovieId { get; set; }

        [Required]
        [Column("start_time")]
        public TimeSpan StartTime { get; set; }

        [Column("hall_number")]
        public int? HallNumber { get; set; }

        [Column("price", TypeName = "decimal(10,2)")]
        public decimal? Price { get; set; }

        // Навигационное свойство для фильма
        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; } = null!;
    }
} 