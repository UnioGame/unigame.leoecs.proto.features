namespace Game.Modules.SequenceActions.Systems
{
    using System;
    using Abstract;
    using Aspects;
    using Components;
    using Components.Requests;
    using Cysharp.Threading.Tasks;
    using Modules.SequenceActions.Data;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.AddressableTools.Runtime;
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
    public class StartSequenceByIdSystem : IProtoRunSystem
    {
        private SequenceAspect _actionAspect;
        private ISequenceActionService _sequenceActionService;
        private ProtoWorld _world;
        
        
        private ProtoItExc _startSequenceFilter = It
            .Chain<StartSequenceByIdRequest>()
            .Exc<SequenceComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _startSequenceFilter)
            {
                var startSequenceRequest = _actionAspect.StartById.Get(entity);
                if (string.IsNullOrEmpty(startSequenceRequest.Sequence)) 
                    continue;
                
                var sequenceData = _sequenceActionService
                    .GetAction(startSequenceRequest.Sequence);
                if (sequenceData == null) continue;

                StartSequenceAsync(startSequenceRequest).Forget();
            }
        }

        public async UniTask StartSequenceAsync(StartSequenceByIdRequest request)
        {

            var sequenceData = _sequenceActionService.GetAction(request.Sequence);
            var sequenceAsset = await sequenceData
                .ActionAsset
                .LoadAssetTaskAsync(_world.GetWorldLifeTime());
                
            StartSequence(sequenceAsset.actions,request);
          
        }
        
        public void StartSequence(ISequenceAction action,StartSequenceByIdRequest request)
        {
            var requestEntity = _world.NewEntity();
            ref var startSequence = ref _actionAspect.Start.GetOrAddComponent(requestEntity);
            startSequence.Action = action;
            startSequence.Target = request.Target;
            startSequence.AutoDestroy = request.AutoDestroy;
        }
    }
}