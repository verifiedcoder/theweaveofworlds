using Weave.Core.Enums;

namespace Weave.Core;

/// <summary>
///     Represents an game environment with quantum fabric properties.
/// </summary>
public class GameEnvironment
{
    public GameEnvironment()
    {
        Name = "New GameEnvironment";
        Description = "Description of the gameEnvironment";
        Stability = StabilityLevel.StandardField;
        ThreadDensity = new Dictionary<ThreadType, double>();

        foreach (var type in Enum.GetValues<ThreadType>())
        {
            // Standard density
            ThreadDensity[type] = 1.0;
        }

        Anomalies = [];
    }

    public GameEnvironment(string name, string description, StabilityLevel stability)
    {
        Name = name;
        Description = description;
        Stability = stability;
        ThreadDensity = new Dictionary<ThreadType, double>();

        foreach (var type in Enum.GetValues<ThreadType>())
        {
            // Standard density.
            ThreadDensity[type] = 1.0;
        }

        Anomalies = [];
    }

    public string Name { get; set; }

    public string Description { get; set; }

    public StabilityLevel Stability { get; set; }

    public Dictionary<ThreadType, double> ThreadDensity { get; set; }

    public int StabilityModifier => CalculateStabilityModifier();

    public int EnergyCostModifier => CalculateEnergyCostModifier();

    public List<Anomaly> Anomalies { get; set; }

    /// <summary>
    ///     Calculate the dice modifier based on stability level.
    /// </summary>
    /// <returns>Dice pool modifier.</returns>
    private int CalculateStabilityModifier() => Stability switch
    {
        StabilityLevel.AnchorZone     => 2,
        StabilityLevel.HarmonicRegion => 1,
        StabilityLevel.StandardField  => 0,
        StabilityLevel.TensionZone    => -1,
        StabilityLevel.FractureBelt   => -2,
        StabilityLevel.VoidRegion     => -3,
        _                             => 0
    };

    /// <summary>
    ///     Calculate energy cost modifier based on stability level.
    /// </summary>
    /// <returns>Energy cost modifier.</returns>
    private int CalculateEnergyCostModifier() => Stability switch
    {
        StabilityLevel.AnchorZone   => -1,
        StabilityLevel.FractureBelt => 1,
        StabilityLevel.VoidRegion   => 2 // Doubled energy cost
       ,
        _ => 0
    };

    /// <summary>
    ///     Get thread density modifier for a specific thread type.
    /// </summary>
    /// <param name="threadType">Thread type to check.</param>
    /// <returns>Dice pool modifier.</returns>
    public int GetThreadDensityModifier(ThreadType threadType)
    {
        var density = ThreadDensity[threadType];

        return density switch
        {
            >= 2.0 => 2,
            >= 1.5 => 1,
            <= 0.5 => -1,
            _      => 0
        };
    }

    /// <summary>
    ///     Change stability level due to manipulation effects.
    /// </summary>
    /// <param name="change">Amount of change (-2 to +2).</param>
    public void ModifyStability(int change)
    {
        var currentLevel = (int)Stability;
        var newLevel = Math.Max(0, Math.Min(Enum.GetValues<StabilityLevel>().Length - 1, currentLevel + change));

        Stability = (StabilityLevel)newLevel;
    }

    /// <summary>
    ///     Generate a random anomaly based on current stability.
    /// </summary>
    /// <returns>Created anomaly.</returns>
    /// <remarks>ToDo: Expand to a more detailed implementation.</remarks>
    public static Anomaly GenerateAnomaly() => new()
    {
        Name = "Random Anomaly",
        Description = "A randomly generated anomaly",
        ThreadType = ThreadType.Force, // Randomly selected
        Severity = AnomalySeverity.Minor
    };
}