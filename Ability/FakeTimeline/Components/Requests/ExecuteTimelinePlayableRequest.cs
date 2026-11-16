namespace UniGame.Ecs.Proto.Ability.SubFeatures.FakeTimeline.Components.Requests
{
    using System;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// Запрос выполнить шаг таймлайна: время шага пришло, его система делает свою работу (урон, снаряд,
    /// анимация…).
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ExecuteTimelinePlayableRequest
    {
        public ProtoPackedEntity TimelineContextEntity;
    }
}