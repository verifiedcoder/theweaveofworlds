namespace Weave.Core.Enums;

/// <summary>
///     Game environment stability levels affecting manipulation difficulty.
/// </summary>
public enum StabilityLevel : byte
{
    AnchorZone = 0,     // Exceptional stability (+2 dice, -1 energy)
    HarmonicRegion = 1, // High stability (+1 die)
    StandardField = 2,  // Normal stability (no modifier)
    TensionZone = 3,    // Low stability (-1 die)
    FractureBelt = 4,   // Very low stability (-2 dice, +1 energy)
    VoidRegion = 5      // Critical stability (-3 dice, doubled energy)
}