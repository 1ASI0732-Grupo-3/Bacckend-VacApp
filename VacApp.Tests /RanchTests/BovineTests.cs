using Xunit.Abstractions;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;

namespace VacApp.Tests.RanchTests;

public class BovineTests
{
    private readonly ITestOutputHelper _output;
    public BovineTests(ITestOutputHelper output) => _output = output;

    [Fact]
    public void Constructor_parametros_valido_asigna_propiedades()
    {
        // Arrange
        var name = "B-001";
        var gender = "male";
        var birth = new DateTime(2022, 5, 10);
        var breed = "Holstein";
        var location = "Lote 1";
        var img = "b1.png";
        int? stableId = 3;

        // Act
        var sut = new Bovine(name, gender, birth, breed, location, img, stableId, ranchUserId: null);

        // Assert
        Assert.Equal(name, sut.Name);
        Assert.Equal(gender, sut.Gender);
        Assert.Equal(birth, sut.BirthDate);
        Assert.Equal(breed, sut.Breed);
        Assert.Equal(location, sut.Location);
        Assert.Equal(img, sut.BovineImg);
        Assert.Equal(stableId, sut.StableId);
        _output.WriteLine($"AAA -> Assert: Bovine creado OK. Name={sut.Name}, Gender={sut.Gender}, Birth={sut.BirthDate:yyyy-MM-dd}, Breed={sut.Breed}, Location={sut.Location}, Img={sut.BovineImg}, StableId={sut.StableId}");
    }

    [Fact]
    public void Constructor_command_con_genero_invalido_lanza_ArgumentException()
    {
        // Arrange
        var cmd = new CreateBovineCommand(
            Name: "B-ERR",
            Gender: "unknown",
            BirthDate: new DateTime(2023, 1, 1),
            Breed: "Jersey",
            Location: "Lote X",
            BovineImg: "x.png",
            StableId: 1,
            RanchUserId: new RanchUserId(1),
            FileData: null
        );

        // Act
        var ex = Assert.Throws<ArgumentException>(() => new Bovine(cmd));

        // Assert
        Assert.Equal("Gender must be either 'male' or 'female'", ex.Message);
        _output.WriteLine("AAA -> Assert: Lanzada excepción por género inválido. Msg='Gender must be either 'male' or 'female''");
    }

    [Fact]
    public void Constructor_command_con_userid_nulo_lanza_ArgumentException()
    {
        // Arrange
        var cmd = new CreateBovineCommand(
            Name: "B-NullU",
            Gender: "female",
            BirthDate: new DateTime(2023, 2, 2),
            Breed: "Angus",
            Location: "Lote 2",
            BovineImg: "b2.png",
            StableId: 2,
            RanchUserId: null,
            FileData: null
        );

        // Act
        var ex = Assert.Throws<ArgumentException>(() => new Bovine(cmd));

        // Assert
        Assert.Equal("UserId must be set by the system", ex.Message);
        _output.WriteLine("AAA -> Assert: Lanzada excepción por RanchUserId nulo en constructor con comando.");
    }

    [Fact]
    public void Update_con_datos_validos_actualiza_propiedades_y_conserva_imagen()
    {
        // Arrange
        var sut = new Bovine("B-Init", "male", new DateTime(2022, 1, 1), "Criollo", "Lote A", "img-ini.png", 5, null);
        var update = new UpdateBovineCommand(
            Id: sut.Id,
            Name: "B-Updated",
            Gender: "female",
            BirthDate: new DateTime(2021, 12, 31),
            Breed: "Brahman",
            Location: "Lote B",
            StableId: 6
        );

        // Act
        sut.Update(update);

        // Assert
        Assert.Equal("B-Updated", sut.Name);
        Assert.Equal("female", sut.Gender);
        Assert.Equal(new DateTime(2021, 12, 31), sut.BirthDate);
        Assert.Equal("Brahman", sut.Breed);
        Assert.Equal("Lote B", sut.Location);
        Assert.Equal(6, sut.StableId);
        Assert.Equal("img-ini.png", sut.BovineImg);
        _output.WriteLine($"AAA -> Assert: Update OK. Name={sut.Name}, Gender={sut.Gender}, Birth={sut.BirthDate:yyyy-MM-dd}, Breed={sut.Breed}, Location={sut.Location}, StableId={sut.StableId}, Img(no-change)={sut.BovineImg}");
    }
}
