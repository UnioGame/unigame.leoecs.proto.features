namespace UniGame.Ecs.Proto.GameResources.Components
{
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;


    /// <summary>
    /// Компонент указывает, что над данной энтити ведется работа по загрузке ресурсов
    /// </summary>
    public struct GameResourceTaskComponent : IProtoAutoReset<GameResourceTaskComponent>
    {
        /// <summary>
        /// источник реквеста
        /// </summary>
        public ProtoPackedEntity RequestOwner;

        /// <summary>
        /// адрес ресурса
        /// </summary>
        public string Resource;

        /// <summary>
        /// время загрузки
        /// </summary>
        public float LoadingDuration;

        /// <summary>
        /// Время начало загрузки
        /// </summary>
        public float LoadingStartTime;
        
        public void AutoReset(ref GameResourceTaskComponent c)
        {
            c.LoadingDuration = 0;
            c.LoadingStartTime = 0;
            c.RequestOwner = new ProtoPackedEntity();
            c.RequestOwner = new ProtoPackedEntity();
            c.Resource = string.Empty;
        }
    }
}