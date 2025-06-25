using Weave.Core.Enums;

namespace Weave.Core;

/// <summary>
///     Represents a quantum manipulation technique.
/// </summary>
public class ThreadManipulation(
    string name,
    string description,
    ThreadType threadType,
    Tier tier,
    CoreAttribute primaryAttribute,
    int attributeRequirement,
    int energyCost,
    int requiredSuccesses)
{
    public string Name { get; set; } = name;

    public string Description { get; set; } = description;

    public ThreadType ThreadType { get; set; } = threadType;

    public Tier Tier { get; set; } = tier;

    public CoreAttribute PrimaryAttribute { get; set; } = primaryAttribute;

    public int AttributeRequirement { get; set; } = attributeRequirement;

    public int EnergyCost { get; set; } = energyCost;

    public int RequiredSuccesses { get; set; } = requiredSuccesses;

    public bool IsSustained { get; set; }

    public bool IsAnchored { get; set; }

    public bool IsAreaEffect { get; set; }

    /// <summary>
    ///     Calculate total energy cost including modifiers
    /// </summary>
    /// <returns>Total energy cost</returns>
    public int CalculateTotalEnergyCost()
    {
        var totalCost = EnergyCost;

        if (IsSustained)
        {
            totalCost += 1;
        }

        if (IsAnchored)
        {
            totalCost += 2;
        }

        if (IsAreaEffect)
        {
            totalCost += 1;
        }

        return totalCost;
    }

    /// <summary>
    ///     Calculate dice pool modifier based on manipulation properties
    /// </summary>
    /// <returns>Dice pool modifier (negative value)</returns>
    public int CalculateDicePoolModifier()
    {
        var modifier = 0;

        if (IsAreaEffect)
        {
            modifier -= 1;
        }

        if (IsSustained && IsAreaEffect)
        {
            modifier -= 1;
        }

        return modifier;
    }
}