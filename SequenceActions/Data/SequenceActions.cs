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
    using Sirenix.OdinInspector;
    using UnityEngine;

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

        public string ActionName => sequenceName;

        public async UniTask<SequenceActionResult> ExecuteAsync(
            ProtoPackedEntity sequenceEntity,
            ProtoWorld world,
            CancellationToken cancellationToken = default)
        {
            foreach (var actionItem in actions)
            {
                var action = actionItem.action;
                var actionResult = await action.ExecuteAsync(sequenceEntity,world,cancellationToken);
                if (!actionResult.IsSuccess)
                    return actionResult;
            }

            return new SequenceActionResult()
            {
                IsSuccess = true,
                IsFinished = true,
            };
        }
    }
}