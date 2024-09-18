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
    public class ChangeCharacteristicMinLimitationSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private CharacteristicsAspect _characteristicsAspect;
        private ModificationsAspect _modificationsAspect;
        
        private ProtoIt _changeRequestFilter = It
            .Chain<ChangeMinLimitRequest>()
            .End();
        
        public void Run()
        {
            foreach (var requestEntity in _changeRequestFilter)
            {
                ref var requestComponent = ref _characteristicsAspect.ChangeMinLimit.Get(requestEntity);
                var value = requestComponent.Value;
                
                if(!requestComponent.Target.Unpack(_world,out var characteristicEntity))
                    continue;
                
                if(!_characteristicsAspect.BaseValue.Has(characteristicEntity))
                    continue;
                
                ref var minComponent = ref _characteristicsAspect.MinValue.Get(characteristicEntity);
                ref var maxComponent = ref _characteristicsAspect.MaxValue.Get(characteristicEntity);
                ref var baseValueComponent = ref _characteristicsAspect.BaseValue.Get(characteristicEntity);

                minComponent.Value = value >= maxComponent.Value ? minComponent.Value : value;
                baseValueComponent.Value = Mathf.Clamp(baseValueComponent.Value, minComponent.Value, maxComponent.Value);
                
                _characteristicsAspect.Recalculate.GetOrAddComponent(characteristicEntity);
            }
            
        }
    }
}