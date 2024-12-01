namespace UniGame.Ecs.Proto.GameEffects.ModificationEffect.Systems
{
    using System;
    using Aspects;
    using Components;
    using Effects.Aspects;
    using Effects.Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessDestroyedModificationEffectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private EffectAspect _effectAspect;
        private ModificationEffectAspect _modificationEffectAspect;

        private ProtoIt _filter = It
            .Chain<EffectComponent>()
            .Inc<DestroyEffectSelfRequest>()
            .Inc<ModificationEffectComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var effect = ref _effectAspect.Effect.Get(entity);
                ref var modification = ref _modificationEffectAspect.ModificationEffect.Get(entity);

                if (!effect.Destination.Unpack(_world, out var destinationEntity))
                    continue;

                foreach (var modificationHandler in modification.ModificationHandlers)
                {
                    modificationHandler.RemoveModification(_world, entity, destinationEntity);
                }
            }
        }
    }
}