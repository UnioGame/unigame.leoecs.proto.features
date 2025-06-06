namespace Game.Modules.leoecs.proto.features.Ability.AbilityBase.Components
{
    using System;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Proto;
    using Unity.Collections;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityContextComponent : IProtoAutoReset<AbilityContextComponent>
    {
        public ProtoPackedEntity abilityOwner;
        public ProtoPackedEntity abilityEntity;
        public NativeList<ProtoPackedEntity> targets;
        
        public void SetHandlers(IProtoPool<AbilityContextComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref AbilityContextComponent c)
        {
            if (c.targets.IsCreated)
            {
                c.targets.Clear();
            }
            else
            {
                c.targets = new NativeList<ProtoPackedEntity>(12, Allocator.Persistent);
            }
        }

        public ProtoPackedEntity FirstTarget()
        {
            if (targets.Length > 0)
            {
                return targets[0];
            }

            return default;
        }
    }
}