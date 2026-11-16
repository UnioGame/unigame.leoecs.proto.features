namespace UniGame.Ecs.Proto.TargetSelection.Components
{
    using System;

    /// <summary>
    /// Метка: этой сущности каждый кадр нужен поиск целей в радиусе. Параметры поиска лежат в
    /// SqrRangeTargetSelectionComponent.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SqrRangeFilterTargetComponent
    {
    }
}