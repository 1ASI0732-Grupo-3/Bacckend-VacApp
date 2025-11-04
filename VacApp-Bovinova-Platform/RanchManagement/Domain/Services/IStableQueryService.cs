using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Queries;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Services;

public interface IStableQueryService
{
    public Task<IEnumerable<Stable>> Handle(GetAllStablesQuery query);

    public Task<Stable?> Handle(GetStablesByIdQuery query);

    public Task<Stable?> Handle(GetStableByNameQuery query);
}
