using System.Text.Json;
using Weave.Core.Enums;

namespace Weave.Core;

/// <summary>
///     Represents a player character or NPC with quantum manipulation abilities.
/// </summary>
public class Character
{
    /// <summary>
    ///     Creates a new character with default values.
    /// </summary>
    public Character()
    {
        Name = "New Weaver";
        Origin = Origin.Academic;
        Specialisation = Specialisation.ThreadWalker;
        Rank = Rank.Initiate;
        ExperiencePoints = 0;

        // Initialise attributes with default values (all 3).
        Attributes = new Dictionary<CoreAttribute, int>();

        foreach (var attr in Enum.GetValues<CoreAttribute>())
        {
            Attributes[attr] = 3;
        }

        // Initialise empty skills dictionary.
        Skills = new Dictionary<string, int>();

        // Initialise lists.
        KnownManipulations = [];
        EquippedItems = [];

        // Initialise resources.
        RecalculateResources();
    }

    /// <summary>
    ///     Creates a character with specified attributes and properties.
    /// </summary>
    public Character(
        string name, Origin origin,
        Specialisation specialisation,
        Dictionary<CoreAttribute, int> attributes,
        Dictionary<string, int> skills)
    {
        Name = name;
        Origin = origin;
        Specialisation = specialisation;
        Rank = Rank.Initiate;
        ExperiencePoints = 0;

        // Copy attributes with validation.
        Attributes = new Dictionary<CoreAttribute, int>();

        foreach (var kvp in attributes)
        {
            Attributes[kvp.Key] = Math.Max(1, Math.Min(10, kvp.Value));
        }

        // Copy skills.
        Skills = new Dictionary<string, int>(skills);

        // Initialise lists.
        KnownManipulations = [];
        EquippedItems = [];

        // Initialise resources.
        RecalculateResources();
    }

    // Basic Information.
    public string Name { get; set; }

    public Origin Origin { get; set; }

    public Specialisation Specialisation { get; set; }

    public Rank Rank { get; set; }

    public int ExperiencePoints { get; set; }

    // Attributes.
    public Dictionary<CoreAttribute, int> Attributes { get; set; }

    // Skills.
    public Dictionary<string, int> Skills { get; set; }

    // Resources.
    public int HealthPoints { get; set; }

    public int MaxHealthPoints { get; set; }

    public int EnergyPoints { get; set; }

    public int MaxEnergyPoints { get; set; }

    public int CognitiveStrain { get; set; }

    // Capabilities
    public List<ThreadManipulation> KnownManipulations { get; set; }

    public List<Equipment> EquippedItems { get; set; }

    // Derived stats
    public int ManipulationThreshold => (Attributes[CoreAttribute.Coherence] + Attributes[CoreAttribute.Cognition]) / 2;

    public int EnergyCapacity => (Attributes[CoreAttribute.Resonance] + Attributes[CoreAttribute.Volatility]) / 2;

    public int ControlRating => Attributes[CoreAttribute.Insight] + Attributes[CoreAttribute.Coherence] -
                                Attributes[CoreAttribute.Volatility] / 3;

    public int FabricImpact => Attributes[CoreAttribute.Volatility] + ManipulationThreshold - Attributes[CoreAttribute.Resonance] / 2;

    /// <summary>
    ///     Reset and recalculate character resources based on attributes.
    /// </summary>
    public void RecalculateResources()
    {
        MaxHealthPoints = 10 + Attributes[CoreAttribute.Coherence];
        HealthPoints = MaxHealthPoints;

        MaxEnergyPoints = EnergyCapacity;
        EnergyPoints = MaxEnergyPoints;

        CognitiveStrain = 0;
    }

    /// <summary>
    ///     Apply origin bonuses to attributes.
    /// </summary>
    public void ApplyOriginBonuses()
    {
        switch (Origin)
        {
            case Origin.Academic:

                Attributes[CoreAttribute.Cognition] += 1;
                Attributes[CoreAttribute.Coherence] += 1;

                break;

            case Origin.Intuitive:

                Attributes[CoreAttribute.Insight] += 1;
                Attributes[CoreAttribute.Resonance] += 1;

                break;

            case Origin.Military:

                Attributes[CoreAttribute.Volatility] += 1;
                Attributes[CoreAttribute.Coherence] += 1;

                break;

            case Origin.Hermetic:

                Attributes[CoreAttribute.Resonance] += 1;
                Attributes[CoreAttribute.Insight] += 1;

                break;

            default:

                throw new InvalidOperationException("Invalid Origin.");
        }

        RecalculateResources();
    }

    /// <summary>
    ///     Calculate dice pool for a thread test.
    /// </summary>
    /// <param name="attribute">Attribute to use.</param>
    /// <param name="skill">Skill to use.</param>
    /// <returns>Number of dice to roll.</returns>
    public int CalculateDicePool(CoreAttribute attribute, string skill)
    {
        var attributeValue = Attributes[attribute];
        var skillValue = Skills.GetValueOrDefault(skill, 0);

        return attributeValue + skillValue;
    }

    /// <summary>
    ///     Perform a thread test.
    /// </summary>
    /// <param name="attribute">Attribute to use.</param>
    /// <param name="skill">Skill to use.</param>
    /// <param name="difficulty">Target number for success.</param>
    /// <param name="requiredSuccesses">Number of successes needed.</param>
    /// <param name="environmentModifier">Modifier from gameEnvironment.</param>
    /// <returns>Result of the thread test.</returns>
    public ThreadTestResult PerformThreadTest(
        CoreAttribute attribute,
        string skill,
        int difficulty = 7,
        int requiredSuccesses = 1,
        int environmentModifier = 0)
    {
        var dicePool = CalculateDicePool(attribute, skill);

        dicePool += environmentModifier;

        // Ensure minimum of 1 die.
        dicePool = Math.Max(1, dicePool);

        return DiceRoller.RollThreadTest(dicePool, difficulty, requiredSuccesses);
    }

    /// <summary>
    ///     Attempt to perform a manipulation.
    /// </summary>
    /// <param name="manipulation">Manipulation to perform.</param>
    /// <param name="environmentModifier">Dice modifier from gameEnvironment.</param>
    /// <returns>Result of the manipulation attempt.</returns>
    public ManipulationResult PerformManipulation(ThreadManipulation manipulation, int environmentModifier = 0)
    {
        // Check if character knows this manipulation.
        if (!KnownManipulations.Contains(manipulation))
        {
            return new ManipulationResult
            {
                Success = false,
                Message = "Character does not know this manipulation"
            };
        }

        // Check if character has enough energy.
        if (EnergyPoints < manipulation.EnergyCost)
        {
            return new ManipulationResult
            {
                Success = false,
                Message = "Insufficient energy to perform this manipulation"
            };
        }

        // Check if manipulation is within threshold.
        if ((int)manipulation.Tier > ManipulationThreshold)
            // Character can still attempt but will gain strain.
        {
            CognitiveStrain += 1;
        }

        // Spend energy.
        EnergyPoints -= manipulation.EnergyCost;

        // Perform thread test.
        var testResult = PerformThreadTest(
            manipulation.PrimaryAttribute,
            $"{manipulation.ThreadType} Threading",
            7, // Standard difficulty
            manipulation.RequiredSuccesses,
            environmentModifier
        );

        // Create result.
        var result = new ManipulationResult
        {
            Success = testResult.Success,
            Successes = testResult.Successes,
            StressDieResult = testResult.StressDieResult,
            Message = testResult.Success ? "Manipulation successful" : "Manipulation failed"
        };

        // Check for fabric stress.
        if (testResult.StressDieResult != 1)
        {
            return result;
        }

        var majorStress = (int)manipulation.Tier > ManipulationThreshold;

        result.FabricStress = majorStress ? FabricStressLevel.Major : FabricStressLevel.Minor;
        result.Message += majorStress ? " - Major fabric stress occurred!" : " - Minor fabric stress occurred";

        return result;
    }

    /// <summary>
    ///     Recover energy points through rest.
    /// </summary>
    /// <param name="hours">Number of hours rested.</param>
    public void RecoverEnergy(int hours)
    {
        // Recover 1 energy point per hour, up to maximum.
        EnergyPoints = Math.Min(MaxEnergyPoints, EnergyPoints + hours);
    }

    /// <summary>
    ///     Recover from cognitive strain.
    /// </summary>
    /// <param name="hours">Number of hours rested.</param>
    public void RecoverStrain(int hours)
    {
        // Recover 1 strain point per 4 hours.
        var recoveredPoints = hours / 4;
        CognitiveStrain = Math.Max(0, CognitiveStrain - recoveredPoints);
    }

    /// <summary>
    ///     Apply the effects of equipment.
    /// </summary>
    /// <param name="equipment">Equipment to equip.</param>
    public void EquipItem(Equipment equipment)
    {
        EquippedItems.Add(equipment);
        // Apply equipment effects (bonuses, etc.).
        // This would be expanded in a more detailed implementation.
    }

    /// <summary>
    ///     Learn a new manipulation technique.
    /// </summary>
    /// <param name="manipulation">Manipulation to learn.</param>
    /// <returns>True if successful, false if requirements not met.</returns>
    public bool LearnManipulation(ThreadManipulation manipulation)
    {
        // Check rank requirements.
        if ((manipulation.Tier == Tier.Two && Rank < Rank.Journeyman) ||
            (manipulation.Tier == Tier.Three && Rank < Rank.Master))
        {
            return false;
        }

        // Check attribute requirements.
        if (Attributes[manipulation.PrimaryAttribute] < manipulation.AttributeRequirement)
        {
            return false;
        }

        // Add manipulation to known list.
        if (!KnownManipulations.Contains(manipulation))
        {
            KnownManipulations.Add(manipulation);
        }

        return true;
    }

    /// <summary>
    ///     Spend XP to improve an attribute.
    /// </summary>
    /// <param name="attribute">Attribute to improve.</param>
    /// <returns>True if successful, false if insufficient XP.</returns>
    public bool ImproveAttribute(CoreAttribute attribute)
    {
        var currentValue = Attributes[attribute];
        var cost = currentValue * 5;

        // Check rank caps.
        var maxValue = Rank switch
        {
            Rank.Initiate   => 5,
            Rank.Journeyman => 7,
            _               => 10
        };

        if (currentValue >= maxValue)
        {
            return false;
        }

        // Check if enough XP.
        if (ExperiencePoints < cost)
        {
            return false;
        }

        // Spend XP and increase attribute.
        ExperiencePoints -= cost;
        Attributes[attribute]++;
        RecalculateResources();

        return true;
    }

    /// <summary>
    ///     Spend XP to improve a skill.
    /// </summary>
    /// <param name="skill">Skill to improve.</param>
    /// <returns>True if successful, false if insufficient XP.</returns>
    public bool ImproveSkill(string skill)
    {
        Skills.TryAdd(skill, 0);

        var currentValue = Skills[skill];
        var cost = currentValue * 3;

        // Check if enough XP.
        if (ExperiencePoints < cost)
        {
            return false;
        }

        // Spend XP and increase skill.
        ExperiencePoints -= cost;
        Skills[skill]++;

        return true;
    }

    /// <summary>
    ///     Add experience points and check for rank advancement.
    /// </summary>
    /// <param name="amount">Amount of XP to add.</param>
    public void AddExperience(int amount)
    {
        ExperiencePoints += amount;
        UpdateRank();
    }

    /// <summary>
    ///     Update rank based on total XP.
    /// </summary>
    private void UpdateRank()
    {
        Rank = ExperiencePoints switch
        {
            >= 60 => Rank.Master,
            >= 25 => Rank.Journeyman,
            _     => Rank.Initiate
        };
    }

    /// <summary>
    ///     Serialize character to JSON.
    /// </summary>
    /// <returns>JSON string representing character.</returns>
    public string ToJson() => JsonSerializer.Serialize(this);

    /// <summary>
    ///     Deserialize character from JSON.
    /// </summary>
    /// <param name="json">JSON string to deserialized <see cref="Character"/>.</param>
    /// <returns>Character object.</returns>
    public static Character? FromJson(string json) => JsonSerializer.Deserialize<Character>(json);
}