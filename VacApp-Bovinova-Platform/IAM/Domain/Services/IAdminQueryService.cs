using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Queries.AdminQueries;

namespace VacApp_Bovinova_Platform.IAM.Domain.Services;

public interface IAdminQueryService
{
    public Task<Admin?> Handle(GetAdminByIdQuery query);
    public Task<Admin?> Handle(GetAdminByEmailQuery query);
    public Task<IEnumerable<Admin>> Handle(GetAllAdminsQuery query);
}
