namespace UniGame.Ecs.Proto.Animations.Systems
{
    using System;
    using Aspects;
    using Components;
    using Data;
    using Game.Ecs.Core.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class EvaluateAnimationSystem : IProtoRunSystem,IProtoInitSystem
    {
        private ProtoIt _filter;
        private ProtoWorld _world;
        private AnimationToolSystem _animationTool;
        
        private AnimationTimelineAspect _animationAspect;
        private PlayableAnimatorAspect _animatorAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _animationTool = _world.GetGlobal<AnimationToolSystem>();
            
            _filter = _world
                .Filter<AnimatorPlayingComponent>()
                .Inc<PlayableDirectorComponent>()
                .End();
        }
        
        public void Run()
        {
            foreach (var animationEntity in _filter)
            {
                ref var activeAnimationComponent = ref _animatorAspect.PlayingTarget.Get(animationEntity);
                ref var directorComponent = ref _animatorAspect.Director.Get(animationEntity);
                var playableDirector = directorComponent.Value;
                
                if(!activeAnimationComponent.Value.Unpack(_world,out var playingEntity))
                    continue;

                ref var animationDuration = ref _animationAspect.Duration.Get(playingEntity);
                
                var state = playableDirector.state;
                var playable = playableDirector.playableAsset;
                var currentTime = playableDirector.time;
                var duration = animationDuration.Value;
                
                var isPlaying = playable != null && duration > currentTime;
                if (isPlaying) continue;
                
                ref var completeRequest = ref _animationAspect.StopSelf.GetOrAddComponent(animationEntity);
            }
        }
    }
}