namespace Game.Modules.SequenceActions
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Abstract;
    using Cysharp.Threading.Tasks;
    using Data;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Unity.Mathematics;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    public class SequenceActions : ISequenceAction
    {
        #region inspector
        
        public string sequenceName = nameof(SequenceActions);
        
#if ODIN_INSPECTOR
        [ListDrawerSettings(ListElementLabelName = nameof(SequenceActionData.ActionName))]
#endif
        public List<SequenceActionData> actions = new();

        
        #endregion
        
        private SequenceActionResult _status = SequenceActionResult.None;
        
        public SequenceActionResult Status { 
            get => _status;
            private set => _status = value;
        }

        public string ActionName => sequenceName;

        public async UniTask ExecuteAsync(
            ProtoPackedEntity sequenceEntity,
            ProtoWorld world,
            CancellationToken cancellationToken = default)
        {
            if (actions.Count == 0)
            {
                _status = SequenceActionResult.Success;
                return;
            }
            
            var maxProgress = 0f;
            var completedProgress = 0f;
            
            foreach (var actionItem in actions)
                maxProgress += actionItem.progressWeight;
            
            maxProgress = maxProgress <= 0 ? 1 : maxProgress;
            
            foreach (var actionItem in actions)
            {
                var action = actionItem.action;
                var actionTask = action.ExecuteAsync(sequenceEntity,world,cancellationToken)
                    .AttachExternalCancellation(cancellationToken);
                
                actionTask.Forget();

                while (actionTask.Status == UniTaskStatus.Pending)
                {
                    var actionStatus = action.Status;
                    
                    var actionProgress = math.clamp(actionStatus.Progress, 0f, 1f);
                    var weight = actionItem.progressWeight * actionProgress;
                    var weightPassed = completedProgress + weight;
                    var progress = weightPassed / maxProgress;
                    var percent =  math.clamp(progress, 0f, 1f);

                    _status.Error = actionStatus.Error;
                    _status.Message = actionStatus.Message;
                    _status.Progress = percent;
                }
                
                var taskStatus = action.Status;
                
                if (actionTask.Status != UniTaskStatus.Succeeded)
                {
                    _status.IsSuccess = false;
                    _status.IsFinished = true;
                    _status.Error = taskStatus.Error;
                    _status.Message = taskStatus.Message;
                    return;
                }
                
                completedProgress += actionItem.progressWeight;
            }
            
            _status.IsSuccess = true;
            _status.IsFinished = true;
            _status.Progress = 1f;
        }
    }
}