namespace UniGame.Ecs.Proto.Ability.Components
{
    using System;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// У владельца прямо сейчас идёт способность: какая, в каком слоте и основная ли она. ИИ по нему
    /// понимает, что перебивать нельзя.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityInProcessingComponent
    {
        public ProtoPackedEntity Ability;
        public bool IsDefault;
        public int Slot;
    }
}