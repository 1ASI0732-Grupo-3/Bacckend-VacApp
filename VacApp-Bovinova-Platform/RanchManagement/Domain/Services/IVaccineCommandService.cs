using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Services;

public interface IVaccineCommandService
{
    public Task<Vaccine?> Handle(CreateVaccineCommand command);

    public Task<Vaccine?> Handle(UpdateVaccineCommand command);

    public Task<Vaccine?> Handle(DeleteVaccineCommand command);
}
