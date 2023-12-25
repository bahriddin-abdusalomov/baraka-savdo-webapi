using Baraka_Savdo.Domain.Entities.Companies;
using Baraka_Savdo.Service.Dtos.Companies;

namespace Baraka_Savdo.Service.Interfaces.Companies;

public interface ICompanyService
{
    public Task<bool> CreateAsync(CompanyCreateDto dto);

    public Task<bool> DeleteAsync(long companyId);

    public Task<long> CountAsync();

    public Task<Company> GetByIdAsync(long companyId);

    public Task<IList<Company>> GetAllAsync(PaginationParams @params);

    public Task<bool> UpdateAsync(long companyId, CompanyUpdateDto dto);
}
