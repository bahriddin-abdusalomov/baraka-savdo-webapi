using Baraka_Savdo.Service.Interfaces.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Baraka_Savdo.Service.Common.Helpers;

namespace Baraka_Savdo.Service.Services.Common
{
    public class FileService : IFileService
    {
        private readonly string MEDIA = "media";
        private readonly string IMAGES = "images";
        private readonly string AVATARS = "avatars";
        private readonly string ROOTPATH;

        public FileService(IWebHostEnvironment env)
        {
            this.ROOTPATH = env.WebRootPath;
        }
        public Task<string> DeleteAvatarAsync(string file)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteImageAsync(string file)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadAvatarAsync(IFormFile file)
        {
            return " sa";
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            string newImageName = MediaHelper.MakeImageName(file.FileName);
            string subPath = Path.Combine(MEDIA, IMAGES, newImageName);

            string path = Path.Combine(ROOTPATH, subPath);

            FileStream fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
            fileStream.Close();

            return subPath;
        }
    }
}
