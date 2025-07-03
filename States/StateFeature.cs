namespace Game.Ecs.State
{
    using System.Collections.Generic;
    using System.Linq;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Components.Events;
    using Components.Requests;
    using Cysharp.Threading.Tasks;
    using Data;
    using Sirenix.OdinInspector;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniModules;
    using UniModules.UniCore.Runtime.Utils;
    using UnityEngine;
    
#if UNITY_EDITOR
    using UnityEditor;
    using UniModules.Editor;
#endif

    [CreateAssetMenu(menuName = "ECS Proto/Features/States/States Feature",fileName = "StatesFeature")]
    public class StateFeature : BaseLeoEcsFeature
    {
        [Header("states map")]
        [InlineEditor]
        [HideLabel]
        public StateDataAsset stateMap;

        public sealed override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            var instMap = Instantiate(stateMap);
            
            ecsWorld.SetGlobal(instMap.Map);

            // Delete used StateChangedEvent
            ecsSystems.DelHere<StateChangedSelfEvent>();

            // System for adding a state to entities.
            ecsSystems.Add(new AddStateSystem());
            // System for removes states from entities.
            ecsSystems.Add(new RemoveStateSystem());
            // System for changing the state of an entity.
            ecsSystems.Add(new ChangeStateSystem());
            // System for updating the state history of an entity.
            ecsSystems.Add(new UpdateStateHistorySystem());

            foreach (var stateSystem in stateMap.stateSystems)
                ecsSystems.AddSystem(stateSystem);
            
            ecsSystems.DelHere<AddStateSelfRequest>();
            ecsSystems.DelHere<RemoveStateSelfRequest>();
            ecsSystems.DelHere<ChangeStateSelfRequest>();
            
            //check current behaviour and set actual by active state
            ecsSystems.AddSystem(new SwitchActiveStateBehaviourSystem());
            //update active state behaviour of entity
            ecsSystems.AddSystem(new UpdateStateBehaviourSystem());

            return UniTask.CompletedTask;
        }

#if UNITY_EDITOR
#if ODIN_INSPECTOR
        
        [OnInspectorInit]
        private void OnInspectorInit()
        {
            if (stateMap == null)
            {
                var statesAsset = this.CreateAsset<StateDataAsset>("GameStatesMap");
                stateMap = statesAsset;
                this.MarkDirty();
                AssetDatabase.SaveAssets();
            }
        }
        
        
#endif
#endif
        
    }
}