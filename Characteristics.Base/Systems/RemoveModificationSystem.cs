namespace UniGame.Ecs.Proto.Characteristics.Base.Systems
{
    using System;
    using Aspects;
    using Components;
    using Components.Requests;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// remove modification from characteristics and make recalculate request
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class RemoveModificationSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private ProtoIt _removeRequestFilter = It
            .Chain<RemoveModificationRequest>()
            .End();
        
        private ProtoIt _modificationsFilter = It
            .Chain<ModificationComponent>()
            .Inc<CharacteristicLinkComponent>()
            .Inc<ModificationSourceLinkComponent>()
            .End();
        
        private CharacteristicsAspect _characteristicsAspect;
        private ModificationsAspect _modificationsAspect;
        
        private ProtoPool<RemoveModificationRequest> _removeRequestPool;
        private ProtoPool<RecalculateCharacteristicSelfRequest> _recalculateCharacteristicPool;

        public void Run()
        {
            foreach (var removeRequestEntity in _removeRequestFilter)
            {
                ref var requestComponent = ref _removeRequestPool.Get(removeRequestEntity);
                
                if(!requestComponent.Source.Unpack(_world,out var modificationSourceEntity))
                    continue;
                
                if(!requestComponent.Characteristic.Unpack(_world,out var targetCharacteristic))
                    continue;
                
                foreach (var modificationEntity in _modificationsFilter)
                {
                    ref var modificationSourceLinkComponent = ref _modificationsAspect
                        .SourceLink.Get(modificationEntity);
                    
                    if (!modificationSourceLinkComponent.Value.Unpack(_world,out var sourceEntity))
                        continue;
                    
                    ref var characteristicsLinkComponent = ref _characteristicsAspect
                        .CharacteristicLink
                        .Get(modificationEntity);
                    
                    if (!characteristicsLinkComponent.Link.Unpack(_world, out var characteristicEntity))
                        continue;
                    
                    if(!sourceEntity.Equals(modificationSourceEntity)) continue;
                    if(!targetCharacteristic.Equals(characteristicEntity)) continue;
                    
                    ref var modificationComponent = ref _modificationsAspect.Modification.Get(modificationEntity);
                    modificationComponent.Counter -= 1;
                    
                    if (modificationComponent.Counter <= 0)
                        _world.DelEntity(modificationEntity);

                    _recalculateCharacteristicPool.GetOrAddComponent(characteristicEntity);
                }
            }
        }
    }
}