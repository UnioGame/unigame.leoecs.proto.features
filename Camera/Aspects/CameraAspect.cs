namespace UniGame.Ecs.Proto.Camera.Aspects
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;

    /// <summary>
    /// camera components
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class CameraAspect : EcsAspect
    {
        public ProtoPool<CameraComponent> Camera;
        public ProtoPool<CameraFollowTargetComponent> FollowTarget;
        public ProtoPool<CameraLookTargetComponent> LookAt;
    }
}