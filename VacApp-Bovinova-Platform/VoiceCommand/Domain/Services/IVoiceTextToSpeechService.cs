namespace VacApp_Bovinova_Platform.VoiceCommand.Domain.Services;

public interface IVoiceTextToSpeechService
{
    Task<Stream> ConvertTextToSpeechAsync(string text);
}