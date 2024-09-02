namespace MusicPortal.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsConfirmed { get; set; }
        public virtual IEnumerable<Song> Songs { get; set; }  
    }
}
