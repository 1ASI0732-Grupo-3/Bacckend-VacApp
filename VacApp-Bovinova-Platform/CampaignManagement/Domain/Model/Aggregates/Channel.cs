namespace VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;

public class Channel
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Details { get; set; }

    public int CampaignId { get; private set; }

    public Channel()
    {
        Type = string.Empty;
        Details = string.Empty;
    }

    public Channel(string Type, string Details, int campaignId)
    {
        this.Type = Type;
        this.Details = Details;
        CampaignId = campaignId;
    }
}
