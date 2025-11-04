using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Services;

public interface IStableCommandService
{
    public Task<Stable?> Handle(CreateStableCommand command);

    //Update
    public Task<Stable?> Handle(UpdateStableCommand command);

    //Delete
    public Task<Stable?> Handle(DeleteStableCommand command);
}
