namespace UniGame.Ecs.Proto.Characteristics.Base.Systems
{
    using System;
    using Aspects;
    using Components;
    using Components.Requests;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using LeoEcs.Shared.Extensions;
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
    public class RecalculatePercentValueSystem : IProtoRunSystem
    {
        private const float HundredPercent = 100.0f;
        
        private ProtoWorld _world;
        
        private CharacteristicsAspect _characteristicsAspect;
        private ModificationsAspect _modificationsAspect;
        
        private ProtoIt _recalculateRequestFilter = It
            .Chain<RecalculateCharacteristicSelfRequest>()
            .Inc<CharacteristicValueComponent>()
            .Inc<CharacteristicBaseValueComponent>()
            .Inc<PercentModificationsValueComponent>()
            .End();
        
        private ProtoIt _percentModificationsFilter = It
            .Chain<ModificationComponent>()
            .Inc<ModificationPercentComponent>()
            .Inc<CharacteristicLinkComponent>()
            .End();

        public void Run()
        {
            foreach (var characteristicEntity in _recalculateRequestFilter)
            {
                ref var percentValueComponent = ref _modificationsAspect
                    .PercentModificationValue
                    .Get(characteristicEntity);

                var percentModification = HundredPercent;
                
                foreach (var modificationEntity in _percentModificationsFilter)
                {
                    ref var linkComponent = ref _characteristicsAspect.CharacteristicLink.Get(modificationEntity);
                    if(!linkComponent.Link.Unpack(_world,out var characteristicValue))
                        continue;
                    if(!characteristicEntity.Equals(characteristicValue)) continue;
                    
                    ref var modificationComponent = ref _modificationsAspect.Modification.Get(modificationEntity);
                    percentModification += modificationComponent.Counter * modificationComponent.BaseValue;
                }
                
                percentValueComponent.Value = percentModification;
            }
        }
    }
}