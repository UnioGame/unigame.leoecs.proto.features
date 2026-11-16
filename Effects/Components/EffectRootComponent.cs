namespace UniGame.Ecs.Proto.Effects.Components
{
    using System;
    using Data;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;

    /// <summary>
    /// Точки на модели (голова, руки, оружие…), куда можно прицеплять визуалы эффектов.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct EffectRootComponent : IProtoAutoReset<EffectRootComponent>
    {
        public EffectRootValue[] Value;
        
        public void SetHandlers(IProtoPool<EffectRootComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref EffectRootComponent c)
        {
            c.Value = Array.Empty<EffectRootValue>();
        }
    }
}