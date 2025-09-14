using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Queries.UserQueries;
using VacApp_Bovinova_Platform.IAM.Interfaces.REST.Resources.UserResources;

namespace VacApp_Bovinova_Platform.IAM.Domain.Services
{
    public interface IUserQueryService
    {
        Task<User?> Handle(GetUserByIdQuery query);
        Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
        Task<User?> Handle(GetUserByEmailQuery query);
        Task<User?> Handle(GetUserByNameQuery query);
        Task<string?> GetUserNameByEmail(string? email);
        Task<string?> GetEmailByUserName(string? userName);
        Task<UserInfoResource?> GetUserInfoWithStatsAsync(int userId);
    }
}