using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class Song
    {
        public int Id { get; set; }

        [Display(Name = "Назва пісні")]
        public string Title { get; set; }

        [Display(Name = "Автор")]
        public string Author { get; set; }

        public string Path { get; set; }
        public string? Image {  get; set; }

        public virtual User User { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
