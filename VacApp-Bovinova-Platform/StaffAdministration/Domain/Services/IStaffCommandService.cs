using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Commands;

namespace VacApp_Bovinova_Platform.StaffAdministration.Domain.Services;

public interface IStaffCommandService
{
    public Task<Staff?> Handle(CreateStaffCommand command);

    public Task<Staff?> Handle(UpdateStaffCommand command);

    public Task<Staff?> Handle(DeleteStaffCommand command);
}
