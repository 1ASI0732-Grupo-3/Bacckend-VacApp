using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Queries;

namespace VacApp_Bovinova_Platform.StaffAdministration.Domain.Services;

public interface IStaffQueryService
{
    public Task<IEnumerable<Staff>> Handle(GetAllStaffQuery query);

    public Task<Staff?> Handle(GetStaffByIdQuery query);

    public Task<IEnumerable<Staff>> Handle(GetStaffByCampaignIdQuery query);

    public Task<IEnumerable<Staff>> Handle(GetStaffByEmployeeStatusQuery query);

    public Task<Staff?> Handle(GetStaffByNameQuery query);
}
