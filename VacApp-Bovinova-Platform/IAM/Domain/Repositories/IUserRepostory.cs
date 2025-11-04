using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.Shared.Domain.Repositories;

namespace VacApp_Bovinova_Platform.IAM.Domain.Repositories;

public interface IUserRepostory : IBaseRepository<User>
{
    public Task<User?> FindByEmailAsync(string? email);

    public Task<User?> FindByNameAsync(string? name);

    public Task<IEnumerable<User>> FindAllAsync();

    public Task UpdateAsync(User user);

    public Task DeleteAsync(User user);
}
