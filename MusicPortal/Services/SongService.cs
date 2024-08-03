using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using MusicPortal.Repository;

namespace MusicPortal.Services
{
    public class SongService : IMusicRepository<Song>
    {
        public readonly MPContext _context;
        public SongService (MPContext context)
        {
            _context = context;
        }
        public async Task Create(Song entity)
        {
            await _context.Songs.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var songForDelete = _context.Songs.FirstOrDefault(e=>e.Id == id);
            if (songForDelete != null)
            {
                _context.Songs.Remove(songForDelete);              
            }
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Song> GetAll()
        {
            //var songList = _context.Songs.Include(e=>e.Genre).Include(e=>e.User).ToList();
            var songList = _context.Songs.ToList();
            return songList;
        }

        public async Task<Song> GetById(int id)
        {
            var song = await _context.Songs.FirstOrDefaultAsync(e => e.Id == id);
            return song;
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateById(Song song)
        {
            if(song != null)
            {
                _context.Update(song);
                await _context.SaveChangesAsync();
            }
        }

        public List<Genre>? GenreList()
        {
            var genreList = _context.Genres.ToList();
            return genreList;
        }
    }
}
