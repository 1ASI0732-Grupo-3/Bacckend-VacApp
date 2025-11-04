using Xunit.Abstractions;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands.AdminCommands;

namespace VacApp.Tests.IAMTests
{
    public class AdminTests
    {
        private readonly ITestOutputHelper _output;
        public AdminTests(ITestOutputHelper output) => _output = output;

        [Fact]
        public void Constructor_con_CreateAdminCommand_valido_asigna_propiedades()
        {
            // Arrange
            var cmd = new CreateAdminCommand(Email: "admin@vacapp.com");

            // Act
            var sut = new Admin(cmd);

            // Assert
            Assert.Equal("admin@vacapp.com", sut.Email);
            Assert.True(sut.EmailConfirmed);
            _output.WriteLine("AAA -> Assert: Admin creado correctamente con email @vacapp.com");
        }

        [Fact]
        public void Constructor_con_email_no_vacapp_lanza_ArgumentException()
        {
            // Arrange
            var cmd = new CreateAdminCommand(Email: "admin@gmail.com");

            // Act
            var ex = Assert.Throws<ArgumentException>(() => new Admin(cmd));

            // Assert
            Assert.StartsWith("Admin email must end with @vacapp.com", ex.Message);
            _output.WriteLine("AAA -> Assert: Excepción por no terminar en @vacapp.com");
        }

        [Fact]
        public void Update_con_email_invalido_lanza_ArgumentException()
        {
            // Arrange
            var sut = new Admin(new CreateAdminCommand(Email: "root@vacapp.com"));
            var update = new UpdateAdminCommand(Email: "bad@@vacapp.com"); // termina en @vacapp.com pero formato inválido NO BORRAR

            // Act
            var ex = Assert.Throws<ArgumentException>(() => sut.Update(update));

            // Assert
            Assert.StartsWith("Invalid email format.", ex.Message);
            _output.WriteLine("AAA -> Assert: Excepción por formato de email inválido en Update");
        }

        [Fact]
        public void ValidateLogin_siempre_devuelve_true()
        {
            // Arrange
            var sut = new Admin(new CreateAdminCommand(Email: "root@vacapp.com"));

            // Act
            var ok = sut.ValidateLogin("cualquier-cosa");

            // Assert
            Assert.True(ok);
            _output.WriteLine("AAA -> Assert: ValidateLogin devolvió true");
        }
    }
}
