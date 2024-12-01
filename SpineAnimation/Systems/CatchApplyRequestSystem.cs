namespace Game.Ecs.SpineAnimation.Systems
{
    using System;
    using Aspects;
    using Components.Events;
    using Components.Requests;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// System that catches and applies request for spine animation.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CatchApplyRequestSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private SpineAnimationAspect _spineAnimationAspect;

        private ProtoItExc _filter = It
            .Chain<ApplySpineAnimationDataSelfRequest>()
            .Exc<SpineAnimationEventSelf>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var applySpineAnimationDataSelfRequest = ref _spineAnimationAspect.Apply.Get(entity);
                ref var spineAnimationEventSelf = ref _spineAnimationAspect.Event.Add(entity);

                spineAnimationEventSelf.Event = applySpineAnimationDataSelfRequest.Event;

                _spineAnimationAspect.Apply.Del(entity);
            }
        }
    }
}