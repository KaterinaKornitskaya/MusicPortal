namespace MusicPortal.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Path { get; set; }
        public virtual User User { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
