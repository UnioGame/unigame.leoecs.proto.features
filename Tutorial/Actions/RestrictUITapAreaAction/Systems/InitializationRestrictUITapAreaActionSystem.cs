namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.RestrictUITapAreaAction.Systems
{
    using System;
    using System.Collections.Generic;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class InitializationRestrictUITapAreaActionSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private RestrictUITapAreaActionAspect _aspect;
        private List<ProtoPackedEntity> _restrictTapAreas = new();

        private ProtoItExc _filter = It
            .Chain<RestrictUITapAreaActionComponent>()
            .Exc<RestrictUITapAreaActionReadyComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                _restrictTapAreas.Clear();
                ref var restrictTapAreaComponent = ref _aspect.RestrictUITapAreaAction.Get(entity);

                var areas = restrictTapAreaComponent.Areas;

                for (var i = 0; i < areas.Count; i++)
                {
                    var restrictEntity = _world.NewEntity();
                    var area = areas[i];
                    if (i == 0)
                    {
                        _aspect.ActivateRestrictUITapArea.Add(restrictEntity);
                    }
                    else
                    {
                        _restrictTapAreas.Add(_world.PackEntity(restrictEntity));
                    }

                    ref var restrictTapArea = ref _aspect.RestrictUITapArea.Add(restrictEntity);
                    restrictTapArea.Value = area;
                }

                ref var restrictUITapAreas = ref _aspect.RestrictUITapAreas.Add(entity);
                restrictUITapAreas.Value = new Queue<ProtoPackedEntity>(_restrictTapAreas);
                _aspect.RestrictUITapAreaActionReady.Add(entity);
            }
        }
    }
}