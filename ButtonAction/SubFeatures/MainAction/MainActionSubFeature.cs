namespace Game.Ecs.ButtonAction.SubFeatures.MainAction
{
    using System;
    using Components.Events;
    using Cysharp.Threading.Tasks;
    using Data;
    using Systems;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using Object = UnityEngine.Object;

    /// <summary>
    /// MainAction sub feature of the ButtonAction feature
    /// </summary>
    [Serializable]
    public class MainActionSubFeature : IGameActionsSubFeature
    {
        [SerializeField]
        [InlineEditor]
        public GameActionsMap mainActionMap;

        public UniTask<IProtoSystems> InitializeActions(IProtoSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            var instMap = Object.Instantiate(mainActionMap);
            ecsWorld.SetGlobal(instMap.value);
            
            //Delete used ButtonActionEvent
            ecsSystems.DelHere<ButtonActionEvent<GameActionId>>();
            
            // System for handling main button actions.
            ecsSystems.Add(new MainActionSystem());
            
            return UniTask.FromResult(ecsSystems);
        }
    }
}