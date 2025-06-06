namespace UniGame.Ecs.Proto.GameEffects.ModificationEffect.Components
{
    using System;
    using System.Collections.Generic;
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
    public struct ModificationEffectComponent : IProtoAutoReset<ModificationEffectComponent>
    {
        public List<ModificationHandler> ModificationHandlers;

        public void SetHandlers(IProtoPool<ModificationEffectComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref ModificationEffectComponent c)
        {
            c.ModificationHandlers ??= new List<ModificationHandler>();
            c.ModificationHandlers.Clear();
        }
    }
}