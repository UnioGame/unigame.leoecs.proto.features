namespace UniGame.Ecs.Proto.Ability.Components
{
    using LeoEcs.Proto;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// Цели для применения умения.
    /// </summary>
    public struct AbilityTargetsComponent : IProtoAutoReset<AbilityTargetsComponent>
    {
        public static readonly ProtoPackedEntity Empty = default;
        
        public ProtoPackedEntity[] Entities;
        public int Count;
        
        public ProtoPackedEntity[] PreviousEntities;
        public int PreviousCount;
        
        public void SetHandlers(IProtoPool<AbilityTargetsComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref AbilityTargetsComponent c)
        {
            c.Entities = new ProtoPackedEntity[12];
            c.Count = 0;

            c.PreviousEntities = null;
            c.Count = 0;
        }
    }
}