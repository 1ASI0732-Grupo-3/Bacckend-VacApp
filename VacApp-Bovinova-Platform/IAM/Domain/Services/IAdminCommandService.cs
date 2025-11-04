using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands.AdminCommands;

namespace VacApp_Bovinova_Platform.IAM.Domain.Services;

public interface IAdminCommandService
{
    public Task<string> Handle(CreateAdminCommand command);
    public Task<string> Handle(AdminSignInCommand command);
    public Task<bool> Handle(UpdateAdminCommand command, int adminId);
    public Task<bool> DeleteAdminAsync(int adminId);
}
