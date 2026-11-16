namespace UniGame.Ecs.Proto.Ability.Common.Components
{
    using System;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// Запрос на владельце: применить эту способность. Она должна принадлежать ему и лежать у него в руке.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ApplyAbilitySelfRequest
    {
        public ProtoPackedEntity Value;
    }
}