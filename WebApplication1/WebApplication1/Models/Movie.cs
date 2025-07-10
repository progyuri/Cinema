using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("movies")]
    public class Movie
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("title")]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Column("director")]
        [StringLength(100)]
        public string Director { get; set; } = string.Empty;

        [Required]
        [Column("genre")]
        [StringLength(50)]
        public string Genre { get; set; } = string.Empty;

        [Column("description")]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Навигационное свойство для сеансов
        public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
} 