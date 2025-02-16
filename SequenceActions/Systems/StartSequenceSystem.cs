namespace Game.Modules.SequenceActions.Systems
{
    using System;
    using System.Buffers;
    using Aspects;
    using Components;
    using Components.Requests;
    using Data;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

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
        private SequenceActionAspect _actionAspect;
        private ProtoWorld _world;

        private ProtoItExc _startSequenceFilter = It
            .Chain<StartSequenceRequest>()
            .Exc<SequenceDataComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _startSequenceFilter)
            {
                ref var startSequenceRequest = ref _actionAspect.Start.Get(entity);
                if(!startSequenceRequest.Target.Unpack(_world, out var target))
                    continue;

                var actions = startSequenceRequest.Actions;
                if(actions == null || actions.Length == 0)
                    continue;
                
                ref var sequenceDataComponent = ref _actionAspect.SequenceData.Add(entity);
                ref var sequenceProgressComponent = ref _actionAspect.SequenceProgress.Add(entity);
                ref var lifeTimeComponent = ref _actionAspect.LifeTime.Add(entity);
                
                var length = actions.Length;
                sequenceDataComponent.Actions = actions;
                sequenceDataComponent.Target = startSequenceRequest.Target;
                sequenceDataComponent.Length = length;
                sequenceDataComponent.Results = ArrayPool<SequenceActionResult>.Shared.Rent(length);
                sequenceDataComponent.ActiveAction = 0;

                for (var i = 0; i < actions.Length; i++)
                {
                    ref var actionItem = ref actions[i];
                    sequenceProgressComponent.MaxProgress += actionItem.progressWeight;
                }
                
                ref var startActionRequest = ref _actionAspect.StartAction.Add(entity);
                startActionRequest.Action = actions[0].action;
                startActionRequest.Token = lifeTimeComponent.Token;
            }
        }
    }
}