using Weave.Core.Enums;

namespace Weave.Core;

/// <summary>
///     Represents a quantum fabric anomaly.
/// </summary>
public class Anomaly
{
    public required string Name { get; init; }

    public required string Description { get; init; }

    public ThreadType ThreadType { get; set; }

    public AnomalySeverity Severity { get; set; }

    public string? Effect { get; set; }

    public int DurationHours { get; set; }
}