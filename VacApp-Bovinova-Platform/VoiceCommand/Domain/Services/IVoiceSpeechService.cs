namespace VacApp_Bovinova_Platform.VoiceCommand.Domain.Services;

public interface IVoiceSpeechService
{
    public Task<string> ConvertSpeechToTextAsync(Stream audioStream);
}
