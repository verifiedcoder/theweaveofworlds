namespace Weave.Core;

/// <summary>
///     Represents a game session with characters and game environment.
/// </summary>
public class GameSession
{
    public List<Character> Characters { get; set; } = [];

    public GameEnvironment CurrentGameEnvironment { get; set; } = new();

    public int Round { get; set; }

    public List<string> EventLog { get; set; } = [];

    public void StartNewRound()
    {
        Round++;
        EventLog.Add($"Round {Round} begins");

        // ToDo: Reset action points, apply ongoing effects, etc.?
    }

    public void AddEvent(string eventDescription) => EventLog.Add(eventDescription);

    public string GetSessionLog() => string.Join("\n", EventLog);
}