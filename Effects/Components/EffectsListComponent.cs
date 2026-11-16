namespace UniGame.Ecs.Proto.Effects.Components
{
    using System;
    using System.Collections.Generic;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// Все эффекты, которые сейчас висят на сущности. Мёртвые эффекты из списка убираются автоматически.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct EffectsListComponent : IProtoAutoReset<EffectsListComponent>
    {
        public List<ProtoPackedEntity> Effects;
        
        public void SetHandlers(IProtoPool<EffectsListComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref EffectsListComponent c)
        {
            c.Effects ??= new List<ProtoPackedEntity>();
            c.Effects.Clear();
        }
    }
}