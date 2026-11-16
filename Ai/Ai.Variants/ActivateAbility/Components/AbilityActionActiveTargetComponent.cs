namespace UniGame.Ecs.Proto.GameAi.ActivateAbility.Components
{
    using System;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// Цель, которую ИИ сейчас держит для этой способности.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityActionActiveTargetComponent
    {
        public ProtoPackedEntity Value;
    }
}