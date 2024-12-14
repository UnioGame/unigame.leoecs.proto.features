namespace Vampire.Game.ECS.UI.GameActionsViews.Extensions
{
    using global::Game.Ecs.GameActions.Data;
    using Leopotam.EcsProto;
    using Systems;

    public static class GameActionViewExtensions
    {
        public static void FireComponentByGameAction<TComponent>(this ProtoSystems systems, GameActionId id)
            where TComponent : struct
        {
            systems.AddSystem(new TriggerComponentByGameActionSystem<TComponent>(id));   
        }
        
        public static void FireComponentByGameAction<TComponent>(this ProtoSystems systems, int id)
            where TComponent : struct
        {
            systems.AddSystem(new TriggerComponentByGameActionSystem<TComponent>(id));   
        }
    }
}