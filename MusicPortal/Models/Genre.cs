namespace MusicPortal.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual IEnumerable<Song> Songs { get; set; }
    }
}
