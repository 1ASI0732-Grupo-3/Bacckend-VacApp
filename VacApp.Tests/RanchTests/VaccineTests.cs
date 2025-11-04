using Xunit.Abstractions;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;

namespace VacApp.Tests.RanchTests;

public class VaccineTests
{
    private readonly ITestOutputHelper _output;
    public VaccineTests(ITestOutputHelper output) => _output = output;

    [Fact]
    public void Constructor_command_con_userid_nulo_lanza_ArgumentException()
    {
        // Arrange
        var cmd = new CreateVaccineCommand(
            Name: "Vac-A",
            VaccineType: "Antiparasitaria",
            VaccineDate: new DateTime(2025, 1, 10),
            VaccineImg: "v1.png",
            BovineId: 10,
            RanchUserId: null,
            FileData: null
        );

        // Act
        var ex = Assert.Throws<ArgumentException>(() => new Vaccine(cmd));

        // Assert
        Assert.Equal("RanchUserId must be set by the system", ex.Message);
        _output.WriteLine("AAA -> Assert: Lanzada excepción por RanchUserId nulo en Vaccine.");
    }

    [Fact]
    public void Constructor_valido_asigna_propiedades()
    {
        // Arrange
        var cmd = new CreateVaccineCommand(
            Name: "Vac-B",
            VaccineType: "Refuerzo",
            VaccineDate: new DateTime(2025, 2, 20),
            VaccineImg: "v2.png",
            BovineId: 11,
            RanchUserId: new RanchUserId(12),
            FileData: null
        );

        // Act
        var sut = new Vaccine(cmd);

        // Assert
        Assert.Equal("Vac-B", sut.Name);
        Assert.Equal("Refuerzo", sut.VaccineType);
        Assert.Equal(new DateTime(2025, 2, 20), sut.VaccineDate);
        Assert.Equal("v2.png", sut.VaccineImg);
        Assert.Equal(11, sut.BovineId);
        _output.WriteLine($"AAA -> Assert: Vaccine creado OK. Name={sut.Name}, Type={sut.VaccineType}, Date={sut.VaccineDate:yyyy-MM-dd}, Img={sut.VaccineImg}, BovineId={sut.BovineId}");
    }

    [Fact]
    public void Update_modifica_campos_y_conserva_imagen()
    {
        // Arrange
        var created = new Vaccine(new CreateVaccineCommand(
            Name: "Vac-Init",
            VaccineType: "Tipo-1",
            VaccineDate: new DateTime(2025, 3, 1),
            VaccineImg: "init.png",
            BovineId: 5,
            RanchUserId: new RanchUserId(12),
            FileData: null
        ));
        var update = new UpdateVaccineCommand(
            Id:23,
            Name: "Vac-New",
            VaccineType: "Tipo-2",
            VaccineDate: new DateTime(2025, 3, 15),
            BovineId: 6
        );

        // Act
        created.Update(update);

        // Assert
        Assert.Equal("Vac-New", created.Name);
        Assert.Equal("Tipo-2", created.VaccineType);
        Assert.Equal(new DateTime(2025, 3, 15), created.VaccineDate);
        Assert.Equal(6, created.BovineId);
        Assert.Equal("init.png", created.VaccineImg); // no se actualiza en Update
        _output.WriteLine($"AAA -> Assert: Update OK. Name={created.Name}, Type={created.VaccineType}, Date={created.VaccineDate:yyyy-MM-dd}, BovineId={created.BovineId}, Img(no-change)={created.VaccineImg}");
    }
}
