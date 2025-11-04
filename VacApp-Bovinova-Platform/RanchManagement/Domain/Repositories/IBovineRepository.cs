using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;
using VacApp_Bovinova_Platform.Shared.Domain.Repositories;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;

public interface IBovineRepository : IBaseRepository<Bovine>
{
    public Task<Bovine?> FindByNameAsync(string name);
    public Task<IEnumerable<Bovine>> FindByStableIdAsync(int? stableId);
    public Task<IEnumerable<Bovine>> FindByUserIdAsync(RanchUserId userId);
    public Task<int> CountBovinesByStableIdAsync(int stableId);
    public Task<IEnumerable<Bovine>> FindAllAsync();
}
