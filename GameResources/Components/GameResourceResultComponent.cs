namespace UniGame.Ecs.Proto.GameResources.Components
{
    using System;
    using Core.Runtime;
    using Object = UnityEngine.Object;

    [Serializable]
    public struct GameResourceResultComponent
    {
        /// <summary>
        /// Идентификатор по которому был загружен ресурс
        /// </summary>
        public string ResourceId;

        /// <summary>
        /// загруженный ресурс
        /// </summary>
        public object Resource;

        /// <summary>
        /// ожидаемое время жизни объекта
        /// </summary>
        public ILifeTime LifeTime;
    }
}