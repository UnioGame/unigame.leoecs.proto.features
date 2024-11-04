namespace Game.Ecs.SpineAnimation.Converters
{
    using System;
    using Components;
    using Data.EventType;
    using Leopotam.EcsProto;
    using Spine.Unity;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniModules.UniGame.Core.Runtime.DataStructure;
    using UnityEngine;

    /// <summary>
    /// Converts a Spine Animation Events to entity in ECS world.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class SpineAnimationEventsConverter : MonoLeoEcsConverter
    {
        [SerializeField]
        public SerializableDictionary<EventTypeId, EventDataReferenceAsset> events = new();
        
        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var spineAnimationComponent = ref world.AddComponent<SpineAnimationEventsComponent>(entity);
            spineAnimationComponent.Events = events;
        }
    }
}