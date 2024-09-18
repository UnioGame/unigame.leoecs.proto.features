namespace UniGame.Ecs.Proto.Ability.SubFeatures.Visuals.Components
{
    using System;
    using Leopotam.EcsProto.QoL;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct DelayedVisualsSpawnComponent
    {
        public string assetIdentification;
        public int spawnPosition;
        public bool boneBound;
        public ProtoPackedEntity target;
    }
}