using Weave.Core.Enums;

namespace Weave.Core;

/// <summary>
///     Represents an item or device that can be used by a character.
/// </summary>
public class Equipment(string name, string description, EquipmentType type)
{
    public string Name { get; set; } = name;

    public string Description { get; set; } = description;

    public EquipmentType Type { get; set; } = type;

    public Dictionary<string, int> Modifiers { get; set; } = new();

    public Dictionary<string, string> Properties { get; set; } = new();

    public int Charges { get; set; }

    public int MaxCharges { get; set; } = 0;

    /// <summary>
    ///     Use a charge of the equipment.
    /// </summary>
    /// <returns>True if successful, false if no charges left.</returns>
    public bool UseCharge()
    {
        if (Charges <= 0)
        {
            return false;
        }

        Charges--;

        return true;
    }

    /// <summary>
    ///     Recharge the equipment.
    /// </summary>
    /// <param name="amount">Number of charges to add.</param>
    public void Recharge(int amount) => Charges = Math.Min(MaxCharges, Charges + amount);
}