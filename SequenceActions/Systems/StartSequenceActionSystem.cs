namespace Game.Modules.SequenceActions.Systems
{
    using System;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class StartSequenceActionSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run()
        {

        }
    }
}