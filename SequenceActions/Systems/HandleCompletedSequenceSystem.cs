namespace Game.Modules.SequenceActions.Systems
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
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
    public class HandleCompletedSequenceSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        
        private ProtoIt _startSequenceFilter = It
            .Chain<SequenceCompleteComponent>()
            .Inc<SequenceComponent>()
            .Inc<SequenceAutoDestroyComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _startSequenceFilter)
            {
                _world.DelEntity(entity);
            }
        }
    }
}