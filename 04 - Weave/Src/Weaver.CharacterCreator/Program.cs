using Weave.Core;
using Weave.Core.Enums;

Console.WriteLine("=== QUANTUM WEAVER CHARACTER CREATOR ===\n");

var character = QuickCreate();
var done = false;

while (!done)
{
    DisplayMenu();

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":

            SetBasicInfo(character);

            break;

        case "2":

            AssignAttributes(character);

            break;

        case "3":

            AssignSkills(character);

            break;

        case "4":

            SelectManipulations(character);

            break;

        case "5":

            SelectEquipment(character);

            break;

        case "6":

            DisplayCharacter(character);

            break;

        case "7":

            SaveCharacter(character);

            break;

        case "8":

            LoadCharacter(ref character);

            break;

        case "0":

            done = true;

            break;

        default:

            Console.WriteLine("Invalid choice. Please try again.");

            break;
    }
}

Console.WriteLine("\nThank you for using the Quantum Weaver Character Creator!");

return;

static void DisplayMenu()
{
    Console.WriteLine("\n--- MAIN MENU ---");
    Console.WriteLine("1. Set Basic Information");
    Console.WriteLine("2. Assign Attributes");
    Console.WriteLine("3. Assign Skills");
    Console.WriteLine("4. Select Manipulations");
    Console.WriteLine("5. Select Equipment");
    Console.WriteLine("6. Display Character");
    Console.WriteLine("7. Save Character");
    Console.WriteLine("8. Load Character");
    Console.WriteLine("9. Quick Create");
    Console.WriteLine("0. Exit");
    Console.Write("\nEnter your choice: ");
}

static void SetBasicInfo(Character character)
{
    Console.Clear();
    Console.WriteLine("=== BASIC INFORMATION ===\n");

    Console.Write("Character Name: ");
    character.Name = Console.ReadLine() ?? throw new InvalidOperationException("Name is required.");

    Console.WriteLine("\nSelect Origin:");
    Console.WriteLine("1. Academic (+1 COG, +1 COH)");
    Console.WriteLine("2. Intuitive (+1 INS, +1 RES)");
    Console.WriteLine("3. Military (+1 VOL, +1 COH)");
    Console.WriteLine("4. Hermetic (+1 RES, +1 INS)");

    Console.Write("\nEnter choice (1-4): ");
    var originChoice = Console.ReadLine();

    switch (originChoice)
    {
        case "1":

            character.Origin = Origin.Academic;

            break;

        case "2":

            character.Origin = Origin.Intuitive;

            break;

        case "3":

            character.Origin = Origin.Military;

            break;

        case "4":

            character.Origin = Origin.Hermetic;

            break;

        default:

            Console.WriteLine("Invalid choice. Defaulting to Academic.");
            character.Origin = Origin.Academic;

            break;
    }

    Console.WriteLine("\nSelect Specialisation:");
    Console.WriteLine("1. Thread Walker (Probability manipulation)");
    Console.WriteLine("2. Kinetic Weaver (Force manipulation)");
    Console.WriteLine("3. Matter Architect (Matter manipulation)");
    Console.WriteLine("4. Chrono Binder (Spacetime manipulation)");
    Console.WriteLine("5. Quantum Surgeon (Precise particle manipulation)");

    Console.Write("\nEnter choice (1-5): ");
    var specChoice = Console.ReadLine();

    switch (specChoice)
    {
        case "1":

            character.Specialisation = Specialisation.ThreadWalker;

            break;

        case "2":

            character.Specialisation = Specialisation.KineticWeaver;

            break;

        case "3":

            character.Specialisation = Specialisation.MatterArchitect;

            break;

        case "4":

            character.Specialisation = Specialisation.ChronoBinder;

            break;

        case "5":

            character.Specialisation = Specialisation.QuantumSurgeon;

            break;

        default:

            Console.WriteLine("Invalid choice. Defaulting to Thread Walker.");
            character.Specialisation = Specialisation.ThreadWalker;

            break;
    }

    // Apply origin bonuses
    character.ApplyOriginBonuses();

    Console.WriteLine("\nBasic information set!");
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}

static void AssignAttributes(Character character)
{
    Console.Clear();
    Console.WriteLine("=== ATTRIBUTE ASSIGNMENT ===\n");
    Console.WriteLine("You have 15 points to distribute among attributes.");
    Console.WriteLine("Minimum value: 1, Maximum value: 6\n");

    // Reset attributes
    foreach (var attr in Enum.GetValues<CoreAttribute>())
    {
        character.Attributes[attr] = 1;
    }

    var remainingPoints = 10; // 5 points already used for minimum values

    Console.WriteLine("Current Attributes:");

    foreach (var attr in Enum.GetValues<CoreAttribute>())
    {
        Console.WriteLine($"{attr}: {character.Attributes[attr]}");
    }

    while (remainingPoints > 0)
    {
        Console.WriteLine($"\nRemaining points: {remainingPoints}");
        Console.WriteLine("Select attribute to increase:");
        var i = 1;

        foreach (var attr in Enum.GetValues<CoreAttribute>())
        {
            Console.WriteLine($"{i}. {attr} (Current: {character.Attributes[attr]})");
            i++;
        }

        Console.Write("\nEnter choice (1-5), or 0 to finish: ");
        var choice = Console.ReadLine();

        if (choice == "0")
        {
            break;
        }

        if (int.TryParse(choice, out var attrIndex) && attrIndex is >= 1 and <= 5)
        {
            var selectedAttr = (CoreAttribute)(attrIndex - 1);

            if (character.Attributes[selectedAttr] < 6)
            {
                character.Attributes[selectedAttr]++;
                remainingPoints--;

                Console.WriteLine($"{selectedAttr} increased to {character.Attributes[selectedAttr]}");
            }
            else
            {
                Console.WriteLine($"{selectedAttr} is already at maximum value (6)");
            }
        }
        else
        {
            Console.WriteLine("Invalid choice. Please try again.");
        }

        Console.WriteLine("\nCurrent Attributes:");

        foreach (var attr in Enum.GetValues<CoreAttribute>())
        {
            Console.WriteLine($"{attr}: {character.Attributes[attr]}");
        }
    }

    // Apply origin bonuses again
    character.ApplyOriginBonuses();

    // Recalculate derived attributes
    character.RecalculateResources();

    Console.WriteLine("\nAttributes assigned!");
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}

static void AssignSkills(Character character)
{
    Console.Clear();
    Console.WriteLine("=== SKILL ASSIGNMENT ===\n");
    Console.WriteLine("You have 10 skill points to distribute.");
    Console.WriteLine("Maximum skill value at character creation: 3\n");

    // Reset skills
    character.Skills.Clear();

    var remainingPoints = 10;

    // Define available skills
    var threadSkills = new List<string>
    {
        "Force Threading",
        "Matter Threading",
        "Probability Threading",
        "Spacetime Threading"
    };

    var technicalSkills = new List<string>
    {
        "Quantum Theory",
        "Equipment Operation",
        "Anomaly Analysis",
        "Fabric Engineering"
    };

    var generalSkills = new List<string>
    {
        "Perception",
        "Athletics",
        "Social Interaction",
        "Technical Knowledge"
    };

    while (remainingPoints > 0)
    {
        Console.WriteLine($"\nRemaining points: {remainingPoints}");
        Console.WriteLine("Select skill category:");
        Console.WriteLine("1. Thread Skills");
        Console.WriteLine("2. Technical Skills");
        Console.WriteLine("3. General Skills");
        Console.WriteLine("4. View Current Skills");
        Console.WriteLine("0. Finish Skill Assignment");

        Console.Write("\nEnter choice: ");
        var categoryChoice = Console.ReadLine();

        if (categoryChoice == "0")
        {
            break;
        }

        if (categoryChoice == "4")
        {
            DisplayCurrentSkills(character);

            continue;
        }

        List<string> selectedCategory;

        switch (categoryChoice)
        {
            case "1":

                selectedCategory = threadSkills;
                Console.WriteLine("\n=== THREAD SKILLS ===");

                break;

            case "2":

                selectedCategory = technicalSkills;
                Console.WriteLine("\n=== TECHNICAL SKILLS ===");

                break;

            case "3":

                selectedCategory = generalSkills;
                Console.WriteLine("\n=== GENERAL SKILLS ===");

                break;

            default:

                Console.WriteLine("Invalid category. Please try again.");

                continue;
        }

        // Display skills in the selected category
        for (var i = 0; i < selectedCategory.Count; i++)
        {
            var skill = selectedCategory[i];
            var currentValue = character.Skills.GetValueOrDefault(skill, 0);
            Console.WriteLine($"{i + 1}. {skill} (Current: {currentValue})");
        }

        Console.Write("\nSelect skill to increase (or 0 to go back): ");
        var skillChoice = Console.ReadLine();

        if (skillChoice == "0")
        {
            continue;
        }

        if (int.TryParse(skillChoice, out var skillIndex) && skillIndex >= 1 && skillIndex <= selectedCategory.Count)
        {
            var selectedSkill = selectedCategory[skillIndex - 1];

            character.Skills.TryAdd(selectedSkill, 0);

            if (character.Skills[selectedSkill] < 3)
            {
                character.Skills[selectedSkill]++;
                remainingPoints--;

                Console.WriteLine($"{selectedSkill} increased to {character.Skills[selectedSkill]}");
            }
            else
            {
                Console.WriteLine($"{selectedSkill} is already at maximum starting value (3)");
            }
        }
        else
        {
            Console.WriteLine("Invalid skill. Please try again.");
        }
    }

    Console.WriteLine("\nSkills assigned!");
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}

static void DisplayCurrentSkills(Character character)
{
    Console.WriteLine("\n=== CURRENT SKILLS ===");

    if (character.Skills.Count == 0)
    {
        Console.WriteLine("No skills assigned yet.");

        return;
    }

    Console.WriteLine("\nThread Skills:");

    foreach (var skill in new[] { "Force Threading", "Matter Threading", "Probability Threading", "Spacetime Threading" })
    {
        if (character.Skills.TryGetValue(skill, out var characterSkill))
        {
            Console.WriteLine($"  {skill}: {characterSkill}");
        }
    }

    Console.WriteLine("\nTechnical Skills:");

    foreach (var skill in new[] { "Quantum Theory", "Equipment Operation", "Anomaly Analysis", "Fabric Engineering" })
    {
        if (character.Skills.TryGetValue(skill, out var characterSkill))
        {
            Console.WriteLine($"  {skill}: {characterSkill}");
        }
    }

    Console.WriteLine("\nGeneral Skills:");

    foreach (var skill in new[] { "Perception", "Athletics", "Social Interaction", "Technical Knowledge" })
    {
        if (character.Skills.TryGetValue(skill, out var characterSkill))
        {
            Console.WriteLine($"  {skill}: {characterSkill}");
        }
    }

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

static void SelectManipulations(Character character)
{
    Console.Clear();
    Console.WriteLine("=== MANIPULATION SELECTION ===\n");

    // Clear existing manipulations
    character.KnownManipulations.Clear();

    // Get starting manipulations based on specialisation
    var availableManipulations = ManipulationFactory.CreateStartingManipulations(character.Specialisation);

    Console.WriteLine($"As a {character.Specialisation}, you automatically know these manipulations:");

    foreach (var manipulation in availableManipulations)
    {
        Console.WriteLine($" - {manipulation.Name} ({manipulation.ThreadType}, Tier {manipulation.Tier})");
        character.KnownManipulations.Add(manipulation);
    }

    // Offer additional basic manipulations
    Console.WriteLine("\nYou may select one additional Tier 1 manipulation from another thread type:");

    var additionalOptions = new List<ThreadManipulation>();

    // Add some options based on thread types different from the character's specialisation
    var specialStabiliserType = GetSpecialisationThreadType(character.Specialisation);

    additionalOptions.AddRange(
        Enum.GetValues<ThreadType>()
            .Where(threadType => threadType != specialStabiliserType)
            .Select(CreateBasicManipulation));

    // Display additional options
    for (var i = 0; i < additionalOptions.Count; i++)
    {
        var manipulation = additionalOptions[i];
        Console.WriteLine($"{i + 1}. {manipulation.Name} ({manipulation.ThreadType}, Tier {manipulation.Tier})");
        Console.WriteLine($"   Description: {manipulation.Description}");
        Console.WriteLine($"   Attribute: {manipulation.PrimaryAttribute}, Energy: {manipulation.EnergyCost}");
        Console.WriteLine();
    }

    Console.Write("Select an additional manipulation (1-3), or 0 to skip: ");
    
    var choice = Console.ReadLine();

    if (int.TryParse(choice, out var index) && index >= 1 && index <= additionalOptions.Count)
    {
        character.KnownManipulations.Add(additionalOptions[index - 1]);
        
        Console.WriteLine($"Added {additionalOptions[index - 1].Name} to your known manipulations.");
    }
    else if (choice != "0")
    {
        Console.WriteLine("Invalid choice. No additional manipulation selected.");
    }

    Console.WriteLine("\nManipulations selected!");
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}

static ThreadType GetSpecialisationThreadType(Specialisation specialisation)
{
    return specialisation switch
    {
        Specialisation.ThreadWalker    => ThreadType.Probability,
        Specialisation.KineticWeaver   => ThreadType.Force,
        Specialisation.MatterArchitect => ThreadType.Matter,
        Specialisation.ChronoBinder    => ThreadType.Spacetime,
        Specialisation.QuantumSurgeon  => ThreadType.Matter // Quantum Surgeons primarily use Matter threads
       ,
        _ => ThreadType.Force
    };
}

static ThreadManipulation CreateBasicManipulation(ThreadType threadType)
{
    return threadType switch
    {
        ThreadType.Force       => new ThreadManipulation("Kinetic Push", "Create directional force", ThreadType.Force, Tier.One, CoreAttribute.Volatility, 3, 1, 2),
        ThreadType.Matter      => new ThreadManipulation("Molecular Analysis", "Determine composition of materials", ThreadType.Matter, Tier.One, CoreAttribute.Cognition, 3, 1, 1),
        ThreadType.Probability => new ThreadManipulation("Probability Nudge", "Slightly influence simple random events", ThreadType.Probability, Tier.One, CoreAttribute.Insight, 3, 1, 2),
        ThreadType.Spacetime   => new ThreadManipulation("Time Sense", "Perceive time flow accurately", ThreadType.Spacetime, Tier.One, CoreAttribute.Coherence, 3, 1, 1),
        _                      => new ThreadManipulation("Basic Manipulation", "Generic basic manipulation", threadType, Tier.One, CoreAttribute.Coherence, 3, 1, 1)
    };
}

static void SelectEquipment(Character character)
{
    Console.Clear();
    Console.WriteLine("=== EQUIPMENT SELECTION ===\n");

    // Clear existing equipment
    character.EquippedItems.Clear();

    // Get starting equipment based on specialisation
    var startingEquipment = EquipmentFactory.CreateStartingEquipment(character.Specialisation);

    Console.WriteLine($"As a {character.Specialisation}, you automatically have this equipment:");

    foreach (var item in startingEquipment)
    {
        Console.WriteLine($" - {item.Name}");
        character.EquippedItems.Add(item);
    }

    // Allow selection of additional personal items
    Console.WriteLine("\nYou may select one additional personal item:");

    var personalItems = new List<Equipment>
    {
        new(
            "Thread Compass",
            "Indicates direction of thread disturbances",
            EquipmentType.Tool
        ),

        new(
            "Weaver's Journal",
            "Record observations and manipulations",
            EquipmentType.Tool
        ),

        new(
            "Focus Charm",
            "Provides +1 die to first manipulation of the day",
            EquipmentType.FocusAmplifier
        )
    };

    // Add modifiers to items
    personalItems[2].Modifiers.Add("FirstManipulation", 1);

    // Display personal items
    for (var i = 0; i < personalItems.Count; i++)
    {
        var item = personalItems[i];
        
        Console.WriteLine($"{i + 1}. {item.Name}");
        Console.WriteLine($"   Description: {item.Description}");
        Console.WriteLine();
    }

    Console.Write("Select a personal item (1-3), or 0 to skip: ");
    
    var choice = Console.ReadLine();

    if (int.TryParse(choice, out var index) && index >= 1 && index <= personalItems.Count)
    {
        character.EquippedItems.Add(personalItems[index - 1]);
        
        Console.WriteLine($"Added {personalItems[index - 1].Name} to your equipment.");
    }
    else if (choice != "0")
    {
        Console.WriteLine("Invalid choice. No personal item selected.");
    }

    Console.WriteLine("\nEquipment selected!");
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}

static void DisplayCharacter(Character character)
{
    Console.Clear();
    Console.WriteLine("=== CHARACTER SHEET ===\n");

    Console.WriteLine($"Name: {character.Name}");
    Console.WriteLine($"Origin: {character.Origin}");
    Console.WriteLine($"Specialisation: {character.Specialisation}");
    Console.WriteLine($"Rank: {character.Rank}");
    Console.WriteLine($"XP: {character.ExperiencePoints}");

    Console.WriteLine("\n--- ATTRIBUTES ---");

    foreach (var attr in character.Attributes)
    {
        Console.WriteLine($"{attr.Key}: {attr.Value}");
    }

    Console.WriteLine("\n--- DERIVED ATTRIBUTES ---");
    Console.WriteLine($"Manipulation Threshold (MT): {character.ManipulationThreshold}");
    Console.WriteLine($"Energy Capacity (EC): {character.EnergyCapacity}");
    Console.WriteLine($"Control Rating (CR): {character.ControlRating}");
    Console.WriteLine($"Fabric Impact (FI): {character.FabricImpact}");

    Console.WriteLine("\n--- SKILLS ---");

    if (character.Skills.Count == 0)
    {
        Console.WriteLine("No skills assigned.");
    }
    else
    {
        // Thread Skills
        Console.WriteLine("Thread Skills:");

        foreach (var skill in new[] { "Force Threading", "Matter Threading", "Probability Threading", "Spacetime Threading" })
        {
            if (character.Skills.TryGetValue(skill, out var characterSkill))
            {
                Console.WriteLine($"  {skill}: {characterSkill}");
            }
        }

        // Technical Skills
        Console.WriteLine("\nTechnical Skills:");

        foreach (var skill in new[] { "Quantum Theory", "Equipment Operation", "Anomaly Analysis", "Fabric Engineering" })
        {
            if (character.Skills.TryGetValue(skill, out var characterSkill))
            {
                Console.WriteLine($"  {skill}: {characterSkill}");
            }
        }

        // General Skills
        Console.WriteLine("\nGeneral Skills:");

        foreach (var skill in new[] { "Perception", "Athletics", "Social Interaction", "Technical Knowledge" })
        {
            if (character.Skills.TryGetValue(skill, out var characterSkill))
            {
                Console.WriteLine($"  {skill}: {characterSkill}");
            }
        }
    }

    Console.WriteLine("\n--- KNOWN MANIPULATIONS ---");

    if (character.KnownManipulations.Count == 0)
    {
        Console.WriteLine("No manipulations known.");
    }
    else
    {
        foreach (var manipulation in character.KnownManipulations)
        {
            Console.WriteLine($"{manipulation.Name} (Tier {manipulation.Tier})");
            Console.WriteLine($"  Thread Type: {manipulation.ThreadType}");
            Console.WriteLine($"  Energy Cost: {manipulation.EnergyCost}");
            Console.WriteLine($"  Required Successes: {manipulation.RequiredSuccesses}");
            Console.WriteLine();
        }
    }

    Console.WriteLine("\n--- EQUIPMENT ---");

    if (character.EquippedItems.Count == 0)
    {
        Console.WriteLine("No equipment.");
    }
    else
    {
        foreach (var item in character.EquippedItems)
        {
            Console.WriteLine($"{item.Name} ({item.Type})");
            Console.WriteLine($"  Description: {item.Description}");

            if (item.Modifiers.Count > 0)
            {
                Console.WriteLine("  Modifiers:");

                foreach (var modifier in item.Modifiers)
                {
                    Console.WriteLine($"    {modifier.Key}: {modifier.Value}");
                }
            }

            if (item.Properties.Count > 0)
            {
                Console.WriteLine("  Properties:");

                foreach (var property in item.Properties)
                {
                    Console.WriteLine($"    {property.Key}: {property.Value}");
                }
            }

            Console.WriteLine();
        }
    }

    Console.WriteLine("\n--- RESOURCES ---");
    Console.WriteLine($"Health Points: {character.HealthPoints}/{character.MaxHealthPoints}");
    Console.WriteLine($"Energy Points: {character.EnergyPoints}/{character.MaxEnergyPoints}");
    Console.WriteLine($"Cognitive Strain: {character.CognitiveStrain}");

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

static void SaveCharacter(Character character)
{
    Console.Clear();
    Console.WriteLine("=== SAVE CHARACTER ===\n");

    Console.Write("Enter filename (without extension): ");
    
    var filename = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(filename))
    {
        filename = character.Name;

        if (string.IsNullOrWhiteSpace(filename))
        {
            filename = "unnamed_character";
        }
    }

    filename = filename.Replace(" ", "_");
    
    var path = $"{filename}.json";

    try
    {
        var json = character.ToJson();
        
        File.WriteAllText(path, json);
        
        Console.WriteLine($"\nCharacter saved successfully as '{path}'");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nError saving character: {ex.Message}");
    }

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

static void LoadCharacter(ref Character character)
{
    Console.Clear();
    Console.WriteLine("=== LOAD CHARACTER ===\n");

    var files = Directory.GetFiles(".", "*.json");

    if (files.Length == 0)
    {
        Console.WriteLine("No character files found.");
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();

        return;
    }

    Console.WriteLine("Available character files:");

    for (var i = 0; i < files.Length; i++)
    {
        Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
    }

    Console.Write("\nEnter file number to load, or 0 to cancel: ");
    
    var choice = Console.ReadLine();

    if (int.TryParse(choice, out var index) && index >= 1 && index <= files.Length)
    {
        try
        {
            var json = File.ReadAllText(files[index - 1]);
            
            character = Character.FromJson(json) ?? throw new InvalidOperationException();
            
            Console.WriteLine("\nCharacter loaded successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError loading character: {ex.Message}");
        }
    }
    else if (choice != "0")
    {
        Console.WriteLine("\nInvalid choice. No character loaded.");
    }

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

static Character QuickCreate()
{
    Console.Clear();
    Console.WriteLine("=== QUICK CHARACTER CREATION ===\n");

    Console.Write("Character Name: ");
    
    var name = Console.ReadLine() ?? throw new InvalidOperationException();

    Console.WriteLine("\nSelect Origin:");
    Console.WriteLine("1. Academic (+1 COG, +1 COH)");
    Console.WriteLine("2. Intuitive (+1 INS, +1 RES)");
    Console.WriteLine("3. Military (+1 VOL, +1 COH)");
    Console.WriteLine("4. Hermetic (+1 RES, +1 INS)");

    Console.Write("\nEnter choice (1-4): ");
    
    var originChoice = Console.ReadLine();

    Origin origin;

    switch (originChoice)
    {
        case "1":

            origin = Origin.Academic;

            break;

        case "2":

            origin = Origin.Intuitive;

            break;

        case "3":

            origin = Origin.Military;

            break;

        case "4":

            origin = Origin.Hermetic;

            break;

        default:

            Console.WriteLine("Invalid choice. Defaulting to Academic.");
            
            origin = Origin.Academic;

            break;
    }

    Console.WriteLine("\nSelect Specialisation:");
    Console.WriteLine("1. Thread Walker (Probability manipulation)");
    Console.WriteLine("2. Kinetic Weaver (Force manipulation)");
    Console.WriteLine("3. Matter Architect (Matter manipulation)");
    Console.WriteLine("4. Chrono Binder (Spacetime manipulation)");
    Console.WriteLine("5. Quantum Surgeon (Precise particle manipulation)");

    Console.Write("\nEnter choice (1-5): ");
    
    var specChoice = Console.ReadLine();

    Specialisation specialisation;

    switch (specChoice)
    {
        case "1":

            specialisation = Specialisation.ThreadWalker;

            break;

        case "2":

            specialisation = Specialisation.KineticWeaver;

            break;

        case "3":

            specialisation = Specialisation.MatterArchitect;

            break;

        case "4":

            specialisation = Specialisation.ChronoBinder;

            break;

        case "5":

            specialisation = Specialisation.QuantumSurgeon;

            break;

        default:

            Console.WriteLine("Invalid choice. Defaulting to Thread Walker.");
            specialisation = Specialisation.ThreadWalker;

            break;
    }

    // Create the character using the factory
    return CharacterFactory.CreateCharacter(name, origin, specialisation);
}