namespace Game.Modules.SequenceActions.Systems
{
    using System;
    using Aspects;
    using Components;
    using Cysharp.Threading.Tasks;
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
    public class UpdateSequenceSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private SequenceAspect _sequenceAspect;
        
        private ProtoIt _updateSequenceFilter = It
            .Chain<SequenceActionComponent>()
            .Inc<SequenceActiveComponent>()
            .Inc<SequenceComponent>()
            .Inc<SequenceResultComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _updateSequenceFilter)
            {
                ref var sequenceComponent = ref _sequenceAspect.Sequence.Get(entity);    
                ref var resultComponent = ref _sequenceAspect.Result.Get(entity);
                ref var sequenceAction = ref _sequenceAspect.SequenceAction.GetOrAddComponent(entity);
                ref var status = ref resultComponent.Value;
                
                if(!status.IsFinished) continue;

                var task = sequenceAction.Task;
                var activeAction = sequenceComponent.Action;
    
                _sequenceAspect.Active.Del(entity);

                ref var sequenceComplete = ref _sequenceAspect.Complete.GetOrAddComponent(entity);
                ref var sequenceCompleteEvent = ref _sequenceAspect.CompleteEvent.GetOrAddComponent(entity);
            }
        }
    }
}