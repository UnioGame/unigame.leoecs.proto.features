namespace UniGame.Ecs.Proto.Ability.SubFeatures.FakeTimeline.Components
{
    using System;

    /// <summary>
    /// Шаг таймлайна способности и через сколько секунд от старта он срабатывает.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct TimelinePlayableComponent
    {
        public float delay;
    }
}