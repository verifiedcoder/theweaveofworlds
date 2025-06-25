using Weave.Core.Enums;

namespace Weave.Core;

/// <summary>
///     Factory class for creating character templates.
/// </summary>
public static class CharacterFactory
{
    /// <summary>
    ///     Create a new character with the specified parameters.
    /// </summary>
    /// <param name="name">Character name.</param>
    /// <param name="origin">Character origin.</param>
    /// <param name="specialisation">Character specialisation.</param>
    /// <returns>Initialised character.</returns>
    public static Character CreateCharacter(string name, Origin origin, Specialisation specialisation)
    {
        // Create basic character.
        var character = new Character
        {
            Name = name,
            Origin = origin,
            Specialisation = specialisation,
            Rank = Rank.Initiate,
            ExperiencePoints = 0
        };

        // Set up attributes based on specialisation.
        var attributes = new Dictionary<CoreAttribute, int>();

        foreach (var attr in Enum.GetValues<CoreAttribute>())
        {
            attributes[attr] = 3; // Default value.
        }

        // Adjust primary attributes based on specialisation.
        switch (specialisation)
        {
            case Specialisation.ThreadWalker:

                attributes[CoreAttribute.Insight] = 5;
                attributes[CoreAttribute.Coherence] = 3;
                attributes[CoreAttribute.Resonance] = 3;
                attributes[CoreAttribute.Volatility] = 2;
                attributes[CoreAttribute.Cognition] = 2;

                break;

            case Specialisation.KineticWeaver:

                attributes[CoreAttribute.Volatility] = 5;
                attributes[CoreAttribute.Resonance] = 3;
                attributes[CoreAttribute.Coherence] = 3;
                attributes[CoreAttribute.Insight] = 2;
                attributes[CoreAttribute.Cognition] = 2;

                break;

            case Specialisation.MatterArchitect:

                attributes[CoreAttribute.Cognition] = 5;
                attributes[CoreAttribute.Volatility] = 3;
                attributes[CoreAttribute.Coherence] = 3;
                attributes[CoreAttribute.Insight] = 2;
                attributes[CoreAttribute.Resonance] = 2;

                break;

            case Specialisation.ChronoBinder:

                attributes[CoreAttribute.Coherence] = 5;
                attributes[CoreAttribute.Insight] = 3;
                attributes[CoreAttribute.Resonance] = 3;
                attributes[CoreAttribute.Cognition] = 2;
                attributes[CoreAttribute.Volatility] = 2;

                break;

            case Specialisation.QuantumSurgeon:

                attributes[CoreAttribute.Insight] = 5;
                attributes[CoreAttribute.Cognition] = 3;
                attributes[CoreAttribute.Coherence] = 3;
                attributes[CoreAttribute.Resonance] = 2;
                attributes[CoreAttribute.Volatility] = 2;

                break;

            default:

                throw new ArgumentOutOfRangeException(nameof(specialisation), specialisation, null);
        }

        character.Attributes = attributes;

        // Set up skills based on specialisation.
        var skills = new Dictionary<string, int>();

        // Add thread skills
        switch (specialisation)
        {
            case Specialisation.ThreadWalker:

                skills["Probability Threading"] = 3;
                skills["Force Threading"] = 1;

                break;

            case Specialisation.KineticWeaver:

                skills["Force Threading"] = 3;
                skills["Matter Threading"] = 1;

                break;

            case Specialisation.MatterArchitect:
            case Specialisation.QuantumSurgeon:
                
                skills["Matter Threading"] = 3;
                skills["Probability Threading"] = 1;

                break;

            case Specialisation.ChronoBinder:

                skills["Spacetime Threading"] = 3;
                skills["Force Threading"] = 1;

                break;

            default:

                throw new ArgumentOutOfRangeException(nameof(specialisation), specialisation, null);
        }

        // Add common skills.
        skills["Quantum Theory"] = 2;
        skills["Perception"] = 2;
        skills["Equipment Operation"] = 1;
        skills["Social Interaction"] = 1;

        character.Skills = skills;

        // Apply origin bonuses.
        character.ApplyOriginBonuses();

        // Add starting manipulations.
        character.KnownManipulations = ManipulationFactory.CreateStartingManipulations(specialisation);

        // Add starting equipment.
        character.EquippedItems = EquipmentFactory.CreateStartingEquipment(specialisation);

        // Calculate resources.
        character.RecalculateResources();

        return character;
    }
}