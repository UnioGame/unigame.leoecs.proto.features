namespace UniGame.Ecs.Proto.Characteristics.Base.Systems
{
    using System;
    using Aspects;
    using Components.Requests;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// changed base value of characteristics
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ChangeCharacteristicBaseValueSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private CharacteristicsAspect _characteristicsAspect;
        private ModificationsAspect _modificationsAspect;
        
        private ProtoIt _filter = It
            .Chain<ChangeCharacteristicBaseRequest>()
            .End();

        public void Run()
        {
            foreach (var requestEntity in _filter)
            {
                ref var requestComponent = ref _characteristicsAspect.ChangeBaseValue.Get(requestEntity);
                var value = requestComponent.Value;
                
                if(!requestComponent.Target.Unpack(_world,out var characteristicEntity))
                    continue;
                
                if(!_characteristicsAspect.Value.Has(characteristicEntity))
                    continue;
                
                ref var minComponent = ref _characteristicsAspect.MinValue.Get(characteristicEntity);
                ref var maxComponent = ref _characteristicsAspect.MaxValue.Get(characteristicEntity);
                ref var baseValueComponent = ref _characteristicsAspect.BaseValue.Get(characteristicEntity);
                
                var currentValue = baseValueComponent.Value;
                currentValue += value;
                currentValue = Mathf.Clamp(currentValue, minComponent.Value,maxComponent.Value);
                baseValueComponent.Value = currentValue;
                
                _characteristicsAspect.Recalculate.GetOrAddComponent(characteristicEntity);
            }
            
        }
    }
}