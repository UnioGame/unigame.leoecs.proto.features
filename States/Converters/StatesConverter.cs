namespace Game.Ecs.State.Converters
{
    using System;
    using System.Collections.Generic;
    using Aspects;
    using Components;
    using Components.Requests;
    using Data;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif

#if UNITY_EDITOR
    using UnityEditor;
#endif
    
    /// <summary>
    /// Converter that can be used to apply a state to a GameObject.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class StatesConverter : GameObjectConverter
    {

#if ODIN_INSPECTOR
        [ValueDropdown(nameof(GetStates))]
        [BoxGroup(nameof(state))]
#endif
        public int state;

        protected override void OnApply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var stateComponent = ref world.GetOrAddComponent<StateComponent>(entity);
            ref var request = ref world.GetOrAddComponent<SetStateSelfRequest>(entity);
            
            //set active state with request
            stateComponent.Value = 0;
            request.Id = state;
        }

#if ODIN_INSPECTOR
        
        public IEnumerable<ValueDropdownItem<int>> GetStates()
        {
#if UNITY_EDITOR
            var stateTypes = StatesMapAsset.GetStates();
            
            foreach (var stateValue in stateTypes)
            {
                yield return new ValueDropdownItem<int>()
                {
                    Value = stateValue.id,
                    Text = stateValue.name,
                };
            }
#endif
            yield break;
        }
        
#endif
    }
}