namespace Game.Ecs.Input
{
    using System;
    using Components.Requests;
    using Cysharp.Threading.Tasks;
    using Data.ActionMap;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class InputFeature : EcsFeature
    {
        [SerializeField]
        private InputActionsMapData inputActionsData;
        
        protected override UniTask OnInitializeAsync(IProtoSystems ecsSystems)
        {
            // System responsible for initializing input actions.
            ecsSystems.AddSystem(new InitInputSystem());
            // System responsible for switching input maps.
            ecsSystems.AddSystem(new SwitchInputMapSystem(inputActionsData));
            // Delete used SwitchInputMapRequest
            ecsSystems.DelHere<SwitchInputMapRequest>();
            
            return UniTask.CompletedTask;
        }
    }
}