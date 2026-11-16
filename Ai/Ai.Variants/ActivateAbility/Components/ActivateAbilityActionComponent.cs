namespace Game.Code.Ai.ActivateAbility
{
    using System;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// Действие ИИ «применить способность» на этот кадр: какую способность и по какой цели.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ActivateAbilityActionComponent
    {
        public int AbilityCellId;
        public ProtoPackedEntity Target;
        public ProtoPackedEntity Ability;
    }
}
