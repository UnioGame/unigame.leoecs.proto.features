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
    public class SequenceAction : ISequenceAction
    {
        #region inspector
        
        public string sequenceName = nameof(SequenceAction);
        
#if ODIN_INSPECTOR
        [ListDrawerSettings(ListElementLabelName = nameof(SequenceActionData.ActionName))]
#endif
        [SerializeReference]
        public List<SequenceActionData> actions = new();

        
        #endregion

        public string ActionName => sequenceName;

        public async UniTask<SequenceActionResultComponent> Execute(ProtoPackedEntity sequenceEntity,
            ProtoWorld world,CancellationToken cancellationToken = default)
        {
            var totalProgress = 0f;
            foreach (var action in actions)
            {
                totalProgress += action.progress;
            }
            
            foreach (var action in actions)
            {
                if(cancellationToken.IsCancellationRequested)
                {
                    return new SequenceActionResultComponent()
                    {
                        Error = string.Empty,
                        IsDone = false,
                        IsError = false,
                    };
                }
                
                //await action.Execute(world,cancellationToken);
            }

            return default;
        }
    }
}