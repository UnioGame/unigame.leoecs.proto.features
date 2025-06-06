namespace UniGame.Ecs.Proto.Effects.Components
{
    using System;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;
    using UnityEngine;

    /// <summary>
    /// target of effect root in the world
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct EffectRootTransformsComponent : IProtoAutoReset<EffectRootTransformsComponent>
    {
        public Transform[] Value;
        
        public void SetHandlers(IProtoPool<EffectRootTransformsComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref EffectRootTransformsComponent c)
        {
            c.Value ??= Array.Empty<Transform>();
            for (var i = 0; i < c.Value.Length; i++)
                c.Value[i] = default;
        }
    }
}