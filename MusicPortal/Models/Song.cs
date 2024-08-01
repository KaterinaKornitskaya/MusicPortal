namespace MusicPortal.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Path { get; set; }
        public User User { get; set; }
        public Genre Genre { get; set; }
    }
}
