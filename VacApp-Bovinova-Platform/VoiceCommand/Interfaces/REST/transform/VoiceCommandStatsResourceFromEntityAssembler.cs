using VacApp_Bovinova_Platform.VoiceCommand.Infrastructure.Persistence.EFC.Repositories;
using VacApp_Bovinova_Platform.VoiceCommand.Interfaces.REST.resources;

namespace VacApp_Bovinova_Platform.VoiceCommand.Interfaces.REST.transform;

/// <summary>
/// Assembler to transform VoiceCommandStats to VoiceCommandStatsResource
/// </summary>
public static class VoiceCommandStatsResourceFromEntityAssembler
{
    /// <summary>
    /// Transforms VoiceCommandStats to VoiceCommandStatsResource
    /// </summary>
    /// <param name="stats">The VoiceCommandStats data</param>
    /// <returns>VoiceCommandStatsResource</returns>
    public static VoiceCommandStatsResource ToResourceFromStats(VoiceCommandStats stats)
    {
        return new VoiceCommandStatsResource(
            stats.TotalCommands,
            stats.ValidCommands,
            stats.ExecutedCommands,
            stats.FailedCommands,
            stats.CommandsByType
        );
    }
}