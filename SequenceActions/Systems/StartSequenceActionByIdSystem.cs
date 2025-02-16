namespace Game.Modules.SequenceActions.Systems
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Aspects;
    using Components;
    using Components.Requests;
    using leoecs.proto.features.SequenceActions.Data;
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
    public class StartSequenceActionByIdSystem : IProtoInitSystem, IProtoRunSystem
    {
        private SequenceActionAspect _actionAspect;
        
        private ProtoWorld _world;
        private Dictionary<string,SequenceActionItem> _actions;
        
        private ProtoItExc _startSequenceFilter = It
            .Chain<StartSequenceByIdRequest>()
            .Exc<SequenceDataComponent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            var map = _world.GetGlobal<SequenceActionsMapAsset>();
            _actions = map.actions.ToDictionary(x => x.ActionName);
        }

        public void Run()
        {
            foreach (var entity in _startSequenceFilter)
            {
                
            }
        }
    }
}