using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Display(Name = "Жанр")]
        public string Title { get; set; }
        public virtual IEnumerable<Song> Songs { get; set; }
    }
}
