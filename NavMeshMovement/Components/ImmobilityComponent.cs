namespace UniGame.Ecs.Proto.Movement.Components
{
    using LeoEcs.Proto;
    using Leopotam.EcsProto;


    public struct ImmobilityComponent : IProtoAutoReset<ImmobilityComponent>
    {
        public int BlockSourceCounter;
        
        public void SetHandlers(IProtoPool<ImmobilityComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref ImmobilityComponent c)
        {
            c.BlockSourceCounter = 0;
        }
    }
}