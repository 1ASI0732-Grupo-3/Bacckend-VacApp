using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Queries.UserQueries;
using VacApp_Bovinova_Platform.IAM.Interfaces.REST.Resources.UserResources;

namespace VacApp_Bovinova_Platform.IAM.Domain.Services;

public interface IUserQueryService
{
    public Task<User?> Handle(GetUserByIdQuery query);
    public Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
    public Task<User?> Handle(GetUserByEmailQuery query);
    public Task<User?> Handle(GetUserByNameQuery query);
    public Task<string?> GetUserNameByEmail(string? email);
    public Task<string?> GetEmailByUserName(string? userName);
    public Task<UserInfoResource?> GetUserInfoWithStatsAsync(int userId);
}
