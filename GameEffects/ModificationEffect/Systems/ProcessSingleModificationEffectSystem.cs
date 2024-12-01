namespace UniGame.Ecs.Proto.GameEffects.ModificationEffect.Systems
{
    using System;
    using Aspects;
    using Components;
    using Effects.Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessSingleModificationEffectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private ModificationEffectAspect _modificationEffectAspect;

        private ProtoItExc _filter = It
            .Chain<EffectComponent>()
            .Inc<ApplyEffectSelfRequest>()
            .Inc<SingleModificationEffectComponent>()
            .Exc<ModificationEffectComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var modification = ref _modificationEffectAspect.SingleModificationEffect.Get(entity);
                var modificationHandler = modification.Value;

                ref var modificationComponent = ref _world
                    .AddComponent<ModificationEffectComponent>(entity);

                modificationComponent.ModificationHandlers.Add(modificationHandler);
            }
        }
    }
}