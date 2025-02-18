namespace Game.Modules.SequenceActions.Systems
{
    using System;
    using Aspects;
    using Components;
    using Components.Requests;
    using Cysharp.Threading.Tasks;
    using Data;
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
    public class StartSequenceSystem : IProtoRunSystem
    {
        private SequenceAspect _actionAspect;
        private ProtoWorld _world;

        private ProtoItExc _startSequenceFilter = It
            .Chain<StartSequenceRequest>()
            .Exc<SequenceActionComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _startSequenceFilter)
            {
                ref var startSequenceRequest = ref _actionAspect.Start.Get(entity);
                if(!startSequenceRequest.Target.Unpack(_world, out var target))
                    continue;

                var action = startSequenceRequest.Action;
                if(action == null) continue;

                _actionAspect.Complete.TryRemove(entity);
                
                ref var sequenceComponent = ref _actionAspect.Sequence.GetOrAddComponent(entity);
                ref var lifeTimeComponent = ref _actionAspect.LifeTime.GetOrAddComponent(entity);
                ref var resultComponent = ref _actionAspect.Result.GetOrAddComponent(entity);
                ref var sequenceAction = ref _actionAspect.SequenceAction.GetOrAddComponent(entity);
                if(startSequenceRequest.AutoDestroy)
                    _actionAspect.AutoDestroy.GetOrAddComponent(entity);
                
                sequenceComponent.Target = startSequenceRequest.Target;
                sequenceComponent.Action = action;
                
                var startActions = SequenceActionResult.None;
                var actionName = action.ActionName;
                
                resultComponent.Value = startActions;

                var packedEntity = entity.PackEntity(_world);
                var token = lifeTimeComponent.Token;
                var task = action.ExecuteAsync(packedEntity, _world, token)
                    .AttachExternalCancellation(token)
                    .Preserve();

                sequenceAction.Action = action;
                sequenceAction.ActionName = actionName;
                sequenceAction.Task = task;
                sequenceAction.Token = token;
                
                task.Forget();
                
                //mark sequence as active
                _actionAspect.Active.GetOrAddComponent(entity);
            }
        }
    }
}