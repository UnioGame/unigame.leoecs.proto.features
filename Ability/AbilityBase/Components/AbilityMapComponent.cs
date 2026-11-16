namespace UniGame.Ecs.Proto.Ability.Common.Components
{
    using System;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Unity.Collections;

    /// <summary>
    /// Компонент со ссылками на доступные умения у сущности.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityMapComponent : IProtoAutoReset<AbilityMapComponent>
    {
        public NativeHashMap<int,ProtoPackedEntity> AbilitySlots;
        public NativeHashSet<ProtoPackedEntity> Abilities;

        public void SetHandlers(IProtoPool<AbilityMapComponent> pool) => pool.SetResetHandler(AutoReset);
        
        // Disposes the native collections of the reset component (c), not of this handler instance.
        public static void AutoReset(ref AbilityMapComponent c)
        {
            if (c.AbilitySlots.IsCreated)
                c.AbilitySlots.Dispose();

            if (c.Abilities.IsCreated)
                c.Abilities.Dispose();

            c.AbilitySlots = default;
            c.Abilities = default;
        }
    }
}