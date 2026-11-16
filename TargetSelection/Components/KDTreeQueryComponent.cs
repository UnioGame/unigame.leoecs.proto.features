namespace UniGame.Ecs.Proto.TargetSelection.Components
{
    using System;
    using UnioGame.KDTree;

    /// <summary>
    /// Переиспользуемый запрос «кто в радиусе» к kd-дереву, чтобы не создавать его на каждый поиск.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct KDTreeQueryComponent
    {
        public KDQuery Value;
    }
}