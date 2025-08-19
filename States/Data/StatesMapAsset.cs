namespace Game.Ecs.State.Data
{
    using System;
    using System.Collections.Generic;
    using UniGame.Core.Runtime.SerializableType;
    using UnityEngine;
    
#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif

#if UNITY_EDITOR
    using UniModules.Editor;
    using UnityEditor;
#endif

    [CreateAssetMenu(menuName = "ECS Proto/Features/States/StatesMap",fileName = "StatesMap")]
    public class StatesMapAsset : ScriptableObject
    {
#if ODIN_INSPECTOR
        [InlineProperty]
        [HideLabel]
#endif
        public StatesMap map = new();


        private static StatesMapAsset _editorMap;

        public static IEnumerable<StateData> GetStates()
        {
#if UNITY_EDITOR
            _editorMap ??= AssetEditorTools.GetAsset<StatesMapAsset>();
            if(_editorMap == null) yield break;

            var map = _editorMap.map;
            var states = map.states;
            
            foreach (var state in states)
            {
                yield return state;
            }
#endif
            yield break;
        }
        
#if UNITY_EDITOR

        [Button]
        public void UpdateStates()
        {

            var states = map.states;
            states.Clear();
            
            var stateTypes = TypeCache.GetTypesDerivedFrom(typeof(IStateComponent));
            var counter = 1;
            
            foreach (var stateType in stateTypes)
            {
                if(stateType.IsAbstract) continue;
                if(stateType.IsInterface) continue;

                var stateName = stateType.Name;
                stateName = stateName.Replace("Component", string.Empty);
                
                states.Add(new StateData()
                {
                    stateType = stateType,
                    name = stateName,
                    id = counter,
                });

                counter++;
            }

            this.MarkDirty();

        }
#endif
    }
    
    [Serializable]
    public class StatesMap
    {
        public List<StateData> states = new();
    }

    [Serializable]
    public class StateData
    {
        public string name;
        public int id;
        public SType stateType;
    }
}