namespace UniGame.Ecs.Proto.Gameplay.FreezingTime.Systems
{
    using System;
    using Components;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    [Serializable]
    [ECSDI]
    public sealed class DebugCreateFreezingRequestSystem : IProtoRunSystem
    {
        private ProtoWorld _world;

        public void Run()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var entity = _world.NewEntity();
                ref var request = ref _world.AddComponent<FreezingTimeRequest>(entity);

                request.TimeScale = 0.1f;
                request.Duration = 2f;

                Debug.Log($"[FreezingTest] Created request: scale={request.TimeScale}, duration={request.Duration}");
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                var entity = _world.NewEntity();
                ref var request = ref _world.AddComponent<FreezingTimeRequest>(entity);

                request.TimeScale = 1f;
                request.Duration = 2f;

                Debug.Log($"[FreezingTest] Created request: scale={request.TimeScale}, duration={request.Duration}");
            }
        }
    }
}