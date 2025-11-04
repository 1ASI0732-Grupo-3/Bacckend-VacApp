using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Commands;

namespace VacApp_Bovinova_Platform.CampaignManagement.Domain.Services;

public interface ICampaignCommandService
{
    public Task<Campaign?> Handle(CreateCampaignCommand command);

    public Task<IEnumerable<Campaign>> Handle(DeleteCampaignCommand command);

    public Task<Campaign?> Handle(UpdateCampaignStatusCommand command);

    public Task<Campaign?> Handle(AddGoalToCampaignCommand command);

    public Task<Campaign?> Handle(AddChannelToCampaignCommand command);

    public Task<Goal?> Handle(UpdateGoalCommand command);
}
