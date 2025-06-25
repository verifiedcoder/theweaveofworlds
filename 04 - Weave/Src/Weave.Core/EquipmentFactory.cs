using Weave.Core.Enums;

namespace Weave.Core;

/// <summary>
///     Factory class for creating equipment.
/// </summary>
public static class EquipmentFactory
{
    /// <summary>
    ///     Create basic starting equipment.
    /// </summary>
    /// <param name="specialisation">Character specialisation.</param>
    /// <returns>List of starting equipment.</returns>
    public static List<Equipment> CreateStartingEquipment(Specialisation specialisation)
    {
        var equipment = new List<Equipment>();

        // Common items for all specialisations.
        var energyCapacitor = new Equipment(
            "Energy Capacitor",
            "Stores quantum energy for later use",
            EquipmentType.EnergyManipulator
        );

        energyCapacitor.Modifiers.Add("EnergyStorage", 5);
        energyCapacitor.Properties.Add("RechargeTime", "1 hour");
        energyCapacitor.MaxCharges = 5;
        energyCapacitor.Charges = 5;
        equipment.Add(energyCapacitor);

        var weaverKit = new Equipment(
            "Essential Weaver Kit",
            "Standard tools for thread manipulation",
            EquipmentType.Tool
        );

        weaverKit.Properties.Add("Contents", "Thread Compass, Energy Rations, Stability Patches, Weaver's Journal");
        equipment.Add(weaverKit);

        // Specialisation-specific items.
        switch (specialisation)
        {
            case Specialisation.ThreadWalker:

                var threadLens = new Equipment(
                    "Thread Lens",
                    "Allows visual stablisation of probability threads",
                    EquipmentType.FocusAmplifier
                );

                threadLens.Modifiers.Add("ProbabilityThreading", 1);
                equipment.Add(threadLens);

                var resonantHeadband = new Equipment(
                    "Resonant Headband",
                    "Reduces cognitive strain",
                    EquipmentType.ProtectiveGear
                );

                resonantHeadband.Modifiers.Add("StrainResistance", 1);
                resonantHeadband.Properties.Add("Attunement", "Required");
                equipment.Add(resonantHeadband);

                break;

            case Specialisation.KineticWeaver:

                var quantumResonator = new Equipment(
                    "Quantum Resonator (Force)",
                    "Enhances force thread manipulations",
                    EquipmentType.FocusAmplifier
                );

                quantumResonator.Modifiers.Add("ForceThreading", 1);
                equipment.Add(quantumResonator);

                var threadWovenCoat = new Equipment(
                    "Thread-Woven Coat",
                    "Provides protection and reduces fabric stress",
                    EquipmentType.ProtectiveGear
                );

                threadWovenCoat.Modifiers.Add("FabricStress", -1);
                threadWovenCoat.Properties.Add("PhysicalProtection", "Light");
                equipment.Add(threadWovenCoat);

                break;

            case Specialisation.MatterArchitect:

                var molecularInterface = new Equipment(
                    "Molecular Interface",
                    "Provides detailed analysis of materials",
                    EquipmentType.FocusAmplifier
                );

                molecularInterface.Modifiers.Add("MatterThreading", 1);
                equipment.Add(molecularInterface);

                var stabilityField = new Equipment(
                    "Portable Stability Field",
                    "Creates a small zone of increased stability",
                    EquipmentType.FabricStabiliser
                );

                stabilityField.Properties.Add("Range", "3 meters");
                stabilityField.Properties.Add("Duration", "30 minutes");
                stabilityField.MaxCharges = 2;
                stabilityField.Charges = 2;
                equipment.Add(stabilityField);

                break;

            case Specialisation.ChronoBinder:

                var temporalResonator = new Equipment(
                    "Temporal Resonator",
                    "Enhances perception and manipulation of time",
                    EquipmentType.FocusAmplifier
                );

                temporalResonator.Modifiers.Add("SpacetimeThreading", 1);
                equipment.Add(temporalResonator);

                var chronometricFrame = new Equipment(
                    "Chronometric Frame",
                    "Stabilises personal time flow during manipulations",
                    EquipmentType.ProtectiveGear
                );

                chronometricFrame.Modifiers.Add("TemporalFeedback", -1);
                equipment.Add(chronometricFrame);

                break;

            case Specialisation.QuantumSurgeon:

                var precisionFocus = new Equipment(
                    "Precision Focus",
                    "Enhances fine control over particle manipulation",
                    EquipmentType.FocusAmplifier
                );

                precisionFocus.Modifiers.Add("ThreadPrecision", 1);
                equipment.Add(precisionFocus);

                var fractureSealant = new Equipment(
                    "Fracture Sealant",
                    "Repairs minor fabric damage",
                    EquipmentType.FabricStabiliser
                );

                fractureSealant.Properties.Add("RepairCapacity", "Minor fractures only");
                fractureSealant.MaxCharges = 3;
                fractureSealant.Charges = 3;
                equipment.Add(fractureSealant);

                break;

            default:

                throw new ArgumentOutOfRangeException(nameof(specialisation), specialisation, null);
        }

        return equipment;
    }
}