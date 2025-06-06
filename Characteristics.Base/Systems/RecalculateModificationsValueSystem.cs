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
    public class RecalculateModificationsValueSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private CharacteristicsAspect _characteristicsAspect;
        private ModificationsAspect _modificationsAspect;
        
        private ProtoIt _recalculateRequestFilter = It
            .Chain<RecalculateCharacteristicSelfRequest>()
            .Inc<CharacteristicValueComponent>()
            .Inc<CharacteristicBaseValueComponent>()
            .End();
        
        private ProtoItExc _modificationsFilter= It
            .Chain<ModificationComponent>()
            .Inc<CharacteristicLinkComponent>()
            .Exc<ModificationPercentComponent>()
            .Exc<ModificationMaxLimitComponent>()
            .End();

        public void Run()
        {
            foreach (var characteristicEntity in _recalculateRequestFilter)
            {
                ref var baseValueComponent = ref _characteristicsAspect.BaseValue.Get(characteristicEntity);
                ref var valueModificationsComponent = ref _modificationsAspect.BaseModificationValue.Get(characteristicEntity);

                var newValue = baseValueComponent.Value;
                
                foreach (var modificationEntity in _modificationsFilter)
                {
                    ref var linkComponent = ref _characteristicsAspect.CharacteristicLink.Get(modificationEntity);
                    if(!linkComponent.Link.Unpack(_world,out var characteristicValue))
                        continue;
                    if(!characteristicEntity.Equals(characteristicValue)) continue;

                    ref var modificationComponent = ref _modificationsAspect.Modification.Get(modificationEntity);
                    newValue += modificationComponent.Counter * modificationComponent.BaseValue;
                }

                valueModificationsComponent.Value = newValue;
            }
        }
    }
}