namespace Game.Ecs.State
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Components.Events;
    using Components.Requests;
    using Cysharp.Threading.Tasks;
    using Data;
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
        public List<BaseStateFeature> states = new();
        
        public sealed override async UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            var mapAsset = Instantiate(statesMap);
            var map = mapAsset.map;
            
            var world = ecsSystems.GetWorld();
            world.SetGlobal(map);
            
            // Delete used StateChangedEvent
            ecsSystems.DelHere<StateChangedSelfEvent>();
            
            // System for changing the state of an entity.
            ecsSystems.Add(new StopStateSystem());
            ecsSystems.Add(new SetStateByTypeSystem());
            ecsSystems.Add(new SetStateSystem());
            
            foreach (var stateSystem in states)
                await stateSystem.InitializeAsync(ecsSystems);
            
            ecsSystems.DelHere<SetStateByTypeSelfRequest>();
            ecsSystems.DelHere<SetStateSelfRequest>();
            ecsSystems.DelHere<StopStateSelfRequest>();
        }


#if UNITY_EDITOR
#if ODIN_INSPECTOR
        [Button]
#endif
        public void FillStates()
        {
            var stateTypes = TypeCache.GetTypesDerivedFrom(typeof(BaseStateFeature));

            foreach (var stateType in stateTypes)
            {
                if (stateType.IsAbstract) continue;
                if(stateType.IsInterface) continue;
                if(states.Any(x => x.GetType() == stateType)) continue;
                var stateFeature = (BaseStateFeature)Activator.CreateInstance(stateType);
                
                states.Add(stateFeature);
            }
            
        }
#endif

        
    }
}