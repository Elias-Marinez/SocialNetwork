
using Microsoft.AspNetCore.Http;

namespace SocialNetwork.Core.Application.Interfaces.Helpers
{
    public interface IFileManager<Entity> where Entity : class
    {
        Task<string> Save(IFormFile archivo);
        Task<string> Update(IFormFile archivo, string imageUrl);
        void Delete(string imageUrl);
    }
}
