using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.Shared.Domain.Repositories;

namespace VacApp_Bovinova_Platform.IAM.Domain.Repositories;

public interface IAdminRepository : IBaseRepository<Admin>
{
    public Task<Admin?> FindByEmailAsync(string email);
    public Task<IEnumerable<Admin>> FindAllAsync();
    public Task UpdateAsync(Admin admin);
    public Task DeleteAsync(Admin admin);
}
