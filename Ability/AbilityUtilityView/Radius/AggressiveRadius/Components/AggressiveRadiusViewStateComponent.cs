namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Radius.AggressiveRadius.Components
{
    using System;
    using System.Collections.Generic;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    ///
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AggressiveRadiusViewStateComponent : IProtoAutoReset<AggressiveRadiusViewStateComponent>
    {
        private List<ProtoPackedEntity> _entities;
        private List<ProtoPackedEntity> _previousEntities;

        public IReadOnlyList<ProtoPackedEntity> Entities => _entities;
        public IReadOnlyList<ProtoPackedEntity> PreviousEntities => _previousEntities;

        public void SetEntities(IEnumerable<ProtoPackedEntity> entities)
        {
            _previousEntities.Clear();
            _previousEntities.AddRange(Entities);
            
            _entities.Clear();
            _entities.AddRange(entities);
        }
        
        public void SetHandlers(IProtoPool<AggressiveRadiusViewStateComponent> pool) => pool.SetResetHandler(AutoReset);

        public static void AutoReset(ref AggressiveRadiusViewStateComponent c)
        {
            c._entities ??= new List<ProtoPackedEntity>();
            c._entities.Clear();
            
            c._previousEntities ??= new List<ProtoPackedEntity>();
            c._previousEntities.Clear();
        }
    }
}