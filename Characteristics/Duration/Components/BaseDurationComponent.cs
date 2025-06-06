namespace UniGame.Ecs.Proto.Characteristics.Duration.Components
{
    using System;
    using System.Collections.Generic;
    using Characteristics;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;

    /// <summary>
    /// Represents the base duration component.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct BaseDurationComponent : IProtoAutoReset<BaseDurationComponent>
    {
        public float Value;
        
        public List<Modification> Modifications;
        
        public void SetHandlers(IProtoPool<BaseDurationComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref BaseDurationComponent c)
        {
            c.Modifications ??= new List<Modification>();
            c.Modifications.Clear();
        }
    }
}