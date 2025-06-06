namespace UniGame.Ecs.Proto.Animations.Components
{
    using System;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;
    using UnityEngine.Playables;

    /// <summary>
    /// playable animation asset
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AnimationPlayableComponent: IProtoAutoReset<AnimationPlayableComponent>
    {
        public PlayableAsset Value;
        
        public void SetHandlers(IProtoPool<AnimationPlayableComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref AnimationPlayableComponent c)
        {
            c.Value = null;
        }
    }
}