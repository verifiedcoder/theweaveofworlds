using Weave.Core;
using Weave.Core.Enums;

Console.WriteLine("=== Quantum Weaver Framework Test Program ===\n");

// Create a new character.
Console.WriteLine("Creating a new character...");

var elara = CharacterFactory.CreateCharacter("Elara Vess", Origin.Intuitive, Specialisation.ThreadWalker);

// Display character information.
DisplayCharacter(elara);

// Create test gameEnvironment.
Console.WriteLine("\nCreating test game environment...");

var testEnvironment = new GameEnvironment(
    "Research Laboratory",
    "An abandoned quantum research facility with unstable fabric conditions",
    StabilityLevel.TensionZone)
{
    ThreadDensity =
    {
        // Make the game environment more interesting by modifying thread density.
        [ThreadType.Probability] = 1.5 // Higher probability thread density.
    }
};

// Display gameEnvironment information.
DisplayGameEnvironment(testEnvironment);

// Create a game session.
Console.WriteLine("\nCreating game session...");

var gameManager = new GameManager();

gameManager.StartNewSession([elara], testEnvironment);

// Perform a test manipulation.
Console.WriteLine("\nPerforming test manipulation...");

// Find the Probability Nudge manipulation.
var probabilityNudge = elara.KnownManipulations.Find(m => m.Name == "Probability Nudge");

// Display manipulation details.
DisplayManipulation(probabilityNudge!);

Console.WriteLine("\nAttempting 'Probability Nudge'...");

var result = gameManager.PerformManipulation(elara, probabilityNudge!);

// Display result.
Console.WriteLine($"Result: {result.Success}");
Console.WriteLine($"Successes: {result.Successes}");
Console.WriteLine($"Stress Die: {result.StressDieResult}");
Console.WriteLine($"Fabric Stress: {result.FabricStress}");
Console.WriteLine($"Message: {result.Message}");

// Advance time.
Console.WriteLine("\nAdvancing time by 4 hours...");

gameManager.AdvanceTime(4);

// Check character status after time advancement.
Console.WriteLine("\nUpdated character status:");
Console.WriteLine($"Energy Points: {elara.EnergyPoints}/{elara.MaxEnergyPoints}");
Console.WriteLine($"Cognitive Strain: {elara.CognitiveStrain}");

// Test character advancement.
Console.WriteLine("\nTesting character advancement...");

// Add some experience.
Console.WriteLine("Adding 10 experience points...");

elara.AddExperience(10);

Console.WriteLine($"Current XP: {elara.ExperiencePoints}");

// Try to improve an attribute.
Console.WriteLine("\nImproving Insight attribute...");

var success = elara.ImproveAttribute(CoreAttribute.Insight);

Console.WriteLine($"Success: {success}");

if (success)
{
    Console.WriteLine($"New Insight value: {elara.Attributes[CoreAttribute.Insight]}");
    Console.WriteLine($"Remaining XP: {elara.ExperiencePoints}");
}

// Try to improve a skill.
Console.WriteLine("\nImproving Probability Threading skill...");

success = elara.ImproveSkill("Probability Threading");

Console.WriteLine($"Success: {success}");

if (success)
{
    Console.WriteLine($"New Probability Threading value: {elara.Skills["Probability Threading"]}");
    Console.WriteLine($"Remaining XP: {elara.ExperiencePoints}");
}

// Display updated character.
Console.WriteLine("\nUpdated character information:");

DisplayCharacter(elara);

// Test saving the character to JSON.
Console.WriteLine("\nSaving character to JSON...");

var json = elara.ToJson();

Console.WriteLine("Character JSON:\n" + json);
Console.WriteLine("\n=== Test Complete ===");
Console.ReadLine();

return;

static void DisplayCharacter(Character character)
{
    Console.WriteLine($"Name: {character.Name}");
    Console.WriteLine($"Origin: {character.Origin}");
    Console.WriteLine($"Specialisation: {character.Specialisation}");
    Console.WriteLine($"Rank: {character.Rank}");
    Console.WriteLine($"XP: {character.ExperiencePoints}");

    Console.WriteLine("\nAttributes:");

    foreach (var attr in character.Attributes)
    {
        Console.WriteLine($"  {attr.Key}: {attr.Value}");
    }

    Console.WriteLine("\nDerived Attributes:");
    Console.WriteLine($"  Manipulation Threshold: {character.ManipulationThreshold}");
    Console.WriteLine($"  Energy Capacity: {character.EnergyCapacity}");
    Console.WriteLine($"  Control Rating: {character.ControlRating}");
    Console.WriteLine($"  Fabric Impact: {character.FabricImpact}");

    Console.WriteLine("\nSkills:");

    foreach (var skill in character.Skills)
    {
        Console.WriteLine($"  {skill.Key}: {skill.Value}");
    }

    Console.WriteLine("\nKnown Manipulations:");

    foreach (var manipulation in character.KnownManipulations)
    {
        Console.WriteLine($"  {manipulation.Name} (Tier {manipulation.Tier})");
    }

    Console.WriteLine("\nEquipment:");

    foreach (var item in character.EquippedItems)
    {
        Console.WriteLine($"  {item.Name}");
    }

    Console.WriteLine("\nResources:");
    Console.WriteLine($"  Health: {character.HealthPoints}/{character.MaxHealthPoints}");
    Console.WriteLine($"  Energy: {character.EnergyPoints}/{character.MaxEnergyPoints}");
    Console.WriteLine($"  Cognitive Strain: {character.CognitiveStrain}");
}

static void DisplayGameEnvironment(GameEnvironment environment)
{
    Console.WriteLine($"GameEnvironment: {environment.Name}");
    Console.WriteLine($"Description: {environment.Description}");
    Console.WriteLine($"Stability: {environment.Stability}");
    Console.WriteLine($"Stability Modifier: {environment.StabilityModifier}");
    Console.WriteLine($"Energy Cost Modifier: {environment.EnergyCostModifier}");

    Console.WriteLine("\nThread Density:");

    foreach (var thread in environment.ThreadDensity)
    {
        Console.WriteLine($"  {thread.Key}: {thread.Value} (Modifier: {environment.GetThreadDensityModifier(thread.Key)})");
    }

    Console.WriteLine("\nAnomalies:");

    if (environment.Anomalies.Count == 0)
    {
        Console.WriteLine("  None");
    }
    else
    {
        foreach (var anomaly in environment.Anomalies)
        {
            Console.WriteLine($"  {anomaly.Name} ({anomaly.Severity})");
        }
    }
}

static void DisplayManipulation(ThreadManipulation manipulation)
{
    Console.WriteLine($"Manipulation: {manipulation.Name}");
    Console.WriteLine($"Description: {manipulation.Description}");
    Console.WriteLine($"Thread Type: {manipulation.ThreadType}");
    Console.WriteLine($"Tier: {manipulation.Tier}");
    Console.WriteLine($"Primary Attribute: {manipulation.PrimaryAttribute}");
    Console.WriteLine($"Attribute Requirement: {manipulation.AttributeRequirement}");
    Console.WriteLine($"Manipulation: {manipulation.Name}");
    Console.WriteLine($"Description: {manipulation.Description}");
    Console.WriteLine($"Thread Type: {manipulation.ThreadType}");
    Console.WriteLine($"Tier: {manipulation.Tier}");
    Console.WriteLine($"Primary Attribute: {manipulation.PrimaryAttribute}");
    Console.WriteLine($"Attribute Requirement: {manipulation.AttributeRequirement}");
    Console.WriteLine($"Energy Cost: {manipulation.EnergyCost}");
    Console.WriteLine($"Required Successes: {manipulation.RequiredSuccesses}");
    Console.WriteLine($"Sustained: {manipulation.IsSustained}");
    Console.WriteLine($"Anchored: {manipulation.IsAnchored}");
    Console.WriteLine($"Area Effect: {manipulation.IsAreaEffect}");
}