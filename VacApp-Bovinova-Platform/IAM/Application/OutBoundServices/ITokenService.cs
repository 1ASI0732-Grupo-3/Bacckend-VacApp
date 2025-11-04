using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;

namespace VacApp_Bovinova_Platform.IAM.Application.OutBoundServices;

public interface ITokenService
{
    public string GenerateToken(User user);
    public Task<int?> ValidateToken(string token);
    public string GenerateToken(Admin admin);
}
