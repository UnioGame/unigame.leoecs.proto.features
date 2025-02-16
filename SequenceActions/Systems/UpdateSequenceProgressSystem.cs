namespace Game.Modules.SequenceActions.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Mathematics;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class UpdateSequenceProgressSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private SequenceActionAspect _sequenceAspect;
        
        private ProtoItExc _updateSequenceFilter = It
            .Chain<SequenceActionComponent>()
            .Inc<SequenceDataComponent>()
            .Inc<SequenceActionProgressComponent>()
            .Exc<SequenceCompleteComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _updateSequenceFilter)
            {
                ref var sequenceData = ref _sequenceAspect.SequenceData.Get(entity);
                ref var sequenceProgress = ref _sequenceAspect.SequenceProgress.Get(entity);
                ref var sequenceActionProgress = ref _sequenceAspect.ActionProgress.Get(entity);
                
                ref var activeAction = ref sequenceData.Actions[sequenceData.ActiveAction];
                
                var actionProgress = math.clamp(sequenceActionProgress.Progress, 0f, 1f);
                var weight = activeAction.progressWeight * actionProgress;
                var weightPassed = sequenceProgress.CompleteProgress + weight;
                var percent =  sequenceProgress.MaxProgress <= 0 ? 0 
                        : weightPassed / sequenceProgress.MaxProgress;

                sequenceProgress.ProgressWeight = weightPassed;
                sequenceProgress.Progress = percent;
            }
        }
    }
}