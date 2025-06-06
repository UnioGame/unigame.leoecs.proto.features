namespace UniGame.Ecs.Proto.Characteristics.Cooldown.Components
{
    using System.Collections.Generic;
    using Characteristics;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;


    public struct BaseCooldownComponent : IProtoAutoReset<BaseCooldownComponent>
    {
        public float Value;
        
        public List<Modification> Modifications;
        
        public void SetHandlers(IProtoPool<BaseCooldownComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref BaseCooldownComponent c)
        {
            c.Modifications ??= new List<Modification>();
            c.Modifications.Clear();
        }
    }
}