namespace UniGame.Ecs.Proto.Gameplay.Damage.Components.Request
{
    using LeoEcs.Proto;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;


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