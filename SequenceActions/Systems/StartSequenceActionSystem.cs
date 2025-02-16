namespace Game.Modules.SequenceActions.Systems
{
    using System;
    using Aspects;
    using Components;
    using Components.Requests;
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
    public class StartSequenceActionSystem : IProtoRunSystem
    {
        private SequenceActionAspect _actionAspect;
        private ProtoWorld _world;

        private ProtoItExc _startSequenceFilter = It
            .Chain<StartSequenceActionSelfRequest>()
            .Exc<SequenceActionComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _startSequenceFilter)
            {
                ref var startSequenceRequest = ref _actionAspect.StartAction.Get(entity);
                ref var sequenceAction = ref _actionAspect.SequenceAction.GetOrAddComponent(entity);
                ref var progressComponent = ref _actionAspect.ActionProgress.GetOrAddComponent(entity);
                
                var action = startSequenceRequest.Action;
                var isDone = startSequenceRequest.Token.IsCancellationRequested || startSequenceRequest.Action == null;
                
                progressComponent.IsSuccess = false;
                progressComponent.ActionName = action.ActionName;
                progressComponent.IsFinished = isDone;
                progressComponent.Progress = isDone ? 1f : 0f;
                progressComponent.Message = string.Empty;
                
                if (isDone) continue;
                
                var packedEntity = entity.PackEntity(_world);
                var token = startSequenceRequest.Token;
                var task = action.ExecuteAsync(packedEntity, _world, token)
                    .AttachExternalCancellation(token)
                    .Preserve();
                
                sequenceAction.Action = action;
                sequenceAction.Task = task;
                sequenceAction.Token = startSequenceRequest.Token;
                
                task.Forget();
            }
        }
    }
}