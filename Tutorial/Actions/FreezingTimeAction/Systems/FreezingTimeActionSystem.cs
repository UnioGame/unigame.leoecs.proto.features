namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.FreezingTimeAction.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Freezing time action
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class FreezingTimeActionSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private FreezingTimeActionAspect _aspect;

        private ProtoItExc _filter = It
            .Chain<FreezingTimeActionComponent>()
            .Exc<CompletedFreezingTimeActionComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var freezingTimeActionComponent = ref _aspect.FreezingTimeAction.Get(entity);
                var requestEntity = _world.NewEntity();
                ref var requestComponent = ref _aspect.FreezingTimeRequest.Add(requestEntity);
                requestComponent.Duration = freezingTimeActionComponent.Duration;
                requestComponent.TimeScale = freezingTimeActionComponent.TimeScale;
                _aspect.CompletedFreezingTimeAction.Add(entity);
            }
        }
    }
}