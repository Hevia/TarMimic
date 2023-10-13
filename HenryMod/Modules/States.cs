using TarMimic.SkillStates;
using TarMimic.SkillStates.BaseStates;
using System.Collections.Generic;
using System;
using TarMimic.SkillStates.Primary;
using TarMimic.SkillStates.Secondary;
using TarMimic.SkillStates.Special;
using R2API.Networking.Interfaces;
using UnityEngine.Networking;
using UnityEngine;

namespace TarMimic.Modules
{
    public class SyncTetherPosition : INetMessage, ISerializableObject
    {
        public NetworkInstanceId netIdToUpdate;

        public Vector3 positionSetter;

        public SyncTetherPosition()
        {
        }

        public SyncTetherPosition(NetworkInstanceId netID, Vector3 positionGiven)
        {
            netIdToUpdate = netID;
            positionSetter = positionGiven;
        }

        public void Deserialize(NetworkReader reader)
        {
            netIdToUpdate = reader.ReadNetworkId();
            positionSetter = reader.ReadVector3();
        }

        public void OnReceived()
        {
            if (!NetworkServer.active)
            {
                ClientScene.FindLocalObject(netIdToUpdate).transform.position = positionSetter;
            }
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netIdToUpdate);
            writer.Write(positionSetter);
        }
    }

    public static class States
    {
        internal static void RegisterStates()
        {
            Modules.Content.AddEntityState(typeof(TarShotgun));

            Modules.Content.AddEntityState(typeof(TarRifle));

            Modules.Content.AddEntityState(typeof(Roll));

            Modules.Content.AddEntityState(typeof(ChestRetreat));

            Modules.Content.AddEntityState(typeof(ChestSap));

            Modules.Content.AddEntityState(typeof(ThrowFireBomb));

            Modules.Content.AddEntityState(typeof(ThrowTarBomb));
        }
    }
}