namespace Game.Ecs.State
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Components.Events;
    using Components.Requests;
    using Cysharp.Threading.Tasks;
    using State;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    
#if UNITY_EDITOR
    using UnityEditor;
    using UniModules.Editor;
#endif

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif

    [CreateAssetMenu(menuName = "ECS Proto/Features/States/States Feature",fileName = "StatesFeature")]
    public class StatesFeature : BaseLeoEcsFeature
    {
        public StatesMapAsset statesMap;
        
        [SerializeReference]
        public List<IStateFeature> states = new();
        
        public sealed override async UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            var mapAsset = Instantiate(statesMap);
            var map = mapAsset.map;
            
            var world = ecsSystems.GetWorld();
            world.SetGlobal(map);

            ecsSystems.Add(new InitializeStateSystem());

            foreach (var stateFeature in states)
            {
                if(stateFeature.IsFeatureEnabled == false)
                    continue;
                await stateFeature.InitializeAsync(ecsSystems);
            }
            
                        
            // Delete used StateChangedEvent
            ecsSystems.DelHere<StateChangedSelfEvent>();

            // System for changing the state of an entity.
            ecsSystems.Add(new StopStateSystem());
            ecsSystems.Add(new SetStateSystem());

            ecsSystems.DelHere<SetStateSelfRequest>();
            ecsSystems.DelHere<StopStateSelfRequest>();
        }


#if UNITY_EDITOR
#if ODIN_INSPECTOR
        [Button]
#endif
        [ContextMenu("Fill States")]
        public void FillStates()
        {
            var stateTypes = TypeCache.GetTypesDerivedFrom(typeof(IStateFeature));

            foreach (var stateType in stateTypes)
            {
                if (stateType.IsAbstract) continue;
                if(stateType.IsInterface) continue;
                if(states.Any(x => x.GetType() == stateType)) continue;
                var stateFeature = (IStateFeature)Activator.CreateInstance(stateType);
                
                states.Add(stateFeature);
            }
            
            statesMap.UpdateStates();
        }
#endif

        
    }
}