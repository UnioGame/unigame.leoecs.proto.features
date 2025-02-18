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
    public class UpdateSequenceActionSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private SequenceAspect _sequenceAspect;
        
        private ProtoIt _updateSequenceFilter = It
            .Chain<SequenceActionComponent>()
            .Inc<SequenceResultComponent>()
            .Inc<SequenceActiveComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _updateSequenceFilter)
            {
                ref var sequenceAction = ref _sequenceAspect.SequenceAction.Get(entity);
                ref var resultComponent = ref _sequenceAspect.Result.Get(entity);

                var actionStatus = sequenceAction.Action.Status;
                var status = sequenceAction.Task.Status;

                resultComponent.Value = actionStatus;

                if (status == UniTaskStatus.Pending) continue;
                
                ref var resultStatus = ref resultComponent.Value;
                resultStatus.IsSuccess = status == UniTaskStatus.Succeeded;
                resultStatus.IsFinished = true;
                resultStatus.Progress = 1f;
            }
        }
    }
}