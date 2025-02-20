namespace Game.Ecs.State.States.Types.SequenceBehaviours
{
    using System;
    using Cysharp.Threading.Tasks;
    using Data;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Modules.SequenceActions;
    using UniGame.AddressableTools.Runtime;
    using UniGame.AddressableTools.Runtime.AssetReferencies;
    using UniModules.UniCore.Runtime.DataFlow;

    [Serializable]
    public class StateSequenceBehaviour : IStateBehaviour
    {
        public StateId nextState;
        public StateId fallbackState;
        
        public AddressableValue<SequenceActionAsset> sequenceAction = new();
        
        private LifeTime _lifeTime = new LifeTime();
        private UniTask _task;
        private bool _isActive = false;
        
        public void Initialize(ProtoWorld world)
        {
            OnInitialize(world);
        }

        public void Enter(ProtoEntity entity, ProtoWorld world)
        {
            _lifeTime.Restart();

            var packedEntity = world.PackEntity(entity);
            if (!packedEntity.Unpack(world, out var unpackedEntity))
                return;
            
            _isActive = true;
            _task = ExecuteAsync(packedEntity, world);
        }

        public StateResult Update(ProtoEntity entity, ProtoWorld world)
        {
            var status = _task.Status;
            if (status == UniTaskStatus.Pending)
                return StateResult.Default;
            
            _isActive = false;
            var failed = status != UniTaskStatus.Succeeded;
            var completeState = failed ? fallbackState : nextState;

            return new StateResult()
            {
                Completed = true,
                Failed = status != UniTaskStatus.Succeeded,
                NextState = completeState,
                Error = failed ? StateSequenceMessages.StateExecutionFailedMessage : string.Empty,
                Message = string.Empty,
            };
        }

        public void Exit(ProtoEntity entity, ProtoWorld world)
        {
            _lifeTime.Restart();
            _isActive = false;
        }

        public async UniTask ExecuteAsync(ProtoPackedEntity entity, ProtoWorld world)
        {
            var sequenceActionAsset = await sequenceAction.reference
                .LoadAssetInstanceTaskAsync(_lifeTime, true);
            var action = sequenceActionAsset.actions;
            await action.ExecuteAsync(entity, world, _lifeTime.Token);
        }

        protected virtual void OnInitialize(ProtoWorld world)
        {
            
        }
    }
}