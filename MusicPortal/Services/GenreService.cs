using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using MusicPortal.Repository;

namespace MusicPortal.Services
{
    public class GenreService : IMusicRepository<Genre>
    {
        public readonly MPContext _context;
        public GenreService(MPContext context)
        {
            _context = context;
        }
        public async Task Create(Genre entity)
        {
           await _context.Genres.AddAsync(entity);
           await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            //var genreForDelete = await _context.Genres.FindAsync(entity);
            var genreForDelete = await _context.Genres.FirstOrDefaultAsync(e=>e.Id == id);
            if (genreForDelete != null)
            {
                _context.Genres.Remove(genreForDelete);
            }
            await _context.SaveChangesAsync();
        }
        public async Task UpdateById(Genre genre)
        {
            //var genreForEdit = await _context.Genres.FirstOrDefaultAsync(e => e.Id==id);
            if (genre != null)
            {
                _context.Genres.Update(genre);
            }
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Genre> GetAll()
        {
            var genresList = _context.Genres.ToList();
            return genresList;
        }

        public async Task<Genre> GetById(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(e => e.Id == id);
            return genre;
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public List<Genre>? GenreList()
        {
            var genreList = _context.Genres.ToList();
            return genreList;
        }
    }
}
