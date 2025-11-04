using VacApp_Bovinova_Platform.Shared.Domain.Repositories;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.ValueObjects;

namespace VacApp_Bovinova_Platform.StaffAdministration.Domain.Repositories;

public interface IStaffRepository : IBaseRepository<Staff>
{
    public Task<Staff?> FindByNameAsync(string name);

    public Task<IEnumerable<Staff>> FindByCampaignIdAsync(int? campaignId);

    public Task<IEnumerable<Staff>> FindByEmployeeStatusAsync(int employeeStatus);

    public Task<IEnumerable<Staff>> FindByUserIdAsync(StaffUserId userId);

    public Task<IEnumerable<Staff>> FindAllAsync();
}
