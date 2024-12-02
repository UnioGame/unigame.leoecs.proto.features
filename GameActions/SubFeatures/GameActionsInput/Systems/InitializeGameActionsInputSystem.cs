namespace Game.Ecs.GameActions.Systems
{
    using System;
    using ButtonAction.Aspects;
    using Data;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class InitializeGameActionsInputSystem : IEcsInitSystem
    {
        private ProtoWorld _world;
        private GameActionsData _gameActionsData;
        private GameActionAspect _gameActionAspect;

        public void Init(IProtoSystems systems)
        {
            var actionMap = _world.NewEntity();
            ref var inputActionsComponent = ref _gameActionAspect.GameActionsMap.Add(actionMap);

            var actions = _gameActionsData.collection;

            inputActionsComponent.Actions = new int[actions.Count];
            inputActionsComponent.Status = new bool[actions.Count];
            inputActionsComponent.Names = new string[actions.Count];
            inputActionsComponent.Length = actions.Count;

            for (var i = 0; i < actions.Count; i++)
            {
                inputActionsComponent.Actions[i] = i;
                inputActionsComponent.Status[i] = actions[i].enabled;
                inputActionsComponent.Names[i] = actions[i].name;
            }
        }
    }
}