namespace UniGame.Ecs.Proto.Gameplay.Death.Aspects
{
    using System;
    using Core.Death.Components;
    using Game.Ecs.Characteristics.Health.Components;
    using Game.Ecs.Core.Components;
    using Game.Ecs.Core.Death.Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class DeathAspect : EcsAspect
    {
        public ProtoPool<PlayableDirectorComponent> Director;
        public ProtoPool<DeadAnimationEvaluateComponent> Evaluate;
        public ProtoPool<DeathAnimationComponent> Animation;
        public ProtoPool<AwaitDeathCompleteComponent> AwaitDeath;
        public ProtoPool<DisabledComponent> Disabled;
        public ProtoPool<DeathCompletedComponent> Completed;
        public ProtoPool<ChampionComponent> Champion;
    }
}