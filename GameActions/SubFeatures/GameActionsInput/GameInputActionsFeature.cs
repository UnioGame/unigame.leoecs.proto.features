namespace Game.Ecs.GameActions
{
    using System;
    using Cysharp.Threading.Tasks;
    using Data;
    using Leopotam.EcsProto;
    using Systems;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using Object = UnityEngine.Object;

#if UNITY_EDITOR
    using UniModules.Editor;
#endif
    
    /// <summary>
    /// MainAction sub feature of the ButtonAction feature
    /// </summary>
    [Serializable]
    public class GameInputActionsFeature : EcsFeature
    {
        [SerializeField]
        [InlineEditor]
        public GameActionsMap mainActionMap;

        protected override UniTask OnInitializeAsync(IProtoSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            var instMap = Object.Instantiate(mainActionMap);
            ecsWorld.SetGlobal(instMap.value);
            ecsSystems.AddService(instMap.value);

            ecsSystems.AddSystem(new InitializeGameActionsInputSystem());
            
            return UniTask.CompletedTask;
        }


#if UNITY_EDITOR

        public void EditorInitialize(string directory)
        {
            if (mainActionMap != null) return;
            mainActionMap = ScriptableObject.CreateInstance<GameActionsMap>();
            mainActionMap.name = nameof(GameActionsMap);
            mainActionMap.SaveAsset(directory);
        }
        
#endif
    }
}