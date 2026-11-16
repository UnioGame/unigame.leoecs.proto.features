namespace UniGame.Ecs.Proto.Effects.Components
{
    using System;
    using System.Collections.Generic;
    using Game.Code.Configuration.Runtime.Effects.Abstract;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;

    /// <summary>
    /// Список эффектов, которые нужно наложить (например, шаг способности «наложить эффекты»).
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
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