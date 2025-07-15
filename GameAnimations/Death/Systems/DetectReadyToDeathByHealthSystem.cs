namespace UniGame.Ecs.Proto.Gameplay.Death.Systems
{
    using System;
    using Characteristics.Base.Components;
    using Characteristics.Health.Aspects;
    using Characteristics.Health.Components;
    using Game.Ecs.Core.Components;
    using Game.Ecs.Core.Death.Components;
    using UniGame.Proto.Ownership;
    using LeoEcs.Bootstrap;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    
    /// <summary>
    /// Detects when a character is ready to die based on its health value.
    /// </summary>
#if ENABLE_IL2CPP
	using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class DetectReadyToDeathByHealthSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private HealthAspect _healthAspect;
        private OwnershipAspect _ownershipAspect;
        
        private ProtoItExc _filter = It
            .Chain<CharacteristicChangedComponent<HealthComponent>>()
            .Inc<CharacteristicComponent<HealthComponent>>()
            .Inc<HealthComponent>()
            .Exc<KillRequest>()
            .Exc<PrepareToDeathComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var healthComponent = ref _healthAspect.HealthCharacteristic.Get(entity);
                
                if(healthComponent.Value > 0.0f) continue;

                ref var prepareToDeath = ref _ownershipAspect.PrepareToDeath.GetOrAddComponent(entity);
                prepareToDeath.Source = entity.PackEntity(_world);
            }
        }
    }
}