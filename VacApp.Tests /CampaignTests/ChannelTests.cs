
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;

namespace VacApp.Tests.CampaignTests;

public class ChannelTests
{
    [Fact]
    public void Channels_inicialmente_vacia_cuando_se_inyecta_lista_vacia()
    {
        // Arrange
        var channels = new List<Channel>();
        var sut = new Campaign(
            name: "Campaña J",
            description: "Desc",
            startDate: new DateTime(2025, 10, 1),
            endDate: new DateTime(2025, 10, 31),
            status: "Draft",
            goals: new List<Goal>(),
            channels: channels,
            stableId: null,
            campaignUserId: null
        );

        // Act
        var count = sut.Channels.Count;

        // Assert
        Assert.Equal(0, count);
    }

    [Fact]
    public void AddChannel_incrementa_conteo_de_channels()
    {
        // Arrange
        var sut = new Campaign(
            name: "Campaña K",
            description: "Desc",
            startDate: new DateTime(2025, 11, 1),
            endDate: new DateTime(2025, 11, 30),
            status: "Draft",
            goals: new List<Goal>(),
            channels: new List<Channel>(),
            stableId: null,
            campaignUserId: null
        );

        // Act
        sut.AddChannel(channel: null);
        sut.AddChannel(channel: null);

        // Assert
        Assert.Equal(2, sut.Channels.Count);
    }

    [Fact]
    public void Channels_preserva_referencia_compartida()
    {
        // Arrange
        var channels = new List<Channel>();
        var sut = new Campaign(
            name: "Campaña L",
            description: "Desc",
            startDate: new DateTime(2025, 12, 1),
            endDate: new DateTime(2025, 12, 31),
            status: "Draft",
            goals: new List<Goal>(),
            channels: channels,
            stableId: null,
            campaignUserId: null
        );

        // Act
        channels.Add(item: null);

        // Assert
        Assert.Same(channels, sut.Channels);
        Assert.Equal(channels.Count, sut.Channels.Count);
    }
}
