namespace UniGame.Ecs.Proto.Characteristics.ManaRegeneration.Converters
{
    using System;
    using Components;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using UnityEngine;
  
#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    /// <summary>
    /// Converts mana regeneration data and applies it to the target game object in the ECS world.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public sealed class ManaRegenerationComponentConverter : GameCharacteristicConverter<ManaRegenerationComponent>
    {
#if ODIN_INSPECTOR
        [ShowInInspector, PropertyRange(0f, 1f)]
#endif
        public float tickTime = 0.2f;

        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            base.Apply(target, world, entity);
            ref var manaRegenerationTimerComponent = ref world
                .AddComponent<ManaRegenerationTimerComponent>(entity);
            
            manaRegenerationTimerComponent.TickTime = tickTime;
            manaRegenerationTimerComponent.LastTickTime = Time.time;
        }

    }
}