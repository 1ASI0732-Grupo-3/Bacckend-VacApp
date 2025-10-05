using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using VacApp_Bovinova_Platform.CampaignManagement.Application.Internal.CommandServices;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.CampaignManagement.Infrastructure.Repositories;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Repositories;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.ValueObjects;

namespace VacAppTests.CampaignManagement;

[TestFixture]
public class CampaignCommandServiceTests
{
    private CampaignCommandService _service;
    private AppDbContext _dbContext;
    private CampaignRepository _repository;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"TestDb_{Guid.NewGuid()}")
            .Options;

        _dbContext = new AppDbContext(options);
        _repository = new CampaignRepository(_dbContext);
        _service = new CampaignCommandService(_repository, new UnitOfWork(_dbContext));
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }

[Test]
public async Task Handle_CreateCampaignCommand_ShouldAddCampaign()
{
    // Arrange
    var goals = new List<Goal> { new Goal("Increase Sales", "Sales", 100, 0, 0) };
    var channels = new List<Channel> { new Channel("Email", "email@example.com", 0) };
    var command = new CreateCampaignCommand(
        "Test Campaign",
        "Description",
        DateTime.Now,
        DateTime.Now.AddDays(10),
        "Active",
        goals,
        channels,
        1,
        new CampaignUserId(1)
    );
    
    // Act
    var result = await _service.Handle(command);
    
    // Assert
    Assert.That(result, Is.Not.Null);
    Assert.That(result!.Name, Is.EqualTo("Test Campaign"));
    Assert.That(_dbContext.Campaigns.Count(), Is.EqualTo(1));
}

[Test]
public void Handle_CreateCampaignCommand_ShouldThrowException_WhenDuplicateName()
{
    // Arrange
    var goals = new List<Goal> { new Goal("Increase Sales", "Sales", 100, 0, 0) };
    var channels = new List<Channel> { new Channel("Email", "email@example.com", 0) };
    
    var existingCampaign = new Campaign(
        "NombreDoble",
        "Description",
        DateTime.Now,
        DateTime.Now.AddDays(10),
        "Active",
        goals,
        channels,
        1, 
        new CampaignUserId(1)
    );
    _dbContext.Campaigns.Add(existingCampaign);
    _dbContext.SaveChanges();
    
    var command = new CreateCampaignCommand(
        "NombreDoble",
        "Description",
        DateTime.Now,
        DateTime.Now.AddDays(10),
        "Active",
        goals,
        channels,
        1, 
        new CampaignUserId(1)
    );
    
    // Act & Assert
    Assert.ThrowsAsync<Exception>(() => _service.Handle(command));
}
}