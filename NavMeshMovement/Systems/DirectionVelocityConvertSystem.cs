namespace UniGame.Ecs.Proto.Movement.Systems.Converters
{
    using System;
    using UniGame.LeoEcs.Shared.Extensions;
    using Aspect;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Система отвечающая за конвертацию map space направления в вектор скорости.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class DirectionVelocityConvertSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private NavMeshAgentAspect _navigationAspect;

        private ProtoIt _filter = It
            .Chain<SetNavAgentVelocitySelfRequest>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var direction = ref _navigationAspect.SetNavAgentVelocity.Get(entity);
                ref var velocity = ref _navigationAspect.Velocity.GetOrAddComponent(entity);
                
                velocity.Value = direction.Value;
            }
        }
    }
}