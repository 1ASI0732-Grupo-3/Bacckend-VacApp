namespace VacApp_Bovinova_Platform.VoiceCommand.Domain.Services;

public interface IVoiceSpeechService
{
    Task<string> ConvertSpeechToTextAsync(Stream audioStream);
}