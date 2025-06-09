namespace Game.Editor.Runtime.CharacteristicsViewer
{
    using System;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.Core.Runtime;
    using UniGame.Ecs.Proto.Characteristics.Base.Components;
    using UniGame.Ecs.Proto.Characteristics.Base.Components.Requests;
    using UniGame.LeoEcs.Shared.Extensions;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
#if ODIN_INSPECTOR
    [InlineProperty]
    [HideLabel]
#endif
    [Serializable]

    public class CharacteristicDebugViewer<TCharacteristic> :
        EcsCharacteristicDebugView,
        ISearchFilterable
        where TCharacteristic : struct
    {

        private ProtoPool<CharacteristicComponent<TCharacteristic>> _pool;
        private ProtoWorld _world;
        private ProtoPackedEntity _packedEntity;

        public bool IsReady => _world != null && _world.IsAlive();
        
        public CharacteristicDebugViewer(string name,ProtoWorld world,ProtoEntity entity)
        {
            Name = name;
            
            _world = world;
            _packedEntity = entity.PackEntity(world);
            _pool = world.GetPool<CharacteristicComponent<TCharacteristic>>();
        }

        public override CharacteristicValue CreateView()
        {
            IsActive = false;
            var result = new CharacteristicValue();
            
            if (!IsReady) return result; 
            if(!_packedEntity.Unpack(_world,out var entity)) return result;
            IsActive = _pool.Has(entity);

            if (!IsActive) return result;
            
            var component = _pool.Get(entity);
            result.Value = component.Value;
            result.MaxValue = component.MaxValue;
            result.MinValue = component.MinValue;
            result.BaseValue = component.BaseValue;

            return result;
        }
        
        public override void Recalculate()
        {
            if (!_packedEntity.Unpack(_world, out var entity)) return;
            var requestComponent = _world.AddComponent<RecalculateCharacteristicSelfRequest<TCharacteristic>>(entity);
        }

        public bool IsMatch(string searchString)
        {
            if (string.IsNullOrEmpty(searchString)) return true;
            if (Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)) return true;
            if (typeof(TCharacteristic).Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)) return true;

            return false;
        }
    }
}