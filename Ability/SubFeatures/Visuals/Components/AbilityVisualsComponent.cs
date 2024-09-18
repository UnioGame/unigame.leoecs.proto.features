namespace UniGame.Ecs.Proto.Ability.SubFeatures.Visuals.Components
{
    using System;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityVisualsComponent
    {
        public string assetIdentification;
        public float spawnDelay;
        public int spawnPosition;
        public bool targetSource;
        public bool boneBound;
    }
}