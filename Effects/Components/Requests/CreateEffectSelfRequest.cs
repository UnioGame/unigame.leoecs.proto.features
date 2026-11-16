namespace UniGame.Ecs.Proto.Effects.Components
{
    using System;
    using Game.Code.Configuration.Runtime.Effects.Abstract;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// Запрос создать эффект: кто накладывает, на кого и какой конфиг. Сущность запроса становится
    /// сущностью эффекта.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct CreateEffectSelfRequest
    {
        public ProtoPackedEntity Source;
        public ProtoPackedEntity Destination;

        public IEffectConfiguration Effect;
    }
}