namespace Game.Ecs.State
{
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Systems;

    public static class StatesExtensions
    {
        public static IProtoSystems RegisterState<TState, TStateFinishedEvent,TStateStartEvent>(this IProtoSystems ecsSystems)
            where TState : struct, IStateComponent
            where TStateFinishedEvent : struct
            where TStateStartEvent : struct
        {
            ecsSystems.DelHere<TStateStartEvent>();
            ecsSystems.AddSystem(new StateStartedEventSystemT<TState,TStateStartEvent>());
            
            ecsSystems.RegisterState<TState,TStateFinishedEvent>();
            return ecsSystems;
        }
        
        public static IProtoSystems RegisterState<TState, TStateFinishedEvent>(this IProtoSystems ecsSystems)
            where TState : struct, IStateComponent
            where TStateFinishedEvent : struct
        {
            ecsSystems.DelHere<TStateFinishedEvent>();
            ecsSystems.AddSystem(new StateFinishedEventSystemT<TState,TStateFinishedEvent>());
            ecsSystems.RegisterState<TState>();
            return ecsSystems;
        }
        
        public static IProtoSystems RegisterState<TState>(this IProtoSystems ecsSystems)
            where TState : struct, IStateComponent
        {
            ecsSystems.AddSystem(new StateChangedSystemT<TState>());
            ecsSystems.AddSystem(new SetStateSystemT<TState>());
            return ecsSystems;
        }

    }
}