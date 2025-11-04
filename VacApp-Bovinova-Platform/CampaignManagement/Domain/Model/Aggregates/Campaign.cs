using System.ComponentModel.DataAnnotations.Schema;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.ValueObjects;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;

namespace VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;

public class Campaign
{
    public int Id { get; }
    public string Name { get; }
    public string Description { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    public string Status { get; private set; }
    public ICollection<Goal> Goals { get; }
    public int GoalId { get; }
    public ICollection<Channel> Channels { get; }

    /// <summary>Stable FK</summary>
    public int? StableId { get; private set; }
    [ForeignKey(nameof(StableId))]
    public Stable? Stable { get; }

    /// <summary>User Identifier As Foreign Key</summary>
    public CampaignUserId? CampaignUserId { get; set; }

    protected Campaign()
    {
        Name = string.Empty;
        Description = string.Empty;
        StartDate = DateTime.Now;
        EndDate = DateTime.Now;
        Status = string.Empty;
        Goals = new List<Goal>();
        Channels = new List<Channel>();
    }

    public Campaign(
        string name,
        string description,
        DateTime startDate,
        DateTime endDate,
        string status,
        ICollection<Goal> goals,
        ICollection<Channel> channels,
        int? stableId,
        CampaignUserId? campaignUserId)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Status = status;
        Goals = goals;
        Channels = channels;
        StableId = stableId;
        CampaignUserId = campaignUserId;
    }

    public Campaign(CreateCampaignCommand command)
    {
        Name = command.Name;
        Description = command.Description;
        StartDate = command.StartDate;
        EndDate = command.EndDate;
        Status = command.Status;
        Goals = command.Goals;
        Channels = command.Channel;
        StableId = command.StableId;
        CampaignUserId = command.CampaignUserId ?? throw new ArgumentException("UserId must be set by the system");
    }

    public void UpdateStatus(string status)
    {
        Status = status;
    }

    public void AddGoal(Goal goal)
    {
        //Goal _goal = new Goal(goal.Description, goal.Metric, goal.TargetValue, goal.CurrentValue);
        //this.Goal.UpdateValues(goal.Description, goal.Metric, goal.TargetValue, goal.CurrentValue);  
        //this.Goal.UpdateValues(description, metric, targetValue, currentValue);
        Goals.Add(goal);
    }

    public void AddChannel(Channel channel)
    {
        Channels.Add(channel);
    }
}
