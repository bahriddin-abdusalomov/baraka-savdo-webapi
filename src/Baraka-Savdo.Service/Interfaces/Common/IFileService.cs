namespace Baraka_Savdo.Service.Interfaces.Common;

public interface IFileService
{
    public Task<string> UploadImageAsync(IFormFile file);
    public Task<bool> DeleteImageAsync(string file);

    public Task<string> UploadAvatarAsync(IFormFile file);
    public Task<bool> DeleteAvatarAsync(string file);

}
