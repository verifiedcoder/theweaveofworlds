using Weave.Core.Enums;

namespace Weave.Core;

/// <summary>
///     Represents the result of a manipulation attempt.
/// </summary>
public class ManipulationResult
{
    public bool Success { get; set; }

    public int Successes { get; set; }

    public int StressDieResult { get; set; }

    public FabricStressLevel FabricStress { get; set; }

    public required string Message { get; set; }
}