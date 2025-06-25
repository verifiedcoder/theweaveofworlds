namespace Weave.Core;

/// <summary>
///     Helper class for dice rolling.
/// </summary>
public static class DiceRoller
{
    private static readonly Random Random = new();

    /// <summary>
    ///     Roll a thread test with the given parameters.
    /// </summary>
    /// <param name="dicePool">Number of dice to roll.</param>
    /// <param name="difficulty">Target number for success.</param>
    /// <param name="requiredSuccesses">Number of successes needed.</param>
    /// <returns>Result of the thread test.</returns>
    public static ThreadTestResult RollThreadTest(int dicePool, int difficulty, int requiredSuccesses)
    {
        var results = new List<int>();

        // Ensure at least one die.
        dicePool = Math.Max(1, dicePool);

        // Roll dice.
        for (var i = 0; i < dicePool; i++)
        {
            results.Add(Random.Next(1, 11)); // d10.
        }

        // Count successes.
        var successes = results.Count(r => r >= difficulty);

        // Designate first die as stress die.
        var stressDie = results.Count > 0 ? results[0] : 0;

        return new ThreadTestResult
        {
            Success = successes >= requiredSuccesses,
            Successes = successes,
            StressDieResult = stressDie,
            TotalDice = dicePool,
            DiceResults = results
        };
    }

    /// <summary>
    ///     Roll a single die with the specified number of sides.
    /// </summary>
    /// <param name="sides">Number of sides on the die.</param>
    /// <returns>Result of the roll.</returns>
    public static int RollDie(int sides) => Random.Next(1, sides + 1);
}