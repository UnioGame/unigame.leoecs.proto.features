namespace UniGame.Ecs.Proto.Camera.Converters
{
    using Components;
    using LeoEcs.Converter.Runtime;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using UnityEngine;

    public sealed class CameraConverter : MonoLeoEcsConverter
    {
        public Camera Camera;
        
        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var cameraComponent = ref world.AddComponent<CameraComponent>(entity);
            cameraComponent.Camera = Camera;
        }
    }
}