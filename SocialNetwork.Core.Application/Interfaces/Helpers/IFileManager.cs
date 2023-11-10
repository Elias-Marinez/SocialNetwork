
using Microsoft.AspNetCore.Http;

namespace SocialNetwork.Core.Application.Interfaces.Helpers
{
    public interface IFileManager
    {
        Task<string> Save(IFormFile archivo, string file);
        Task<string> Update(IFormFile archivo, string file, string imageUrl);
        void Delete(string file, string imageUrl);
    }
}
