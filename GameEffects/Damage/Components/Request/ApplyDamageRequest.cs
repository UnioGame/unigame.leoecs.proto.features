namespace UniGame.Ecs.Proto.Gameplay.Damage.Components.Request
{
    using System;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// Запрос нанести урон: кто бьёт, по кому, сколько и чем (эффект-источник). Отсюда урон проходит через
    /// крит, блок, уклонение и щит и списывается со здоровья.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ApplyDamageRequest : IProtoAutoReset<ApplyDamageRequest>
    {
        public float Value;
        public bool IsCritical;

        public ProtoPackedEntity Source;
        public ProtoPackedEntity Destination;
        public ProtoPackedEntity Effector;
        
        public void SetHandlers(IProtoPool<ApplyDamageRequest> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref ApplyDamageRequest c)
        {
            c.Value = 0.0f;
            c.IsCritical = false;
        }
    }
}