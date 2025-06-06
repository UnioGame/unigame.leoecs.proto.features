namespace UniGame.Ecs.Proto.Effects.Components
{
    using System.Collections.Generic;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;


    /// <summary>
    /// Список эффектов на игровой сущности. При обработке эффектов, они добавляются в этот список или
    /// убираются из него. Также список автоматически валидируется на момент мертвых эффектов.
    /// </summary>
    public struct EffectsListComponent : IProtoAutoReset<EffectsListComponent>
    {
        public List<ProtoPackedEntity> Effects;
        
        public void SetHandlers(IProtoPool<EffectsListComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref EffectsListComponent c)
        {
            c.Effects ??= new List<ProtoPackedEntity>();
            c.Effects.Clear();
        }
    }
}