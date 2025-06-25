namespace Weave.Core.Enums;

/// <summary>
///     Severity levels for anomalies
/// </summary>
public enum AnomalySeverity : byte
{
    Minor = 0,    // Small, localised effects
    Moderate = 1, // Notable effects covering an area
    Major = 2,    // Dangerous effects with significant impact
    Critical = 3  // Extreme effects with wide-ranging consequences
}