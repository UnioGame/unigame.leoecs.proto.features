namespace Game.Ecs.SpineAnimation.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.Proto.Ownership;
    using R3;
    using Spine;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// System for initializing skeleton animation and subscription to spine events.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class InitSkeletonAnimationSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private SpineAnimationAspect _spineAnimationAspect;
        private OwnershipAspect _lifeTimeAspect;

        private ProtoItExc _filter = It
            .Chain<SkeletonAnimationComponent>()
            .Exc<SkeletonAnimationInitializedComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var lifeTimeComponent = ref _lifeTimeAspect.LifeTime.GetOrAddComponent(entity);
                ref var skeletonAnimationComponent = ref _spineAnimationAspect.SkeletonAnimation.Get(entity);
                
                var lifeTime = lifeTimeComponent.GetLifeTime();
                var skeletonAnimation = skeletonAnimationComponent.SkeletonAnimation;

                var spineEventObservable = Observable.FromEvent<AnimationState.TrackEntryEventDelegate, (TrackEntry, Event)>(
                    handler => (trackEntry, e) => handler((trackEntry, e)),
                    h => skeletonAnimation.AnimationState.Event += h,
                    h => skeletonAnimation.AnimationState.Event -= h
                );

                var subscription = spineEventObservable
                    .Subscribe(eventData => HandleSpineEvent(entity.PackEntity(_world), eventData.Item1, eventData.Item2))
                    .AddTo(lifeTime);

                lifeTime.AddDispose(subscription);

                _spineAnimationAspect.Initialized.Add(entity);
            }
        }
        
        private void HandleSpineEvent(ProtoPackedEntity packedEntity, TrackEntry trackEntry, Event e)
        {
            if (!packedEntity.Unpack(_world, out var entity)) return;
            
            ref var spineAnimationEvent = ref _spineAnimationAspect.Apply.GetOrAddComponent(entity);
             spineAnimationEvent.Event = e;
        }
    }
}