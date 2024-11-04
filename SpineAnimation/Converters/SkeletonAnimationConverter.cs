namespace Game.Ecs.SpineAnimation.Converters
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using Spine.Unity;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// Converts a Skeleton Animation Component to entity in ECS world.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class SkeletonAnimationConverter : MonoLeoEcsConverter
    {
        [SerializeField]
        private SkeletonAnimation skeletonAnimation;
        [SpineSkin, SerializeField]
        private string templateSkinName;

        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var skeletonAnimationComponent = ref world.AddComponent<SkeletonAnimationComponent>(entity);
            skeletonAnimationComponent.SkeletonAnimation = skeletonAnimation;
        }
    }
}