namespace UniGame.Ecs.Proto.GameResources.Components
{
    using System;
    using Core.Runtime;
    using Data;
    using Game.Code.DataBase.Runtime;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Abstract;
    using UnityEngine;

    /// <summary>
    /// Spawn Request for some game world entity
    /// Это OneShot компонент, он будет убит после обработкой систему спавна
    /// </summary>
    [Serializable]
    public struct GameResourceSpawnRequest : 
        IApplyableComponent<GameResourceSpawnRequest>, 
        IProtoAutoReset<GameResourceSpawnRequest>
    {
        /// <summary>
        /// id of game entity in game database
        /// </summary>
        public string ResourceId;

        /// <summary>
        /// link to source entity
        /// </summary>
        public ProtoPackedEntity Source;

        /// <summary>
        /// entity target for spawn
        /// </summary>
        public ProtoPackedEntity Target;
        
        /// <summary>
        /// parent entity for spawn
        /// </summary>
        public ProtoPackedEntity ParentEntity;
        
        /// <summary>
        /// spawn location data
        /// </summary>
        public GamePoint LocationData;

        /// <summary>
        /// spawn parent object
        /// </summary>
        public Transform Parent;

        /// <summary>
        /// entity lifeTime
        /// </summary>
        public ILifeTime LifeTime;

        public void Apply(ref GameResourceSpawnRequest data)
        {
            data.ResourceId = ResourceId;
            data.Source = Source;
            data.LocationData = LocationData;
            data.Parent = Parent;
            data.Target = Target;
            data.ParentEntity = ParentEntity;
            data.LifeTime = LifeTime;
        }

        public void AutoReset(ref GameResourceSpawnRequest c)
        {
            c.ResourceId = (GameResourceRecordId)string.Empty;
            c.Parent = default;
            c.LocationData = GamePoint.Zero;
            c.Source = default;
            c.Target = default;
            c.ParentEntity = default;
            c.LifeTime = default;
        }
    }
}