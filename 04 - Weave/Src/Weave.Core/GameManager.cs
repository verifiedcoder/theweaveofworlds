using System.Text.Json;
using Weave.Core.Enums;

namespace Weave.Core;

/// <summary>
///     Manager class for handling game state.
/// </summary>
public class GameManager
{
    private GameSession? _currentSession = new();

    /// <summary>
    ///     Start a new game session.
    /// </summary>
    /// <param name="characters">Characters to include.</param>
    /// <param name="gameEnvironment">Starting gameEnvironment.</param>
    public void StartNewSession(List<Character> characters, GameEnvironment gameEnvironment)
    {
        _currentSession = new GameSession
        {
            Characters = characters,
            CurrentGameEnvironment = gameEnvironment,
            Round = 0,
            EventLog = []
        };

        _currentSession.AddEvent("Session started");
    }

    /// <summary>
    ///     Perform a manipulation in the current gameEnvironment.
    /// </summary>
    /// <param name="character">Character performing the manipulation.</param>
    /// <param name="manipulation">Manipulation to perform.</param>
    /// <returns>Result of the manipulation.</returns>
    public ManipulationResult PerformManipulation(Character character, ThreadManipulation manipulation)
    {
        // Get environmental modifiers.
        if (_currentSession == null)
        {
            throw new InvalidOperationException("There is noo current session available.");
        }

        var stabilityModifier = _currentSession.CurrentGameEnvironment.StabilityModifier;
        var threadModifier = _currentSession.CurrentGameEnvironment.GetThreadDensityModifier(manipulation.ThreadType);
        var totalModifier = stabilityModifier + threadModifier;

        // Adjust energy cost based on game environment.
        var energyCostModifier = _currentSession.CurrentGameEnvironment.EnergyCostModifier;

        if (energyCostModifier != 0)
        {
            manipulation.EnergyCost += energyCostModifier;
        }

        // Perform manipulation.
        var result = character.PerformManipulation(manipulation, totalModifier);

        // Log the result.
        _currentSession.AddEvent($"{character.Name} attempts {manipulation.Name}: {result.Message}");

        // Handle fabric stress if it occurred.
        if (result.FabricStress != FabricStressLevel.None)
        {
            HandleFabricStress(result.FabricStress);
        }

        return result;
    }

    /// <summary>
    ///     Handle fabric stress effects.
    /// </summary>
    /// <param name="stressLevel">Level of fabric stress.</param>
    private void HandleFabricStress(FabricStressLevel stressLevel)
    {
        if (_currentSession is null)
        {
            throw new InvalidOperationException("There is noo current session available.");
        }

        switch (stressLevel)
        {
            case FabricStressLevel.Minor:

                _currentSession.AddEvent("Minor fabric stress: Thread patterns distort slightly");

                // Small chance of minor anomaly.
                if (DiceRoller.RollDie(10) == 1)
                {
                    var anomaly = GameEnvironment.GenerateAnomaly();
                    _currentSession.CurrentGameEnvironment.Anomalies.Add(anomaly);
                    _currentSession.AddEvent($"A minor anomaly forms: {anomaly.Name}");
                }

                break;

            case FabricStressLevel.Major:

                _currentSession.AddEvent("Major fabric stress: Reality fluctuates dangerously");

                // Decrease gameEnvironment stability.
                _currentSession.CurrentGameEnvironment.ModifyStability(-1);
                _currentSession.AddEvent(
                    $"GameEnvironment stability decreases to {_currentSession.CurrentGameEnvironment.Stability}");

                // Generate an anomaly.
                var majorAnomaly = GameEnvironment.GenerateAnomaly();
                majorAnomaly.Severity = AnomalySeverity.Moderate;
                _currentSession.CurrentGameEnvironment.Anomalies.Add(majorAnomaly);
                _currentSession.AddEvent($"A moderate anomaly forms: {majorAnomaly.Name}");

                break;

            case FabricStressLevel.None:

                break;

            default:

                throw new ArgumentOutOfRangeException(nameof(stressLevel), stressLevel, null);
        }
    }

    /// <summary>
    ///     Advance time in the game world.
    /// </summary>
    /// <param name="hours">Number of hours to advance.</param>
    public void AdvanceTime(int hours)
    {
        if (_currentSession is null)
        {
            throw new InvalidOperationException("There is noo current session available.");
        }

        // Update characters.
        foreach (var character in _currentSession.Characters)
        {
            character.RecoverEnergy(hours);
            character.RecoverStrain(hours);
        }

        // Update game environment.
        // This would include natural fluctuations, anomaly behavior, etc.
        _currentSession.AddEvent($"Time advances by {hours} hours");
    }

    /// <summary>
    ///     Save the current game state to a file.
    /// </summary>
    /// <param name="filePath">Path to save the file.</param>
    public void SaveGame(string filePath)
    {
        var json = JsonSerializer.Serialize(_currentSession);

        File.WriteAllText(filePath, json);
    }

    /// <summary>
    ///     Load a game state from a file.
    /// </summary>
    /// <param name="filePath">Path to the save file.</param>
    public void LoadGame(string filePath)
    {
        var json = File.ReadAllText(filePath);

        _currentSession = JsonSerializer.Deserialize<GameSession>(json);
    }
}