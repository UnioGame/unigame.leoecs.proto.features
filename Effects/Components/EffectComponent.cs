namespace UniGame.Ecs.Proto.Effects.Components
{
    using System;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// Эффект: кто его наложил (Source) и на кого (Destination). Всё остальное в своих компонентах,
    /// например урон или лечение.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct EffectComponent
    {
        public ProtoPackedEntity Source;
        public ProtoPackedEntity Destination;
    }
}