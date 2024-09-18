namespace UniGame.Ecs.Proto.GameResources.Components
{
    using System;
    using Core.Runtime;
    using Leopotam.EcsProto.QoL;

    [Serializable]
    public struct GameResourceHandleComponent
    {
        /// <summary>
        /// источник реквеста
        /// </summary>
        public ProtoPackedEntity Source;

        /// <summary>
        /// адрес ресурса
        /// </summary>
        public string Resource;

        /// <summary>
        /// object lifetime
        /// </summary>
        public ILifeTime LifeTime;
    }
    
    
    [Serializable]
    public struct GameResourceHandleComponent<TAsset>
    {
        /// <summary>
        /// источник реквеста
        /// </summary>
        public ProtoPackedEntity RequestOwner;
        
        /// <summary>
        /// Владелец ресурса. Может быть пустым
        /// </summary>
        public ProtoPackedEntity ResourceOwner;

        /// <summary>
        /// адрес ресурса
        /// </summary>
        public string Resource;
    }
}
