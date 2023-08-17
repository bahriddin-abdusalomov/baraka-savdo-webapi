using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.Service.Interfaces.Common
{
    public interface IFileService
    {
        public Task<string> UploadImageAsync(IFormFile file);
        public Task<string> DeleteImageAsync(string file);

        public Task<string> UploadAvatarAsync(IFormFile file);
        public Task<string> DeleteAvatarAsync(string file);

    }
}
