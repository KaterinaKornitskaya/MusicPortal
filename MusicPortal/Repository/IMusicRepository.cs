using MusicPortal.Models;

namespace MusicPortal.Repository
{
    public interface IMusicRepository<T> where T : class
    {
        IEnumerable<T> GetAll();     
        Task Create (T entity, IFormFile? uploadedFile, IFormFile? uploadedFile2);
        Task UpdateById (T entity);
        Task DeleteById (int id);
        Task<T> GetById (int? id);
        Task Save();
        List<Genre>? GenreList();
        User GetUserByEmail(string email);
    }
}
