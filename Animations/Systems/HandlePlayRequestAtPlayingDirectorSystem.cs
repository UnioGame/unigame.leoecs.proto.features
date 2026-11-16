namespace UniGame.Ecs.Proto.Animations.Systems
{
    using System;
    using Aspects;
    using Components;
    using Components.Requests;
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
    public sealed class HandlePlayRequestAtPlayingDirectorSystem : IProtoRunSystem,IProtoInitSystem
    {
        private ProtoIt _filter = It
            .Chain<PlayOnDirectorSelfRequest>()
            .Inc<AnimatorPlayingComponent>()
            .Inc<PlayableDirectorComponent>()
            .End();
        private ProtoWorld _world;
        
        private AnimationTimelineAspect _animationAspect;
        private PlayableAnimatorAspect _animatorAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            
        }
        
        public void Run()
        {
            foreach (var animatorEntity in _filter)
            {
                _animatorAspect.StopSelf.GetOrAddComponent(animatorEntity);
            }
        }
    }
}