namespace UniGame.Ecs.Proto.Effects.Systems
{
    using System;
    using Aspects;
    using Components;
    using UniGame.Proto.Ownership;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniCore.Runtime.ProfilerTools;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class CreateEffectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private EffectAspect _effectAspect;
        private OwnershipAspect _ownershipAspect;
        
        private ProtoIt _filter = It
            .Chain<CreateEffectSelfRequest>()
            .End();
        
        public void Run()
        {
            foreach (var effectEntity in _filter)
            {
                ref var request = ref _effectAspect.Create.Get(effectEntity);
                ref var effectComponent = ref _effectAspect.Effect.GetOrAdd(effectEntity);
                
                request.Destination.Unpack(_world,out var destinationEntity);
                
                if (request.Source.Unpack(_world, out var sourceEntity) && 
                    _effectAspect.Power.Has(sourceEntity))
                {
                    _effectAspect.Power.Copy(sourceEntity, effectEntity);
                }

                effectComponent.Destination = request.Destination;
                effectComponent.Source = request.Source;
                
                request.Effect.ComposeEntity(_world, effectEntity);

                if (!request.Destination.Unpack(_world, out var unpackedDestination))
                {
                    GameLog.LogError("CreateEffectSystem: Cannot unpack destination entity");
                    continue;
                }
                
                unpackedDestination.AddChild(effectEntity, _world);

                ref var list = ref _effectAspect.List.GetOrAddComponent(destinationEntity);
                list.Effects.Add(_world.PackEntity(effectEntity));
            }
        }
    }
}