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
    public class UpdateSequenceActionSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private SequenceAspect _sequenceAspect;
        
        private ProtoItExc _updateSequenceFilter = It
            .Chain<SequenceActionComponent>()
            .Inc<SequenceActionProgressComponent>()
            .Exc<SequenceCompleteComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _updateSequenceFilter)
            {
                ref var sequenceAction = ref _sequenceAspect.SequenceAction.Get(entity);
                ref var progress = ref _sequenceAspect.ActionProgress.Get(entity);
                
                var status = sequenceAction.Task.Status;
                if (status != UniTaskStatus.Pending)
                {
                    progress.IsSuccess = status == UniTaskStatus.Succeeded;
                    progress.IsFinished = true;
                    progress.Progress = 1f;
                }
            }
        }
    }
}