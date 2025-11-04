using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Queries;

namespace VacApp_Bovinova_Platform.CampaignManagement.Domain.Services;

public interface ICampaignQueryService
{
    public Task<Campaign?> Handle(GetCampaignByIdQuery query);

    public Task<IEnumerable<Campaign>> Handle(GetAllCampaignsQuery query);

    public Task<IEnumerable<Goal>> Handle(GetGoalsFromCampaignIdQuery query);

    public Task<IEnumerable<Channel>> Handle(GetChannelsFromCampaignIdQuery query);
}
