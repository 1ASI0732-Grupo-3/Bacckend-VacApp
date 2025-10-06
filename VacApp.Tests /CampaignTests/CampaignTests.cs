// csharp
using System;
using System.Collections.Generic;
using Xunit;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.ValueObjects;

namespace VacApp.Tests.CampaignTests;

public class CampaignTests
{
    [Fact]
    public void Constructor_parametros_asigna_propiedades_correctamente()
    {
        // Arrange
        var name = "Campaña A";
        var description = "Desc A";
        var start = new DateTime(2025, 1, 1);
        var end = new DateTime(2025, 12, 31);
        var status = "Draft";
        var goals = new List<Goal>();
        var channels = new List<Channel>();
        int? stableId = 10;
        CampaignUserId? userId = null;

        // Act
        var sut = new Campaign(name, description, start, end, status, goals, channels, stableId, userId);

        // Assert
        Assert.Equal(name, sut.Name);
        Assert.Equal(description, sut.Description);
        Assert.Equal(start, sut.StartDate);
        Assert.Equal(end, sut.EndDate);
        Assert.Equal(status, sut.Status);
        Assert.Same(goals, sut.Goals);
        Assert.Same(channels, sut.Channels);
        Assert.Equal(stableId, sut.StableId);
        Assert.Null(sut.CampaignUserId);
    }

    [Fact]
    public void Constructor_command_con_userid_nulo_lanza_ArgumentException()
    {
        // Arrange
        var cmd = new CreateCampaignCommand(
            Name: "Campaña B",
            Description: "Desc",
            StartDate: new DateTime(2025, 2, 1),
            EndDate: new DateTime(2025, 3, 1),
            Status: "Draft",
            Goals: new List<Goal>(),
            Channel: new List<Channel>(),
            StableId: null,
            CampaignUserId: null
        );

        // Act
        var ex = Assert.Throws<ArgumentException>(() => new Campaign(cmd));

        // Assert
        Assert.Equal("UserId must be set by the system", ex.Message);
    }

    [Theory]
    [InlineData("Active")]
    [InlineData("Paused")]
    [InlineData("Completed")]
    public void UpdateStatus_cambia_el_estado_de_la_campania(string nuevoEstado)
    {
        // Arrange
        var sut = new Campaign(
            name: "Campaña C",
            description: "Desc",
            startDate: new DateTime(2025, 1, 1),
            endDate: new DateTime(2025, 1, 31),
            status: "Draft",
            goals: new List<Goal>(),
            channels: new List<Channel>(),
            stableId: null,
            campaignUserId: null
        );

        // Act
        sut.UpdateStatus(nuevoEstado);

        // Assert
        Assert.Equal(nuevoEstado, sut.Status);
    }

    [Fact]
    public void Constructor_parametros_preserva_referencias_de_listas()
    {
        // Arrange
        var goals = new List<Goal>();
        var channels = new List<Channel>();

        // Act
        var sut = new Campaign(
            name: "Campaña D",
            description: "Desc",
            startDate: new DateTime(2025, 4, 1),
            endDate: new DateTime(2025, 4, 30),
            status: "Draft",
            goals: goals,
            channels: channels,
            stableId: null,
            campaignUserId: null
        );

        // Assert
        Assert.Same(goals, sut.Goals);
        Assert.Same(channels, sut.Channels);
    }

    [Fact]
    public void AddGoal_agrega_elemento_a_la_coleccion()
    {
        // Arrange
        var sut = new Campaign(
            name: "Campaña E",
            description: "Desc",
            startDate: new DateTime(2025, 5, 1),
            endDate: new DateTime(2025, 5, 31),
            status: "Draft",
            goals: new List<Goal>(),
            channels: new List<Channel>(),
            stableId: null,
            campaignUserId: null
        );
        var countAntes = sut.Goals.Count;

        // Act
        sut.AddGoal(goal: null); // Se valida el contrato actual del agregado (no valida nulos)

        // Assert
        Assert.Equal(countAntes + 1, sut.Goals.Count);
    }

    [Fact]
    public void AddChannel_agrega_elemento_a_la_coleccion()
    {
        // Arrange
        var sut = new Campaign(
            name: "Campaña F",
            description: "Desc",
            startDate: new DateTime(2025, 6, 1),
            endDate: new DateTime(2025, 6, 30),
            status: "Draft",
            goals: new List<Goal>(),
            channels: new List<Channel>(),
            stableId: null,
            campaignUserId: null
        );
        var countAntes = sut.Channels.Count;

        // Act
        sut.AddChannel(channel: null); // Se valida el contrato actual del agregado (no valida nulos)

        // Assert
        Assert.Equal(countAntes + 1, sut.Channels.Count);
    }
}
