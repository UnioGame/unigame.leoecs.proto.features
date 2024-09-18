namespace UniGame.Ecs.Proto.Characteristics.Base.Systems
{
    using System;
    using Aspects;
    using Components;
    using Components.Requests;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    /// <summary>
    /// get RecalculateCharacteristicSelfRequest and mark characteristic as changed
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class MarkCharacteristicAsChangedSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private CharacteristicsAspect _characteristicsAspect;
        private ModificationsAspect _modificationsAspect;
        
        private ProtoItExc _filter= It
            .Chain<RecalculateCharacteristicSelfRequest>()
            .Inc<CharacteristicValueComponent>()
            .Inc<CharacteristicBaseValueComponent>()
            .Exc<CharacteristicChangedComponent>()
            .End();
        
        private ProtoPool<CharacteristicChangedComponent> _changedPool;
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                _characteristicsAspect.Changed.Add(entity);
            }
        }
    }
}