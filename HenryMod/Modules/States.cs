using TarMimic.SkillStates;
using TarMimic.SkillStates.BaseStates;
using System.Collections.Generic;
using System;
using TarMimic.SkillStates.Primary;
using TarMimic.SkillStates.Secondary;

namespace TarMimic.Modules
{
    public static class States
    {
        internal static void RegisterStates()
        {
            Modules.Content.AddEntityState(typeof(TarShotgun));

            Modules.Content.AddEntityState(typeof(TarRifle));

            Modules.Content.AddEntityState(typeof(Roll));

            Modules.Content.AddEntityState(typeof(ChestRetreat));

            Modules.Content.AddEntityState(typeof(ThrowFireBomb));

            Modules.Content.AddEntityState(typeof(ThrowTarBomb));
        }
    }
}