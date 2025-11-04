using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands.UserCommands;

namespace VacApp_Bovinova_Platform.IAM.Domain.Services;

public interface IUserCommandService
{
    public Task<string> Handle(SignUpCommand command);
    public Task<string> Handle(SignInCommand command);
    public Task UpdateUserAsync(User user);
    public Task<bool> Handle(UpdateUserCommand command, int userId);
    public Task<bool> Handle(DeleteUserCommand command);
}
