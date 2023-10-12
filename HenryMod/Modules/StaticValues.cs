using System;

namespace TarMimic.Modules
{
    internal static class StaticValues
    {
        internal static string descriptionText = "Mimic is a being reborn by tar with a powerful and mobile close ranged kit." + Environment.NewLine + Environment.NewLine
             + "< ! > Mimic's Blunderbuss is a powerful close ranged slugger that tars enemies. Firing when enemies are nearby grants additional damage and an escape buff." + Environment.NewLine + Environment.NewLine
             + "< ! > Bombs are weak on their own, but ignite enemies. Using bombs with stacks of bomb buffs consumes them and increases damage." + Environment.NewLine + Environment.NewLine
             + "< ! > Launch allows mimic to quickly escape fights, damaging enemies as they reposition. Using launch with an escape buff increases vertical leap." + Environment.NewLine + Environment.NewLine
             + "< ! > Chest Slam is a powerful crowd control attack, allowing mimic to wipe out weak foes, and stun tougher ones. Hitting enemies grants stackable bomb buffs." + Environment.NewLine + Environment.NewLine;

        internal const float swordDamageCoefficient = 2.8f;

        internal const float gunDamageCoefficient = 1.0f; // prev 1.2f, 3.2f

        internal const float bombDamageCoefficient = 1.6f;
        internal const float tarBombDamageCoeff = 3.2f;
        internal const int numberFireBombs = 8;
        internal const int numberTarBombs = 3;
    }
}