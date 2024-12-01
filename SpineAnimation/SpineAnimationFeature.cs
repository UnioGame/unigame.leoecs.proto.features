namespace Game.Ecs.SpineAnimation
{
    using Components.Events;
    using Components.Requests;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    
    [CreateAssetMenu(menuName = "ECS Proto/Features/Spine Animation Feature")]
    public class SpineAnimationFeature : BaseLeoEcsFeature
    {
        public sealed override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            // System for initializing skeleton animation and subscription to spine events.
            ecsSystems.Add(new InitSkeletonAnimationSystem());
            
            // Delete used SpineAnimationEventSelf
            ecsSystems.DelHere<SpineAnimationEventSelf>();
            // System that catches and applies request for spine animation.
            ecsSystems.Add(new CatchApplyRequestSystem());
            
            // Represents a system for playing Spine animations.
            ecsSystems.Add(new PlaySpineAnimationSystem());
            // Delete used PlaySpineAnimationSelfRequest
            ecsSystems.DelHere<PlaySpineAnimationSelfRequest>();
            
            return UniTask.CompletedTask;
        }
    }
}