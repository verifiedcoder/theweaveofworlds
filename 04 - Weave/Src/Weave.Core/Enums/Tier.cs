namespace Weave.Core.Enums;

/// <summary>
///     Manipulation complexity tiers.
/// </summary>
public enum Tier : byte
{
    One = 1,  // Basic manipulations (1-2 successes)
    Two = 2,  // Intermediate manipulations (3-4 successes)
    Three = 3 // Advanced manipulations (5+ successes)
}