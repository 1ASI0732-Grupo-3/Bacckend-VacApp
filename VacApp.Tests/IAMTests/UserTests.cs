using Xunit.Abstractions;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands.UserCommands;

namespace VacApp.Tests.IAMTests
{
    public class UserTests
    {
        private readonly ITestOutputHelper _output;
        public UserTests(ITestOutputHelper output) => _output = output;

        [Fact]
        public void Constructor_por_defecto_inicializa_campos_vacios_y_email_no_confirmado()
        {
            // Arrange

            // Act
            var sut = new User();

            // Assert
            Assert.Equal(string.Empty, sut.Username);
            Assert.Equal(string.Empty, sut.Password);
            Assert.Equal(string.Empty, sut.Email);
            Assert.False(sut.EmailConfirmed);
            _output.WriteLine("AAA -> Assert: User por defecto con strings vacíos y EmailConfirmed=false");
        }

        [Fact]
        public void Constructor_con_SignUpCommand_valido_asigna_propiedades()
        {
            // Arrange
            var cmd = new SignUpCommand(
                Username: "jdoe",
                Password: "SecreT123!",
                Email: "jdoe@example.com"
            );

            // Act
            var sut = new User(cmd);

            // Assert
            Assert.Equal("jdoe", sut.Username);
            Assert.Equal("SecreT123!", sut.Password);
            Assert.Equal("jdoe@example.com", sut.Email);
            _output.WriteLine("AAA -> Assert: User creado correctamente con SignUpCommand válido");
        }

        [Fact]
        public void Constructor_con_email_invalido_lanza_ArgumentException()
        {
            // Arrange
            var cmd = new SignUpCommand(
                Username: "jdoe",
                Password: "SecreT123!",
                Email: "sin-formato"
            );

            // Act
            var ex = Assert.Throws<ArgumentException>(() => new User(cmd));

            // Assert
            Assert.StartsWith("Invalid email format.", ex.Message);
            _output.WriteLine("AAA -> Assert: Excepción por email inválido en constructor de User");
        }

        [Fact]
        public void Update_con_email_invalido_lanza_ArgumentException()
        {
            // Arrange
            var sut = new User(new SignUpCommand(
                Username: "jdoe",
                Password: "SecreT123!",
                Email: "jdoe@example.com"
            ));
            var update = new UpdateUserCommand(
                Username: "jdoe2",
                Email: "mal-email"
            );

            // Act
            var ex = Assert.Throws<ArgumentException>(() => sut.Update(update));

            // Assert
            Assert.StartsWith("Invalid email format.", ex.Message);
            _output.WriteLine("AAA -> Assert: Excepción por email inválido en Update de User");
        }
    }
}
