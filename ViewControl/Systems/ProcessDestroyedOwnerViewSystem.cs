namespace UniGame.Ecs.Proto.ViewControl.Systems
{
    using System;
    using Components;
    using Game.Ecs.Core.Death.Aspects;
    using UniGame.Proto.Ownership;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessDestroyedOwnerViewSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private DestroyAspect _destroyAspect;

        private ProtoIt _filter = It
            .Chain<ViewInstanceComponent>()
            .Inc<OwnerDestroyedEvent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                _destroyAspect.Kill.GetOrAddComponent(entity);
                _world.DelEntity(entity);
            }
        }
    }
}