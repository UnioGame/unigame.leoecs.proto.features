namespace UniGame.Ecs.Proto.Ability.Common.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// Завершает способность: снимает «используется» и кидает событие завершения.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class CompleteAbilitySystem : IProtoRunSystem,IProtoInitSystem
    {
        private ProtoIt _filter = It
            .Chain<AbilityUsingComponent>()
            .Inc<CompleteAbilitySelfRequest>()
            .End();
        private ProtoWorld _world;
        private AbilityAspect _abilityAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                _abilityAspect.AbilityUsing.Del(entity);
                
                _abilityAspect.CompleteEvent.GetOrAddComponent(entity);
            }
        }
    }
}