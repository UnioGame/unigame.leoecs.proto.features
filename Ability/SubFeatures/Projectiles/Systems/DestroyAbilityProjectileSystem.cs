namespace UniGame.Ecs.Proto.Ability.SubFeatures.Projectiles.Systems
{
    using System;
    using Aspects;
    using Components;
    using Game.Ecs.Core.Components;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using Game.Modules.leoecs.proto.tools.Ownership.Extensions;
    using LeoEcs.Timer.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class DestroyAbilityProjectileSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;

        private ProjectileAbilityAspect _projectileAbilityAspect;
        private OwnershipAspect _ownershipAspect;

        private ProtoItExc _projectileFilter = It
            .Chain<ProjectileComponent>()
            .Inc<CooldownCompleteComponent>()
            .Exc<PrepareToDeathComponent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run()
        {
            foreach (var projectileEntity in _projectileFilter)
            {
                _ownershipAspect.Kill(projectileEntity);
            }
        }
    }
}