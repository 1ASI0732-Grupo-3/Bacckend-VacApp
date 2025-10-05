using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace VacAppTests;


// Para las pruebas de integracion, porfi no tocar xd
[TestFixture]
public abstract class TestSetup
{
    protected DbContextOptions<DbContext> DbContextOptions;

    [SetUp]
    public void BaseSetUp()
    {
        DbContextOptions = new DbContextOptionsBuilder<DbContext>()
            .UseInMemoryDatabase($"TestDb_{Guid.NewGuid()}")
            .Options;
    }
}