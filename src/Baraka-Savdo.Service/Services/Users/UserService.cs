using Baraka_Savdo.DataAccess.Interfaces.Users;
using Baraka_Savdo.Domain.Entities.Users;
using Baraka_Savdo.Domain.Exceptions.Files;
using Baraka_Savdo.Domain.Exceptions.Users;
using Baraka_Savdo.Service.Common.Helpers;
using Baraka_Savdo.Service.Interfaces.Users;

namespace Baraka_Savdo.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IFileService _fileService;
    private readonly IPaginator _paginator;

    public UserService(
        IUserRepository userRepository,
        IFileService fileService,
        IPaginator paginator)
    {
        _userRepository = userRepository;
        _fileService = fileService;
        _paginator = paginator;
    }

    public async Task<long> CountAsync()
    {
        long count = await _userRepository.CountAsync();
        return count;
    }

    public async Task<bool> CreateAsync(UserCreateDto dto)
    {
        string imagePath = await _fileService.UploadAvatarAsync(dto.ImagePath);
        User user = new User()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            IsMale = dto.IsMale,
            BirthDate = dto.BirthDate,
            Country = dto.Country,
            Region = dto.Region,
            ImagePath = imagePath,
            PhoneNumber = dto.PhoneNumber,
            PhoneNumberConfirmed = dto.PhoneNumberConfirmed,
            PassportSeriaNumber = dto.PassportSeriaNumber,
            PasswordHash = dto.PasswordHash,
            Salt = dto.Salt,
            LastActivity = dto.LastActivity,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var result = await _userRepository.CreateAsync(user);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) throw new UserNotFoundException();

        var userImage = await _fileService.DeleteImageAsync(user.ImagePath);
        if (userImage == false) throw new ImageNotFoundException();

        var result = await _userRepository.DeleteAsync(userId);
        return result > 0;
    }

    public async Task<IList<User>> GetAllAsync(PaginationParams @params)
    {
        var users = await _userRepository.GetAllAsync(@params);
        var count = await _userRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return users;
    }

    public async Task<User> GetByIdAsync(long userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null) throw new UserNotFoundException();

        return user;
    }

    public async Task<IList<User>?> GetByPhoneAsync(string phone)
    {
        var users = await _userRepository.GetByPhoneAsync(phone);
        return users;
    }

    public async Task<(int TModel, List<User>)> SearchAsync(string search, PaginationParams @params)
    {
        var users = await _userRepository.SearchAsync(search, @params);
        return users;
    }

    public async Task<bool> UpdateAsync(long userId, UserUpdateDto dto)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null) throw new UserNotFoundException();

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.IsMale = dto.IsMale;
        user.BirthDate = dto.BirthDate;
        user.Country = dto.Country;
        user.Region = dto.Region;
        user.PhoneNumber = dto.PhoneNumber;
        user.PhoneNumberConfirmed = dto.PhoneNumberConfirmed;
        user.PassportSeriaNumber = dto.PassportSeriaNumber;
        
        if (dto.ImagePath is not null)
        {
            var deleteResult = await _fileService.DeleteImageAsync(user.ImagePath);
            if (!deleteResult) throw new ImageNotFoundException();

            string newImagePath = await _fileService.UploadImageAsync(dto.ImagePath);
            user.ImagePath = newImagePath;
        }

        user.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _userRepository.UpdateAsync(userId, user);
        return dbResult > 0; ;
    }
}
