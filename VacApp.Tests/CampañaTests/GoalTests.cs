
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;

namespace VacApp.Tests.CampaignTests;

public class GoalTests
{
    [Fact]
    public void Goals_inicialmente_vacia_cuando_se_inyecta_lista_vacia()
    {
        // Arrange
        var goals = new List<Goal>();
        var sut = new Campaign(
            name: "Campaña G",
            description: "Desc",
            startDate: new DateTime(2025, 7, 1),
            endDate: new DateTime(2025, 7, 31),
            status: "Draft",
            goals: goals,
            channels: new List<Channel>(),
            stableId: null,
            campaignUserId: null
        );

        // Act
        var count = sut.Goals.Count;

        // Assert
        Assert.Equal(0, count);
    }

    [Fact]
    public void AddGoal_incrementa_conteo_de_goals()
    {
        // Arrange
        var sut = new Campaign(
            name: "Campaña H",
            description: "Desc",
            startDate: new DateTime(2025, 8, 1),
            endDate: new DateTime(2025, 8, 31),
            status: "Draft",
            goals: new List<Goal>(),
            channels: new List<Channel>(),
            stableId: null,
            campaignUserId: null
        );

        // Act
        sut.AddGoal(goal: null);
        sut.AddGoal(goal: null);

        // Assert
        Assert.Equal(2, sut.Goals.Count);
    }

    [Fact]
    public void Goals_preserva_referencia_compartida()
    {
        // Arrange
        var goals = new List<Goal>();
        var sut = new Campaign(
            name: "Campaña I",
            description: "Desc",
            startDate: new DateTime(2025, 9, 1),
            endDate: new DateTime(2025, 9, 30),
            status: "Draft",
            goals: goals,
            channels: new List<Channel>(),
            stableId: null,
            campaignUserId: null
        );

        // Act
        goals.Add(item: null);

        // Assert
        Assert.Same(goals, sut.Goals);
        Assert.Equal(goals.Count, sut.Goals.Count);
    }
}
