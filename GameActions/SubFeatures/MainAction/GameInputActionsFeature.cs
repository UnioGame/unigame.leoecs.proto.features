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

#if UNITY_EDITOR
    using UniModules.Editor;
    using UnityEditor;
#endif
    
    /// <summary>
    /// MainAction sub feature of the ButtonAction feature
    /// </summary>
    [Serializable]
    public class GameInputActionsFeature : GameActionsSubFeatureAsset
    {
        [SerializeField]
        [InlineEditor]
        public GameActionsMap mainActionMap;

        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            var instMap = Instantiate(mainActionMap);
            ecsWorld.SetGlobal(instMap.value);
            ecsSystems.AddService(instMap.value);
            
            //Delete used ButtonActionEvent
            ecsSystems.DelHere<ButtonActionEvent<GameActionId>>();
            
            // System for handling main button actions.
            ecsSystems.Add(new MainActionSystem());
            
            return UniTask.CompletedTask;
        }


#if UNITY_EDITOR

        public override void EditorInitialize(string directory)
        {
            base.EditorInitialize(directory);

            if (mainActionMap != null) return;
            mainActionMap = CreateInstance<GameActionsMap>();
            mainActionMap.name = nameof(GameActionsMap);
            mainActionMap.SaveAsset(directory);
        }
#endif
    }
}