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
        public string ResourceId;
        public GamePoint LocationData;
        public Transform Parent;
        public ILifeTime LifeTime;
        public bool SetActive;

        public void Apply(ref GameResourceSpawnRequest data)
        {
            data.ResourceId = ResourceId;
            data.LocationData = LocationData;
            data.Parent = Parent;
            data.LifeTime = LifeTime;
            data.SetActive = SetActive;
        }

        public void AutoReset(ref GameResourceSpawnRequest c)
        {
            c.ResourceId = (GameResourceRecordId)string.Empty;
            c.Parent = default;
            c.LocationData = GamePoint.Zero;
            c.LifeTime = default;
            c.SetActive = false;
        }
    }
}