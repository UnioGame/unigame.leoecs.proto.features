namespace UniGame.Ecs.Proto.Effects.Systems
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CP
    using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    [Serializable]
    [ECSDI]
    public sealed class DestroyEffectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;

        private ProtoIt _filter = It
            .Chain<EffectComponent>()
            .Inc<DestroyEffectSelfRequest>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                _world.DelEntity(entity);
            }
        }
    }
}