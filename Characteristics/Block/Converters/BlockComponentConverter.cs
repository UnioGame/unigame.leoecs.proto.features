namespace UniGame.Ecs.Proto.Characteristics.Block.Converters
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using UniGame.Ecs.Proto.Characteristics.Base.Components.Requests;

    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    /// <summary>
    /// Converts block data and applies it to the target game object in the ECS world.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class BlockComponentConverter : LeoEcsConverter
    {
        public float block = 0f;

#if ODIN_INSPECTOR
        [MaxValue(100)]
#endif
        [SerializeField]
        public float maxDodge = 100f;

#if ODIN_INSPECTOR
        [MinValue(0)]
#endif
        public float minDodge = 0f;

        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var createCharacteristicRequest =
                ref world.GetOrAddComponent<CreateCharacteristicRequest<BlockComponent>>(entity);
            createCharacteristicRequest.Value = block;
            createCharacteristicRequest.MaxValue = maxDodge;
            createCharacteristicRequest.MinValue = minDodge;
            createCharacteristicRequest.Owner = entity.PackEntity(world);

            ref var healthComponent = ref world.GetOrAddComponent<BlockComponent>(entity);
            healthComponent.Value = block;
            healthComponent.MaxValue = maxDodge;
            healthComponent.MinValue = minDodge;
        }
    }
}