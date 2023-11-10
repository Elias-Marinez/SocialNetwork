
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Interfaces.Helpers;

namespace SocialNetwork.Core.Application.Helpers
{
    public class FileManager : IFileManager
    {
        private string root = "wwwroot/images/";
        public async Task<string> Save(IFormFile archive, string file)
        {
            var saveroot = root + file;

            if (archive == null || archive.Length == 0)
            {
                throw new ArgumentException("El archivo no es válido.");
            }

            var name = Guid.NewGuid().ToString() + Path.GetExtension(archive.FileName);

            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), saveroot);

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
        public async Task<string> Update(IFormFile archivo, string file, string imageUrl)
        {
            Delete(file, imageUrl);

            return await Save(archivo, file);
        }

        public void Delete(string file, string imageUrl)
        {
            var deleteRoot = root + file;
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), deleteRoot);
            var fileRoot = Path.Combine(directoryPath, imageUrl);

            if (File.Exists(fileRoot))
            {
                File.Delete(fileRoot);
            }
        }
    }
}
