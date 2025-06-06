namespace UniGame.Ecs.Proto.Effects.Components
{
    using System.Collections.Generic;
    using Game.Code.Configuration.Runtime.Effects.Abstract;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;


    public struct EffectsComponent : IProtoAutoReset<EffectsComponent>
    {
        public List<IEffectConfiguration> Effects;
        
        public void SetHandlers(IProtoPool<EffectsComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref EffectsComponent c)
        {
            c.Effects ??= new List<IEffectConfiguration>();
            c.Effects.Clear();
        }
    }
}