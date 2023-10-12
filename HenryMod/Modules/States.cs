using TarMimic.SkillStates;
using TarMimic.SkillStates.BaseStates;
using System.Collections.Generic;
using System;

namespace TarMimic.Modules
{
    public static class States
    {
        internal static void RegisterStates()
        {
            Modules.Content.AddEntityState(typeof(Shoot));

            Modules.Content.AddEntityState(typeof(Roll));

            Modules.Content.AddEntityState(typeof(ChestRetreat));

            Modules.Content.AddEntityState(typeof(ThrowFireBomb));

            Modules.Content.AddEntityState(typeof(ThrowTarBomb));
        }
    }
}