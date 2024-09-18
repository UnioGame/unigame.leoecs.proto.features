namespace UniGame.Ecs.Proto.Characteristics.Base.Systems
{
    using System;
    using Aspects;
    using Components;
    using Components.Requests;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using LeoEcs.Shared.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    /// <summary>
    /// recalculate characteristic by modifications
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class RecalculateMaxLimitValueSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private CharacteristicsAspect _characteristicsAspect;
        private ModificationsAspect _modificationsAspect;
        
        private ProtoIt _recalculateRequestFilter = It
            .Chain<RecalculateCharacteristicSelfRequest>()
            .Inc<CharacteristicValueComponent>()
            .Inc<CharacteristicBaseValueComponent>()
            .Inc<MaxLimitModificationsValueComponent>()
            .Inc<MaxValueComponent>()
            .End();
        
        private ProtoIt _maxLimitFilter= It
            .Chain<ModificationComponent>()
            .Inc<ModificationMaxLimitComponent>()
            .Inc<CharacteristicLinkComponent>()
            .End();

        public void Run()
        {
            foreach (var characteristicEntity in _recalculateRequestFilter)
            {
                ref var maxLimitValueComponent = ref _modificationsAspect
                    .MaxLimitModificationValue
                    .Get(characteristicEntity);

                var maxValue = 0f;

                foreach (var modificationEntity in _maxLimitFilter)
                {
                    ref var linkComponent = ref _characteristicsAspect.CharacteristicLink.Get(modificationEntity);
                    if(!linkComponent.Link.Unpack(_world,out var characteristicValue))
                        continue;
                    if(!characteristicEntity.Equals(characteristicValue)) continue;
                    
                    ref var modificationComponent = ref _modificationsAspect.Modification.Get(modificationEntity);
                    maxValue += modificationComponent.Counter * modificationComponent.BaseValue;
                }
                
                maxLimitValueComponent.Value = maxValue;
            }
        }
    }
}