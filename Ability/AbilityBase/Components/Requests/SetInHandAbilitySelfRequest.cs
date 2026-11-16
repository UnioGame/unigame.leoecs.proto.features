namespace UniGame.Ecs.Proto.Ability.Common.Components
{
    using System;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// Запрос "положить" в руку умение по энтити. Умение должно принадлежать этому же чемпиону.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SetInHandAbilitySelfRequest
    {
        public ProtoPackedEntity Value;
    }
}