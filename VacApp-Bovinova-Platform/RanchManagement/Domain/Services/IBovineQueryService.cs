using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Queries;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Services;

public interface IBovineQueryService
{
    public Task<IEnumerable<Bovine>> Handle(GetAllBovinesQuery query);

    public Task<Bovine> Handle(GetBovinesByIdQuery query);

    public Task<IEnumerable<Bovine>> Handle(GetBovinesByStableIdQuery query);
}
