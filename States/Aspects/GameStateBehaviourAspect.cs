namespace Game.Ecs.State.Aspects
{
    using System;
    using System.Runtime.CompilerServices;
    using Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class GameStateBehaviourAspect : EcsAspect
    {
        public GameStatesAspect StatesAspect;
        
        public ProtoPool<StateBehaviourComponent> StateBehaviour;
        public ProtoPool<StateBehavioursMapComponent> StateBehavioursMap;
        
        //requests
        
        /// <summary>
        /// update to actual behaviour
        /// </summary>
        public ProtoPool<UpdateStateBehaviourRequest> UpdateStateBehaviour;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UpdateEntityBehaviour(ProtoEntity entity,ProtoWorld entityWorld)
        {
            ref var stateBehaviourComponent = ref StateBehaviour.Get(entity);
            ref var stateComponent = ref StatesAspect.State.Get(entity);
            ref var stateBehavioursMapComponent = ref StateBehavioursMap.Get(entity);
                
            var stateId = stateComponent.Id;
            if(!stateBehavioursMapComponent.Behaviours.TryGetValue(stateId, out var behaviour))
                return;

            var activeBehaviour = stateBehaviourComponent.Value;
            if(activeBehaviour == behaviour) return;

            activeBehaviour?.Exit(entity,entityWorld);
            
            stateBehaviourComponent.Value = behaviour;
            behaviour.Enter(entity,entityWorld);
        }
        
        #region statics

        public static ProtoEntity CreateStateBehaviourEntity(ProtoEntity entity,ProtoWorld world)
        {
            ref var stateBehaviour = ref world.AddComponent<StateBehaviourComponent>(entity);
            ref var stateBehaviours = ref world.AddComponent<StateBehavioursMapComponent>(entity);
            ref var updateStateBehaviour = ref world.AddComponent<UpdateStateBehaviourRequest>(entity);
            return entity;
        }

        #endregion
        
        
    }
}