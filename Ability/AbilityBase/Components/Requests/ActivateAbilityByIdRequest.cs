namespace UniGame.Ecs.Proto.Ability.Components.Requests
{
    using System;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// Запрос взять способность по id в руку и применить её.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ActivateAbilityByIdRequest
    {
        public int AbilityId;
        public ProtoPackedEntity Target;
    }
}