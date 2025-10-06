using Xunit;
using Xunit.Abstractions;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Commands;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.ValueObjects;

namespace VacApp.Tests.StaffTests;

public class StaffTests
{
    private readonly ITestOutputHelper _output;
    public StaffTests(ITestOutputHelper output) => _output = output;

    private static string Dump(Staff s) =>
        $"Staff{{ Id={s.Id}, Name='{s.Name}', EmployeeStatus={s.EmployeeStatus}, CampaignId={(s.CampaignId?.ToString() ?? "null")}, StaffUserId={(s.StaffUserId != null ? s.StaffUserId.UserIdentifier.ToString() : "null")} }}";

    [Fact]
    public void Constructor_por_defecto_inicializa_campos_por_defecto()
    {
        // Arrange
        _output.WriteLine("AAA -> Arrange: sin parámetros");

        // Act
        var sut = new Staff();
        _output.WriteLine($"AAA -> Act: creado -> {Dump(sut)}");

        // Assert
        Assert.Equal(string.Empty, sut.Name);
        Assert.NotNull(sut.EmployeeStatus);
        Assert.Null(sut.CampaignId);
        Assert.Null(sut.StaffUserId);
        _output.WriteLine("AAA -> Assert: Name='', EmployeeStatus!=null, CampaignId=null, StaffUserId=null");
    }

    [Fact]
    public void Constructor_parametros_valido_asigna_propiedades()
    {
        // Arrange
        var name = "Alice";
        var employeeStatus = 1;
        int? campaignId = 10;
        var staffUserId = new StaffUserId(1);
        _output.WriteLine($"AAA -> Arrange: name='{name}', employeeStatus={employeeStatus}, campaignId={campaignId}, staffUserId={staffUserId.UserIdentifier}");

        // Act
        var sut = new Staff(name, employeeStatus, campaignId, staffUserId);
        _output.WriteLine($"AAA -> Act: creado -> {Dump(sut)}");

        // Assert
        Assert.Equal(name, sut.Name);
        Assert.NotNull(sut.EmployeeStatus);
        Assert.Equal(campaignId, sut.CampaignId);
        Assert.Equal(staffUserId, sut.StaffUserId);
        _output.WriteLine("AAA -> Assert: propiedades asignadas según Arrange");
    }

    [Fact]
    public void Constructor_con_CreateStaffCommand_valido_asigna_propiedades()
    {
        // Arrange
        var name = "Bob";
        var employeeStatus = 2;
        int? campaignId = 7;
        var staffUser = new StaffUserId(42);
        _output.WriteLine($"AAA -> Arrange(cmd): Name='{name}', EmployeeStatus={employeeStatus}, CampaignId={campaignId}, StaffUserId={staffUser.UserIdentifier}");
        var cmd = new CreateStaffCommand(Name: name, EmployeeStatus: employeeStatus, CampaignId: campaignId, StaffUserId: staffUser);

        // Act
        var sut = new Staff(cmd);
        _output.WriteLine($"AAA -> Act: creado -> {Dump(sut)}");

        // Assert
        Assert.Equal(name, sut.Name);
        Assert.NotNull(sut.EmployeeStatus);
        Assert.Equal(campaignId, sut.CampaignId);
        Assert.Equal(staffUser.UserIdentifier, sut.StaffUserId!.UserIdentifier);
        _output.WriteLine("AAA -> Assert: propiedades asignadas desde command");
    }

    [Fact]
    public void Constructor_con_command_CampaignId_null_lanza_ArgumentException()
    {
        // Arrange
        var name = "NoCampaign";
        var employeeStatus = 1;
        int? campaignId = null;
        var staffUser = new StaffUserId(1);
        _output.WriteLine($"AAA -> Arrange(cmd): Name='{name}', EmployeeStatus={employeeStatus}, CampaignId=null, StaffUserId={staffUser.UserIdentifier}");
        var cmd = new CreateStaffCommand(Name: name, EmployeeStatus: employeeStatus, CampaignId: campaignId, StaffUserId: staffUser);

        // Act
        var ex = Assert.Throws<ArgumentException>(() => new Staff(cmd));

        // Assert
        _output.WriteLine($"AAA -> Assert: excepción -> {ex.GetType().Name}: \"{ex.Message}\"");
        Assert.Equal("CampaignId is required", ex.Message);
    }

    [Fact]
    public void Constructor_con_command_StaffUserId_null_lanza_ArgumentException()
    {
        // Arrange
        var name = "NoUser";
        var employeeStatus = 1;
        int? campaignId = 5;
        _output.WriteLine($"AAA -> Arrange(cmd): Name='{name}', EmployeeStatus={employeeStatus}, CampaignId={campaignId}, StaffUserId=null");
        var cmd = new CreateStaffCommand(Name: name, EmployeeStatus: employeeStatus, CampaignId: campaignId, StaffUserId: null);

        // Act
        var ex = Assert.Throws<ArgumentException>(() => new Staff(cmd));

        // Assert
        _output.WriteLine($"AAA -> Assert: excepción -> {ex.GetType().Name}: \"{ex.Message}\"");
        Assert.Equal("StaffUserId must be set by the system", ex.Message);
    }

    [Fact]
    public void Update_con_datos_validos_actualiza_propiedades_y_admite_CampaignId_null()
    {
        // Arrange
        var sut = new Staff("Init", 1, 10, new StaffUserId(7));
        _output.WriteLine($"AAA -> Arrange: before -> {Dump(sut)}");
        var updateId = 3; // si tu comando requiere Id
        var update = new UpdateStaffCommand(Id: updateId, Name: "Updated", EmployeeStatus: 3, CampaignId: null);
        _output.WriteLine("AAA -> Arrange(update): Id=3, Name='Updated', EmployeeStatus=3, CampaignId=null");

        // Act
        sut.Update(update);
        _output.WriteLine($"AAA -> Act: after -> {Dump(sut)}");

        // Assert
        Assert.Equal("Updated", sut.Name);
        Assert.NotNull(sut.EmployeeStatus);
        Assert.Null(sut.CampaignId);
        _output.WriteLine("AAA -> Assert: nombre y estado actualizados, CampaignId=null");
    }

    [Fact]
    public void Update_con_datos_validos_actualiza_propiedades_con_CampaignId_no_null()
    {
        // Arrange
        var sut = new Staff("Init", 1, 10, new StaffUserId(7));
        _output.WriteLine($"AAA -> Arrange: before -> {Dump(sut)}");
        var updateId = 3; // si tu comando requiere Id
        var update = new UpdateStaffCommand(Id: updateId, Name: "Updated2", EmployeeStatus: 4, CampaignId: 99);
        _output.WriteLine("AAA -> Arrange(update): Id=3, Name='Updated2', EmployeeStatus=4, CampaignId=99");

        // Act
        sut.Update(update);
        _output.WriteLine($"AAA -> Act: after -> {Dump(sut)}");

        // Assert
        Assert.Equal("Updated2", sut.Name);
        Assert.NotNull(sut.EmployeeStatus);
        Assert.Equal(99, sut.CampaignId);
        _output.WriteLine("AAA -> Assert: nombre y estado actualizados, CampaignId=99");
    }
}
