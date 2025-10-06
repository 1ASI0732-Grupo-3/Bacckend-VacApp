using Xunit.Abstractions;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;

namespace VacApp.Tests.RanchTests;

public class StableTests
{
    private readonly ITestOutputHelper _output;
    public StableTests(ITestOutputHelper output) => _output = output;

    [Fact]
    public void Constructor_con_limit_invalido_lanza_ArgumentException()
    {
        // Arrange
        var cmd = new CreateStableCommand(Limit: 0, Name: "Establo X", RanchUserId: new RanchUserId(1));

        // Act
        var ex = Assert.Throws<ArgumentException>(() => new Stable(cmd));

        // Assert
        Assert.Equal("Limit must be greater than 0", ex.Message);
        _output.WriteLine("AAA -> Assert: Excepción esperada por límite no válido (<= 0) al crear Stable.");
    }

    [Fact]
    public void Constructor_con_nombre_vacio_lanza_ArgumentException()
    {
        // Arrange
        var cmd = new CreateStableCommand(Limit: 10, Name: string.Empty, RanchUserId: new RanchUserId(1));

        // Act
        var ex = Assert.Throws<ArgumentException>(() => new Stable(cmd));

        // Assert
        Assert.Equal("Name must not be empty", ex.Message);
        _output.WriteLine("AAA -> Assert: Excepción esperada por nombre vacío al crear Stable.");
    }

    [Fact]
    public void Constructor_con_userid_nulo_lanza_ArgumentException()
    {
        // Arrange
        var cmd = new CreateStableCommand(Limit: 10, Name: "Establo A", RanchUserId: null);

        // Act
        var ex = Assert.Throws<ArgumentException>(() => new Stable(cmd));

        // Assert
        Assert.Equal("RanchUserId must be set by the system", ex.Message);
        _output.WriteLine("AAA -> Assert: Excepción esperada por RanchUserId nulo al crear Stable.");
    }

    [Fact]
    public void Constructor_valido_asigna_propiedades()
    {
        // Arrange
        var cmd = new CreateStableCommand(Limit: 50, Name: "Establo A", RanchUserId: new RanchUserId(1));

        // Act
        var sut = new Stable(cmd);

        // Assert
        Assert.Equal(50, sut.Limit);
        Assert.Equal("Establo A", sut.Name);
        _output.WriteLine($"AAA -> Assert: Stable creado correctamente. Name={sut.Name}, Limit={sut.Limit}");
    }

    [Fact]
    public void Update_con_datos_invalidos_lanza_ArgumentException()
    {
        // Arrange
        var sut = new Stable(new CreateStableCommand(Limit: 10, Name: "Establo B", RanchUserId: new RanchUserId(1)));
        var update = new UpdateStableCommand(Limit: -1, Name: "Establo B2", Id: sut.Id);

        // Act
        var ex = Assert.Throws<ArgumentException>(() => sut.Update(update));

        // Assert
        Assert.Equal("Limit must be greater than 0", ex.Message);
        _output.WriteLine("AAA -> Assert: Excepción esperada en Update por límite no válido (<= 0).");
    }

    [Fact]
    public void Update_con_datos_validos_actualiza_propiedades()
    {
        // Arrange
        var sut = new Stable(new CreateStableCommand(Limit: 10, Name: "Establo B", RanchUserId: new RanchUserId(1)));
        var update = new UpdateStableCommand(Limit: 80, Name: "Establo B2", Id: sut.Id);

        // Act
        sut.Update(update);

        // Assert
        Assert.Equal(80, sut.Limit);
        Assert.Equal("Establo B2", sut.Name);
        _output.WriteLine($"AAA -> Assert: Update correcto. Name={sut.Name}, Limit={sut.Limit}");
    }
}
