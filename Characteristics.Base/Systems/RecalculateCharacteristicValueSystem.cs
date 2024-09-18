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
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

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
    public class RecalculateCharacteristicValueSystem : IProtoRunSystem
    {
        private const float HundredPercent = 100.0f;
        
        private ProtoWorld _world;
        private CharacteristicsAspect _characteristicsAspect;
        private ModificationsAspect _modificationsAspect;
        
        private ProtoIt _recalculateRequestFilter = It
            .Chain<RecalculateCharacteristicSelfRequest>()
            .Inc<CharacteristicValueComponent>()
            .Inc<CharacteristicBaseValueComponent>()
            .Inc<MaxLimitModificationsValueComponent>()
            .Inc<BaseModificationsValueComponent>()
            .Inc<PercentModificationsValueComponent>()
            .Inc<MaxValueComponent>()
            .End();

        public void Run()
        {
            foreach (var characteristicEntity in _recalculateRequestFilter)
            {
                ref var characteristicComponent = ref _characteristicsAspect.Value.Get(characteristicEntity);
                ref var defaultValueComponent = ref _characteristicsAspect.DefaultValue.Get(characteristicEntity);
                ref var minComponent = ref _characteristicsAspect.MinValue.Get(characteristicEntity);
                ref var maxComponent = ref _characteristicsAspect.MaxValue.Get(characteristicEntity);
                ref var previousValueComponent = ref _characteristicsAspect.PreviousValue.Get(characteristicEntity);
                ref var percentValueComponent = ref _characteristicsAspect.PercentValue.Get(characteristicEntity);
                ref var maxLimitValueComponent = ref _characteristicsAspect.MaxLimitValue.Get(characteristicEntity);
                ref var baseModificationsValueComponent = ref _modificationsAspect.BaseModificationValue.Get(characteristicEntity);

                var previousValue = characteristicComponent.Value;
                var previousMaxLimit = maxComponent.Value;

                var maxValue = defaultValueComponent.MaxValue + maxLimitValueComponent.Value;
                maxComponent.Value = maxValue;
                
                var newValue = baseModificationsValueComponent.Value;
                newValue *= percentValueComponent.Value / HundredPercent;
                newValue = Mathf.Clamp(newValue, minComponent.Value,maxComponent.Value);

                if(Mathf.Approximately(previousMaxLimit , maxValue) && 
                   Mathf.Approximately(previousValue , newValue)) continue;

                previousValueComponent.Value = previousValue;
                characteristicComponent.Value = newValue;
                
                ref var changedComponent = ref _characteristicsAspect.Changed
                    .GetOrAddComponent(characteristicEntity);
                changedComponent.PreviousValue = previousValue;
                changedComponent.Value = newValue;
            }
        }
    }
}