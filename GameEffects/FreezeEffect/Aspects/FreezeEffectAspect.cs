namespace UniGame.Ecs.Proto.GameEffects.FreezeEffect.Aspects
{
    using System;
    using Components;
    using Components.Requests;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class FreezeEffectAspect : EcsAspect
    {
        public ProtoPool<FreezeEffectComponent> FreezeEffect;
        public ProtoPool<FreezeTargetEffectComponent> FreezeTargetEffect;
        
        public ProtoPool<ApplyFreezeTargetEffectRequest> ApplyFreezeTarget;
        public ProtoPool<RemoveFreezeTargetEffectRequest> RemoveFreezeTarget;
    }
}