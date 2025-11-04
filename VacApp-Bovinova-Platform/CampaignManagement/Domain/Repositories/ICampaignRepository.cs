using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.ValueObjects;
using VacApp_Bovinova_Platform.Shared.Domain.Repositories;

namespace VacApp_Bovinova_Platform.CampaignManagement.Domain.Repositories;

public interface ICampaignRepository : IBaseRepository<Campaign>
{
    public Task<Campaign?> FindByNameAsync(string name);
    public Task<IEnumerable<Goal>> FindByCampaignId(int campaignId);
    public Task<IEnumerable<Channel>> FindChannelsByCampaignId(int campaignId);
    public Task<IEnumerable<Campaign>> FindByUserIdAsync(CampaignUserId userId);
    public Task<IEnumerable<Campaign>> FindAllAsync();
}
