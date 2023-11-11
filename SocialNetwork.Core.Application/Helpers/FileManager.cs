
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Interfaces.Helpers;

namespace SocialNetwork.Core.Application.Helpers
{
    public class FileManager<Entity> : IFileManager<Entity> where Entity : class
    {
        private string root = $"wwwroot/images/{typeof(Entity).Name}/";
        public async Task<string> Save(IFormFile archive)
        {
            if (archive == null || archive.Length == 0)
            {
                throw new ArgumentException("El archivo no es válido.");
            }

            var name = Guid.NewGuid().ToString() + Path.GetExtension(archive.FileName);

            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), root);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var destiny = Path.Combine(directoryPath, name);

            using (var stream = new FileStream(destiny, FileMode.Create))
            {
                await archive.CopyToAsync(stream);
            }
            return name;
        }
        public async Task<string> Update(IFormFile archivo, string imageUrl)
        {
            Delete(imageUrl);

            return await Save(archivo);
        }

        public void Delete( string imageUrl)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), root);
            var fileRoot = Path.Combine(directoryPath, imageUrl);

            if (File.Exists(fileRoot))
            {
                File.Delete(fileRoot);
            }
        }
    }
}
