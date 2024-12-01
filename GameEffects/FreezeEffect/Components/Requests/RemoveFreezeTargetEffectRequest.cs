namespace UniGame.Ecs.Proto.GameEffects.FreezeEffect.Components.Requests
{
    using System;
    using Leopotam.EcsProto.QoL;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct RemoveFreezeTargetEffectRequest
    {
        public ProtoPackedEntity Target;
    }
}