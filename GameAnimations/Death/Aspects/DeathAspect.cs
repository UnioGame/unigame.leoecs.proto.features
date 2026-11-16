namespace UniGame.Ecs.Proto.Gameplay.Death.Aspects
{
    using System;
    using Core.Death.Components;
    using Game.Ecs.Characteristics.Health.Components;
    using Game.Ecs.Core.Components;
    using Game.Ecs.Core.Death.Components;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;

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