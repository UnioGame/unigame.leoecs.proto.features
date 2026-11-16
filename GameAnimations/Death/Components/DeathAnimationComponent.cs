namespace UniGame.Ecs.Proto.Core.Death.Components
{
    using System;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;
    using UnityEngine.Playables;

    /// <summary>
    /// Какую анимацию смерти проиграть (Playable из таймлайна).
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct DeathAnimationComponent : IProtoAutoReset<DeathAnimationComponent>
    {
        public PlayableAsset Animation;

        public void SetHandlers(IProtoPool<DeathAnimationComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref DeathAnimationComponent c)
        {
            c.Animation = null;
        }
    }
}