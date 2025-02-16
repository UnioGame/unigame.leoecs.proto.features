namespace Game.Modules.SequenceActions.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

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
    public class UpdateSequenceSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        private SequenceActionAspect _sequenceAspect;
        
        private ProtoItExc _updateSequenceFilter = It
            .Chain<SequenceActionComponent>()
            .Inc<SequenceDataComponent>()
            .Inc<SequenceActionProgressComponent>()
            .Exc<SequenceCompleteComponent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run()
        {

        }
    }
}