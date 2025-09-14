using VacApp_Bovinova_Platform.VoiceCommand.Domain.Model.ValueObjects;

namespace VacApp_Bovinova_Platform.VoiceCommand.Domain.Services;

public interface IVoiceParserService
{
    VoiceCommandResult ParseCommand(string text);
}