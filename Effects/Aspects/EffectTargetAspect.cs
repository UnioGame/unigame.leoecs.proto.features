namespace UniGame.Ecs.Proto.Effects.Data
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;

    [Serializable]
    public class EffectTargetAspect : EcsAspect
    {
        public ProtoPool<EffectRootTargetComponent> Target;
        public ProtoPool<EffectParentComponent> Transform;
        public ProtoPool<EffectRootIdComponent> Id;
    }
}