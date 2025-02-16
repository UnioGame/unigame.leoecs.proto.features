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

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
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
        private SequenceActionAspect _sequenceAspect;
        
        private ProtoItExc _updateSequenceFilter = It
            .Chain<SequenceActionComponent>()
            .Inc<SequenceProgressComponent>()
            .Inc<SequenceDataComponent>()
            .Inc<SequenceActionProgressComponent>()
            .Exc<SequenceCompleteComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _updateSequenceFilter)
            {
                ref var sequenceData = ref _sequenceAspect.SequenceData.Get(entity);    
                ref var progress = ref _sequenceAspect.SequenceProgress.Get(entity);
                ref var sequenceAction = ref _sequenceAspect.SequenceAction.GetOrAddComponent(entity);
                ref var sequenceActionProgress = ref _sequenceAspect.ActionProgress.GetOrAddComponent(entity);
                
                if(!sequenceActionProgress.IsFinished) continue;

                var task = sequenceAction.Task;
                var activeAction = sequenceData.ActiveAction;
                var nextAction = activeAction + 1;
                var isLastAction = nextAction >= sequenceData.Length;
                
                ref var actionItem = ref sequenceData.Actions[activeAction];
                ref var actionResult = ref sequenceData.Results[activeAction];

                actionResult.Error = sequenceActionProgress.Error;
                actionResult.IsSuccess = sequenceActionProgress.IsSuccess;
                actionResult.Message = sequenceActionProgress.Message;
                actionResult.Progress = sequenceActionProgress.Progress;
                actionResult.IsFinished = sequenceActionProgress.IsFinished;
                
                sequenceData.Results[activeAction] = actionResult;
                sequenceData.Result = actionResult;
                
                progress.CompleteProgress += actionItem.progressWeight;
                
                _sequenceAspect.ActionProgress.Del(entity);
                _sequenceAspect.SequenceAction.Del(entity);

                sequenceActionProgress.IsFinished = isLastAction;

                var isError = !sequenceActionProgress.IsSuccess;
                var isDone = isLastAction && actionResult.IsSuccess;

                if (isLastAction || isError)
                {
                    ref var sequenceComplete = ref _sequenceAspect.Complete.GetOrAddComponent(entity);
                    ref var sequenceCompleteEvent = ref _sequenceAspect.CompleteEvent.GetOrAddComponent(entity);
                    
                    progress.Progress = isDone ? 1f : progress.Progress;
                    progress.IsComplete = true;
                    progress.IsError = isError;
                    
                    continue;
                }
                
                sequenceData.ActiveAction = nextAction;
                ref var startActionRequest = ref _sequenceAspect.StartAction.GetOrAddComponent(entity);
                startActionRequest.Action = sequenceData.Actions[nextAction].action;
                startActionRequest.Token = sequenceAction.Token;
            }
        }
    }
}