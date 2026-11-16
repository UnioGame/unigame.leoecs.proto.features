namespace UniGame.Ecs.Proto.GameAi.ActivateAbility
{
    using System;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// Решение ИИ на этот кадр: какую способность применить и по кому. Из него планировщик собирает
    /// действие.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityAiActionTargetComponent
    {
        public int AbilityCellId;
        public ProtoPackedEntity Ability;
        public ProtoPackedEntity AbilityTarget;
    }

}