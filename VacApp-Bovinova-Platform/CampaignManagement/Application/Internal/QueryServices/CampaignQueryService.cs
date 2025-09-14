using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Queries;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.ValueObjects;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Repositories;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Services;

namespace VacApp_Bovinova_Platform.CampaignManagement.Application.Internal.QueryServices;

public class CampaignQueryService(ICampaignRepository campaignRepository,
    IHttpContextAccessor httpContextAccessor) : ICampaignQueryService
{
    public async Task<Campaign?> Handle(GetCampaignByIdQuery query)
    {
        return await campaignRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Campaign>> Handle(GetAllCampaignsQuery query)
    {
        // Extract user type from JWT token
        var userTypeClaim = httpContextAccessor.HttpContext?.User.FindFirst("user_type")?.Value;
        
        // If user_type is Admin, return all campaigns
        if (string.Equals(userTypeClaim, "Admin", StringComparison.OrdinalIgnoreCase))
        {
            return await campaignRepository.FindAllAsync();
        }
        
        // If user is not Admin, filter by userId
        return await campaignRepository.FindByUserIdAsync(new CampaignUserId(query.UserId));
    }

    public async Task<IEnumerable<Goal>> Handle(GetGoalsFromCampaignIdQuery query)
    {
        return await campaignRepository.FindByCampaignId(query.CampaignId);
    }

    public async Task<IEnumerable<Channel>> Handle(GetChannelsFromCampaignIdQuery query)
    {
        return await campaignRepository.FindChannelsByCampaignId(query.CampaignId);
    }
}