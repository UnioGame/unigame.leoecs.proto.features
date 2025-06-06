namespace UniGame.Ecs.Proto.Core.Death.Components
{
    using LeoEcs.Proto;
    using Leopotam.EcsProto;
    using UnityEngine.Playables;

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