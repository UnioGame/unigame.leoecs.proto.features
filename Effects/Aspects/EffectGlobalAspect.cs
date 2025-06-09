namespace UniGame.Ecs.Proto.Effects.Data
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;

    [Serializable]
    public class EffectGlobalAspect : EcsAspect
    {
        public ProtoPool<EffectGlobalRootMarkerComponent> Global;
        public ProtoPool<EffectRootTransformsComponent> Transforms;
        public ProtoPool<EffectsRootDataComponent> Configuration;
    }
}