namespace Weave.Core.Enums;

/// <summary>
///     Fabric stress severity levels.
/// </summary>
public enum FabricStressLevel : byte
{
    None = 0,  // No stress occurred
    Minor = 1, // Minor fabric stress
    Major = 2  // Major fabric stress
}