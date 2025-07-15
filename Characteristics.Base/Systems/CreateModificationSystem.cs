namespace UniGame.Ecs.Proto.Characteristics.Base.Systems
{
    using System;
    using Aspects;
    using UniGame.Proto.Ownership;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// create new modification for target characteristic
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CreateModificationSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private CharacteristicsAspect _characteristicsAspect;
        private ModificationsAspect _modificationsAspect;
        private OwnershipAspect _ownershipAspect;
        
        private ProtoIt _filter = It
            .Chain<CreateModificationRequest>()
            .End();

        public void Run()
        {
            foreach (var requestEntity in _filter)
            {
                ref var requestComponent = ref _modificationsAspect.CreateModification.Get(requestEntity);
                if (!requestComponent.Target.Unpack(_world, out var characteristicEntity))
                    continue;
                
                //create entity
                var modificationEntity = _world.NewEntity();
                
                
                characteristicEntity.AddChild(modificationEntity, _world);

                ref var modificationValueComponent = ref _modificationsAspect.Modification.Add(modificationEntity);
                ref var linkCharacteristicComponent = ref _characteristicsAspect.CharacteristicLink.Add(modificationEntity);
                ref var sourceLinkComponent = ref _modificationsAspect.SourceLink.Add(modificationEntity);
                
                //mark modification as binded to source lifetime
                _modificationsAspect.ModificationSourceTrack.Add(modificationEntity);
                
                var modificationValue = requestComponent.Modification;
                var counter = modificationValue.counter == 0 ? 1 : modificationValue.counter;
                
                modificationValueComponent.IsPercent = modificationValue.isPercent;
                modificationValueComponent.AllowedSummation = modificationValue.allowedSummation;
                modificationValueComponent.BaseValue = modificationValue.baseValue;
                modificationValueComponent.Counter = counter;

                if (modificationValue.isPercent) _modificationsAspect.ModificationPercent.Add(modificationEntity);
                if (modificationValue.isMaxLimitModification) _modificationsAspect.ModificationMaxLimit.Add(modificationEntity);
                
                linkCharacteristicComponent.Link = requestComponent.Target;
                sourceLinkComponent.Value = requestComponent.Source;
                
                _characteristicsAspect.Recalculate.GetOrAddComponent(characteristicEntity);
            }
        }
    }
}