namespace UniGame.Ecs.Proto.Effects.Aspects
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;

    [Serializable]
    public class EffectViewAspect : EcsAspect
    {
        public ProtoPool<EffectViewComponent> View;
        public ProtoPool<EffectParentComponent> Parent;
    }
}