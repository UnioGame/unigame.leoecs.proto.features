namespace Game.Ecs.Input.Systems
{
    using System;
    using Aspects;
    using Data.ActionMap;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// System responsible for switching input maps.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class SwitchInputMapSystem : IEcsRunSystem
    {
        private ProtoWorld _world;
        private InputAspect _inputAspect;
        
        private ProtoIt _requestFilter;
        private ProtoIt _inputFilter;
        
        private InputActionsMapData _inputActionsData;

        public SwitchInputMapSystem(InputActionsMapData inputActionsData)
        {
            _inputActionsData = inputActionsData;
        }

        public void Run()
        {
            foreach (var requestEntity in _inputAspect.SwitchInputFilter)
            {
                ref var switchInputMapRequest = ref _inputAspect.SwitchInputMap.Get(requestEntity);
                var actionId = switchInputMapRequest.actionsMapId;
                var mapName = _inputActionsData.GetInputActionMap(actionId);

                foreach (var inputEntity in _inputAspect.InputFilter)
                {
                    ref var inputActionComponent = ref _inputAspect.InputActions.Get(inputEntity);
                    var inputActions = inputActionComponent.InputActions;
                    var maps = inputActions.asset.actionMaps;
                    
                    inputActions.Disable();

                    foreach (var map in maps)
                    {
                        if(map.name != mapName.name) continue;
                        map.Enable();
                        break;
                    }
                }
            }
        }
    }
}