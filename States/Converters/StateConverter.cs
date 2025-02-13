namespace Game.Ecs.State.Converters
{
    using System;
    using System.Collections.Generic;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    using Data;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;

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
        public List<StateId> states = new();
        
#if ODIN_INSPECTOR
        [ValueDropdown(nameof(GetStates))]
#endif
        private StateId activeState = StateId.Empty;
        
        public bool addBehaviour = true;
        
        [ShowIf(nameof(addBehaviour))]
        [HideLabel]
        [InlineProperty]
        public StateBehavioursConverter behaviours = new StateBehavioursConverter();

        protected override void OnApply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            GameStatesAspect.CreateStatesEntity(entity, world);

            ref var statesMapComponent = ref world.GetComponent<StatesMapComponent>(entity);
            ref var stateComponent = ref world.GetComponent<StateComponent>(entity);
            
            foreach (var state in states)
                statesMapComponent.States.Add(state);
            
            stateComponent.Id = activeState;
            
            if(addBehaviour)
                behaviours.Apply(target, world, entity);
        }

#if ODIN_INSPECTOR
        
        public IEnumerable<ValueDropdownItem<StateId>> GetStates()
        {
            foreach (var state in states)
            {
                yield return new ValueDropdownItem<StateId>(StateId.GetStateName(state), state);
            }
        }
        
#endif
    }
}