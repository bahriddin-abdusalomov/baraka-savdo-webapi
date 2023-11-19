using Baraka_Savdo.Service.Interfaces.Common;
using Microsoft.AspNetCore.Http;
using Baraka_Savdo.Service.Common.Helpers;
using Microsoft.AspNetCore.Hosting;

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
            public Task<bool> DeleteAvatarAsync(string file)
            {
                throw new NotImplementedException();
            }

            public async Task<bool> DeleteImageAsync(string imagePath)
            {
                string path = Path.Combine(ROOTPATH, imagePath);

                if(File.Exists(path))
                {
                   await  Task.Run(() =>
                        {
                            File.Delete(path);
                        });
                   return true;
                }
                else
                {
                   return false;
                }

            }

            public Task<string> UploadAvatarAsync(IFormFile file)
            {
                throw new NotImplementedException();
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
