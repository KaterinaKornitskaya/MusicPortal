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
        private IWebHostEnvironment _appEnvironment;
        public SongService (MPContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }
        public async Task Create(Song entity, IFormFile? uploadedFilePath, IFormFile? uploadedFileImage)
        {
            if(uploadedFilePath != null)
            {
                string path = "/audio/" + uploadedFilePath.FileName;       
                using (FileStream fs = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFilePath.CopyToAsync(fs); 
                    entity.Path = path;
                }
            }

            if(uploadedFileImage == null)
            {
                string image = "/image/music.jpg";
                entity.Image = image;
            }

            if(uploadedFileImage != null)
            {
                string image = "/image/" + uploadedFileImage.FileName;
                using (FileStream fs = new FileStream(_appEnvironment.WebRootPath+image, FileMode.Create))
                {
                    await uploadedFileImage.CopyToAsync(fs);
                    entity.Image = image;
                }
            }
            
                
                //using(FileStream fs = new FileStream(_appEnvironment.WebRootPath+image, FileMode.Create))
                //{
                //    await uploadedFileImage.CopyToAsync(fs);
                //    entity.Image = image;
                //}
            

            await _context.Songs.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public User GetUserByEmail(string email)
        {
            var user =  _context.Users.FirstOrDefault(e => e.Email == email);
            return user;
        }

        public async Task DeleteById(int id)
        {
            var songForDelete = await _context.Songs.FirstOrDefaultAsync(e=>e.Id == id);
            if (songForDelete != null)
            {
                _context.Songs.Remove(songForDelete);              
            }
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Song> GetAll()
        {
            var songList = _context.Songs.Include(e=>e.Genre).Include(e=>e.User).ToList();
           // var songList = _context.Songs.ToList();
            return songList;
        }

        public async Task<Song> GetById(int? id)
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
