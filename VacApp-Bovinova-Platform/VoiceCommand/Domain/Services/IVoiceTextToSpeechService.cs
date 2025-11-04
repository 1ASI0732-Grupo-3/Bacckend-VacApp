namespace VacApp_Bovinova_Platform.VoiceCommand.Domain.Services;

public interface IVoiceTextToSpeechService
{
    public Task<Stream> ConvertTextToSpeechAsync(string text);
}
