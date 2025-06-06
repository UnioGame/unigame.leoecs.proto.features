namespace UniGame.Ecs.Proto.GameEffects.ModificationEffect.Components
{
    using System;
    using Characteristics;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SingleModificationEffectComponent : IProtoAutoReset<SingleModificationEffectComponent>
    {
        public ModificationHandler Value;
        
        public void SetHandlers(IProtoPool<SingleModificationEffectComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref SingleModificationEffectComponent c)
        {
            c.Value = null;
        }
    }
}