namespace UniGame.Ecs.Proto.GameResources.Components
{
    using System;
    using Core.Runtime;
    using Data;
    using Leopotam.EcsProto.QoL;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    [Serializable]
    public struct GameResourceSpawnComponent
    {
        public GamePoint LocationData;
        public Transform Parent;
        public ILifeTime ResourceLifeTime;
    }
}