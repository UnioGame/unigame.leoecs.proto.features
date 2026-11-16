namespace UniGame.Ecs.Proto.Ability.Common.Components
{
    using System;
    using Game.Code.Configuration.Runtime.Ability;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// create new ability for target
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct CreateGameAbilityRequest
    {
        public ProtoPackedEntity Target;
        public AbilityConfiguration Configuration;
        public int AbilityId;
        public bool IsDefault;
        public int SlotId;
        public bool IsInput;
    }
}