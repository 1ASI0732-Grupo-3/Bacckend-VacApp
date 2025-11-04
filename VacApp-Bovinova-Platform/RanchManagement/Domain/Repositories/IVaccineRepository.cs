using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;
using VacApp_Bovinova_Platform.Shared.Domain.Repositories;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;

public interface IVaccineRepository : IBaseRepository<Vaccine>
{
    public Task<Vaccine?> FindByNameAsync(string name);

    public Task<IEnumerable<Vaccine>> FindByBovineIdAsync(int? bovineId);

    public Task<IEnumerable<Vaccine>> FindByUserIdAsync(RanchUserId userId);

    public Task<IEnumerable<Vaccine>> FindAllAsync();
}
