using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using VacApp_Bovinova_Platform.RanchManagement.Application.Internal.CommandServices;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Infrastructure.Persistence.EFC.Repositories;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace VacAppTests.RanchManagement;

[TestFixture]
public class BovineCommandServiceTests
{
    private BovineCommandService _service;
    private AppDbContext _dbContext;
    private BovineRepository _bovineRepository;
    private StableRepository _stableRepository;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"TestDb_{Guid.NewGuid()}")
            .Options;

        _dbContext = new AppDbContext(options);
        _bovineRepository = new BovineRepository(_dbContext);
        _stableRepository = new StableRepository(_dbContext);
        _service = new BovineCommandService(_bovineRepository, _stableRepository, null, new UnitOfWork(_dbContext));
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }

    [Test]
    public void Handle_CreateBovineCommand_ShouldAddBovine()
    {
        // Arrange
        var stable = new Stable(new CreateStableCommand("Stable A", 10, new RanchUserId(1)));
        _dbContext.Stables.Add(stable);
        _dbContext.SaveChanges();
        var command = new CreateBovineCommand(
            "Test Bovine",
            "Male",
            DateTime.Now,
            "Breed",
            "Location",
            null,
            stable.Id,
            new RanchUserId(1),
            null
        );

        // Act
        var result = _service.Handle(command).Result;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Test Bovine", result.Name);
        Assert.AreEqual(1, _dbContext.Bovines.Count());
    }

    [Test]
public void Handle_CreateBovineCommand_ShouldThrowException_WhenStableFull()
{
    // Arrange
    var stable = new Stable(new CreateStableCommand("Stable B", 2, new RanchUserId(1)));
    _dbContext.Stables.Add(stable);
    _dbContext.SaveChanges();
    
    var bovine1 = new Bovine(new CreateBovineCommand(
        "Bovine 1",
        "Male",
        DateTime.Now,
        "Breed",
        "Location",
        "https://example.com/image1.jpg", 
        stable.Id,
        new RanchUserId(1),
        null
    ));
    var bovine2 = new Bovine(new CreateBovineCommand(
        "Bovine 2",
        "Female",
        DateTime.Now,
        "Breed",
        "Location",
        "https://example.com/image2.jpg",
        stable.Id,
        new RanchUserId(1),
        null
    ));
    
    _dbContext.Bovines.Add(bovine1);
    _dbContext.Bovines.Add(bovine2);
    _dbContext.SaveChanges();
    
    var command = new CreateBovineCommand(
        "Test Bovine",
        "Male",
        DateTime.Now,
        "Breed",
        "Location",
        "https://example.com/image3.jpg",
        stable.Id,
        new RanchUserId(1),
        null
    );
    
    // Act & Assert
    Assert.ThrowsAsync<Exception>(() => _service.Handle(command));
}
}