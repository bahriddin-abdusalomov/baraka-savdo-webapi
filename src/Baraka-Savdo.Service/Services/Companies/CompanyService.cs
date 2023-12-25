using Baraka_Savdo.DataAccess.Interfaces.Companies;
using Baraka_Savdo.Domain.Entities.Companies;
using Baraka_Savdo.Domain.Exceptions.Categories;
using Baraka_Savdo.Domain.Exceptions.Companies;
using Baraka_Savdo.Domain.Exceptions.Files;
using Baraka_Savdo.Service.Common.Helpers;
using Baraka_Savdo.Service.Dtos.Companies;
using Baraka_Savdo.Service.Interfaces.Companies;

namespace Baraka_Savdo.Service.Services.Companies;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IFileService _fileService;
    private readonly IPaginator _paginator;

    public CompanyService(
        ICompanyRepository companyRepository, 
        IFileService fileService, 
        IPaginator paginator)
    {
        _companyRepository = companyRepository;
        _fileService = fileService;
        _paginator = paginator;
    }

    public Task<long> CountAsync()
    {
        return _companyRepository.CountAsync();
    }

    public async Task<bool> CreateAsync(CompanyCreateDto dto)
    {
        string imagePath = await _fileService.UploadImageAsync(dto.ImagePath);
        Company company = new Company()
        {
            ImagePath = imagePath,
            Name = dto.Name,
            Description = dto.Description,
            PhoneNumber = dto.PhoneNumber,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };
        var result = await _companyRepository.CreateAsync(company);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long companyId)
    {
        var company = await _companyRepository.GetByIdAsync(companyId);
        if (company == null) throw new CompanyNotFoundException();

        var categoryImage = await _fileService.DeleteImageAsync(company.ImagePath);
        if (categoryImage == false) throw new ImageNotFoundException();

        var result = await _companyRepository.DeleteAsync(companyId);
        return result > 0;
    }

    public async Task<IList<Company>> GetAllAsync(PaginationParams @params)
    {
        var companies = await _companyRepository.GetAllAsync(@params);
        var count = await _companyRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return companies;
    }

    public async Task<Company> GetByIdAsync(long companyId)
    {
        var company = await _companyRepository.GetByIdAsync(companyId);

        if (company == null) throw new CategoryNotFoundException();
        else return company;
    }

    public async Task<bool> UpdateAsync(long companyId, CompanyUpdateDto dto)
    {
        var company = await _companyRepository.GetByIdAsync(companyId);
        if (company is null) throw new CompanyNotFoundException();

        company.Name = dto.Name;
        company.Description = dto.Description;
        company.PhoneNumber = dto.PhoneNumber;

        if (dto.ImagePath is not null)
        {
            var deleteResult = await _fileService.DeleteImageAsync(company.ImagePath);
            if (deleteResult is false) throw new ImageNotFoundException();

            string newImagePath = await _fileService.UploadImageAsync(dto.ImagePath);

            company.ImagePath = newImagePath;
        }

        company.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _companyRepository.UpdateAsync(companyId, company);
        return dbResult > 0;
    }
}
