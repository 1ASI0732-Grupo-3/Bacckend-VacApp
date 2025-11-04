namespace VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;

public class Goal
{
    public int Id { get; }
    public string Description { get; private set; }
    public string Metric { get; private set; }
    public int TargetValue { get; private set; }
    public int CurrentValue { get; private set; }

    public int CampaignId { get; }

    public Goal()
    {
        Description = string.Empty;
        Metric = string.Empty;
        TargetValue = 0;
        CurrentValue = 0;
    }

    public Goal(string description, string metric, int targetValue, int currentValue, int campaignId)
    {
        Description = description;
        Metric = metric;
        TargetValue = targetValue;
        CurrentValue = currentValue;
        CampaignId = campaignId;
    }

    public void UpdateValues(string description, string metric, int targetValue, int currentValue)
    {
        Description = description;
        Metric = metric;
        TargetValue = targetValue;
        CurrentValue = currentValue;
    }

    /*
    public void AddGoalToCampaign(AddGoalToCampaignCommand command)
    {
        this.Description = command.Description;
        this.Metric = command.Metric;
        this.TargetValue = command.TargetValue;
        this.CurrentValue = command.CurrentValue;
    }
    */
}
