namespace UniGame.Ecs.Proto.Gameplay.FreezingTime.Systems
{
    using System;
    using Aspects;
    using Components;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UnityEngine;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if LITMOTION
    using LitMotion;
#endif

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    [Serializable]
    [ECSDI]
    public sealed class FreezingTimeByLitMotionSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private FreezingTimeAspect _aspect;
        private bool _newTimeScale;

#if LITMOTION
        private MotionHandle _tween;
#endif


        private ProtoIt _filter = It
            .Chain<FreezingTimeRequest>()
            .End();

        public void Run()
        {
            if (_newTimeScale)
            {
                var entityEvent = _world.NewEntity();
                ref var freezingTimeEvent = ref _aspect.freezingTimeCompletedEvent.Add(entityEvent);
                freezingTimeEvent.TimeScale = Time.timeScale;
                _newTimeScale = false;
            }

            foreach (var requestEntity in _filter)
            {
                ref var request = ref _aspect.freezingTimeRequest.Get(requestEntity);
                var newScale = Mathf.Clamp(request.TimeScale, 0f, 1f);
                var duration = request.Duration;

#if LITMOTION
                if (_tween.IsActive())
                    _tween.Cancel();

                _tween = LMotion
                    .Create(Time.timeScale, newScale, duration)
                    .WithOnComplete(() => _newTimeScale = true)
                    .WithScheduler(MotionScheduler.Update)
                    .Bind(x => Time.timeScale = x);
#endif
            }
        }
    }
}