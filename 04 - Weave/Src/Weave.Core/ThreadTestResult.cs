namespace Weave.Core;

/// <summary>
///     Represents the result of a thread test.
/// </summary>
public class ThreadTestResult
{
    /// <summary>
    ///     Gets or sets a value indicating whether this <see cref="ThreadTestResult"/> is success.
    /// </summary>
    /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
    public bool Success { get; set; }

    /// <summary>
    ///     Gets or sets the successes.
    /// </summary>
    /// <value>The successes.</value>
    public int Successes { get; set; }

    /// <summary>
    ///     Gets or sets the stress die result.
    /// </summary>
    /// <value>The stress die result.</value>
    public int StressDieResult { get; set; }

    /// <summary>
    ///     Gets or sets the total dice.
    /// </summary>
    /// <value>The total dice.</value>
    public int TotalDice { get; set; }

    /// <summary>
    ///     Gets or sets the dice results.
    /// </summary>
    /// <value>The dice results.</value>
    public List<int> DiceResults { get; set; } = [];
}