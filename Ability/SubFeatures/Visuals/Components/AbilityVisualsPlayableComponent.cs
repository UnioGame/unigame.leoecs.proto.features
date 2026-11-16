namespace UniGame.Ecs.Proto.Ability.SubFeatures.Visuals.Components
{
    using System;

    /// <summary>
    /// Шаг способности «визуал»: какой эффект заспавнить, в какой точке и прицепить ли к кости.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityVisualsPlayableComponent
    {
        public string assetIdentification;
        public int spawnPosition;
        public bool boneBound;
    }
}