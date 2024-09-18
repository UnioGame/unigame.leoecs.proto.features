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
    /// check modifications is tracked modification source is dead, then remove modification
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CheckModificationSourceLifeTimeComponent : IProtoRunSystem
    {
        private ProtoWorld _world;
        private CharacteristicsAspect _characteristicsAspect;
        private ModificationsAspect _modificationsAspect;
        
        private ProtoIt _filter= It
            .Chain<ModificationComponent>()
            .Inc<ModificationSourceTrackComponent>()
            .Inc<CharacteristicLinkComponent>()
            .Inc<ModificationSourceLinkComponent>()
            .End();
        
        private ProtoPool<CharacteristicLinkComponent> _characteristicsLinkPool;
        private ProtoPool<RecalculateCharacteristicSelfRequest> _recalculateSelfPool;

        public void Run()
        {
            foreach (var modification in _filter)
            {
                ref var ownerLinkComponent = ref _modificationsAspect.SourceLink.Get(modification);
                if (ownerLinkComponent.Value.Unpack(_world,out var sourceEntity)) continue;

                ref var characteristicsLinkComponent = ref _modificationsAspect.CharacteristicLink.Get(modification);

                if (characteristicsLinkComponent.Link.Unpack(_world, out var characteristicEntity))
                {
                    ref var recalculateRequest = ref _characteristicsAspect.Recalculate
                        .GetOrAddComponent(characteristicEntity);
                }
                
                _world.DelEntity(modification);
            }
        }
    }
}