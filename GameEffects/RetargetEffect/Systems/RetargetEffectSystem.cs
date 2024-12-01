namespace UniGame.Ecs.Proto.GameEffects.RetargetEffect.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UnityEngine;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Remove untargetable mark from target
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class RetargetEffectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private RetargetEffectAspect _aspect;

        private ProtoIt _filter = It
            .Chain<RetargetComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var retargetComponent = ref _aspect.RetargetComponent.Get(entity);
                if (retargetComponent.Value > Time.time)
                    continue;
                _aspect.RetargetComponent.Del(entity);
            }
        }
    }
}