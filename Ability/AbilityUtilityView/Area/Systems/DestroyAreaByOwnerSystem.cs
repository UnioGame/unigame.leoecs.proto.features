namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Area.Systems
{
    using System;
    using Aspects;
    using Components;
    using Game.Modules.leoecs.proto.tools.Ownership.Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Object = UnityEngine.Object;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class DestroyAreaByOwnerSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AbilityUtilityViewAspect _abilityUtilityViewAspect;

        private ProtoIt _filter = It
            .Chain<OwnerDestroyedEvent>()
            .Inc<AreaInstanceComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var areaInstance = ref _abilityUtilityViewAspect.AreaInstance.Get(entity);
                _abilityUtilityViewAspect.AreaInstance.Del(entity);
                
                if (areaInstance.Instance == null)
                    continue;
                
                Object.Destroy(areaInstance.Instance);
            }
        }
    }
}