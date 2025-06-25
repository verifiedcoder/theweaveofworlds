using Weave.Core.Enums;

namespace Weave.Core;

/// <summary>
///     Factory class for creating common manipulations.
/// </summary>
public static class ManipulationFactory
{
    /// <summary>
    ///     Create a basic manipulation for the given specialisation.
    /// </summary>
    /// <param name="specialisation">Character specialisation.</param>
    /// <returns>List of starting manipulations.</returns>
    public static List<ThreadManipulation> CreateStartingManipulations(Specialisation specialisation)
    {
        var manipulations = new List<ThreadManipulation>();

        switch (specialisation)
        {
            case Specialisation.ThreadWalker:

                manipulations.Add(new ThreadManipulation(
                                      "Probability Nudge",
                                      "Slightly influence simple random events",
                                      ThreadType.Probability,
                                      Tier.One,
                                      CoreAttribute.Insight,
                                      3,
                                      1,
                                      2));

                manipulations.Add(new ThreadManipulation(
                                      "Pattern Recognition",
                                      "Identify patterns in seemingly random data",
                                      ThreadType.Probability,
                                      Tier.One,
                                      CoreAttribute.Insight,
                                      3,
                                      1,
                                      1));

                // Special ability.
                var threadGlimpse = new ThreadManipulation(
                    "Thread Glimpse",
                    "Brief precognition of immediate future",
                    ThreadType.Probability,
                    Tier.One,
                    CoreAttribute.Insight,
                    4,
                    1,
                    2)
                {
                    IsAnchored = true
                };

                manipulations.Add(threadGlimpse);

                break;

            case Specialisation.KineticWeaver:

                manipulations.Add(new ThreadManipulation(
                                      "Kinetic Push",
                                      "Create directional force",
                                      ThreadType.Force,
                                      Tier.One,
                                      CoreAttribute.Volatility,
                                      3,
                                      1,
                                      2));

                manipulations.Add(new ThreadManipulation(
                                      "Electromagnetic Shift",
                                      "Manipulate electrical currents or magnetic fields",
                                      ThreadType.Force,
                                      Tier.One,
                                      CoreAttribute.Volatility,
                                      3,
                                      1,
                                      1));

                // Special ability.
                var forceCascade = new ThreadManipulation(
                    "Force Cascade",
                    "Chain reactions of kinetic energy",
                    ThreadType.Force,
                    Tier.One,
                    CoreAttribute.Volatility,
                    4,
                    2,
                    2)
                {
                    IsAreaEffect = true
                };
                manipulations.Add(forceCascade);

                break;

            case Specialisation.MatterArchitect:

                manipulations.Add(new ThreadManipulation(
                                      "Molecular Analysis",
                                      "Determine composition of materials",
                                      ThreadType.Matter,
                                      Tier.One,
                                      CoreAttribute.Cognition,
                                      3,
                                      1,
                                      1));

                manipulations.Add(new ThreadManipulation(
                                      "Surface Alteration",
                                      "Change minor properties like texture or color",
                                      ThreadType.Matter,
                                      Tier.One,
                                      CoreAttribute.Cognition,
                                      3,
                                      1,
                                      2));

                // Special ability.
                var molecularMemory = new ThreadManipulation(
                    "Molecular Memory",
                    "Store and recall material states",
                    ThreadType.Matter,
                    Tier.One,
                    CoreAttribute.Cognition,
                    4,
                    2,
                    2)
                {
                    IsAnchored = true
                };

                manipulations.Add(molecularMemory);

                break;

            case Specialisation.ChronoBinder:

                manipulations.Add(new ThreadManipulation(
                                      "Time Sense",
                                      "Perceive time flow accurately",
                                      ThreadType.Spacetime,
                                      Tier.One,
                                      CoreAttribute.Coherence,
                                      3,
                                      1,
                                      1));

                manipulations.Add(new ThreadManipulation(
                                      "Minor Dilation",
                                      "Slightly accelerate or decelerate personal time",
                                      ThreadType.Spacetime,
                                      Tier.One,
                                      CoreAttribute.Coherence,
                                      3,
                                      1,
                                      2));

                // Special ability.
                var timeEcho = new ThreadManipulation(
                    "Time Echo",
                    "Create brief temporal duplicate",
                    ThreadType.Spacetime,
                    Tier.One,
                    CoreAttribute.Coherence,
                    4,
                    2,
                    2
                )
                {
                    IsSustained = true
                };

                manipulations.Add(timeEcho);

                break;

            case Specialisation.QuantumSurgeon:

                manipulations.Add(new ThreadManipulation(
                                      "Particle Shift",
                                      "Alter individual particle properties",
                                      ThreadType.Matter,
                                      Tier.One,
                                      CoreAttribute.Insight,
                                      3,
                                      1,
                                      1));

                manipulations.Add(new ThreadManipulation(
                                      "Quantum Diagnosis",
                                      "Detect fabric irregularities",
                                      ThreadType.Matter,
                                      Tier.One,
                                      CoreAttribute.Insight,
                                      3,
                                      1,
                                      2));

                // Special ability.
                var quantumStitch = new ThreadManipulation(
                    "Quantum Stitch",
                    "Repair minor fabric damage",
                    ThreadType.Matter,
                    Tier.One,
                    CoreAttribute.Insight,
                    4,
                    2,
                    2)
                {
                    IsAnchored = true
                };

                manipulations.Add(quantumStitch);

                break;

            default:

                throw new ArgumentOutOfRangeException(nameof(specialisation), specialisation, null);
        }

        return manipulations;
    }
}