using MusicPortal.Models;
using MusicPortal.Repository;

namespace MusicPortal.Services
{
    public class SongService : IMusicRepository<Song>
    {
      
        public Task Create(Song entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Song> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Song> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public Task Update(Song entity)
        {
            throw new NotImplementedException();
        }
    }
}
