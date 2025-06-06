namespace UniGame.Ecs.Proto.Effects.Components
{
    using LeoEcs.Proto;
    using Leopotam.EcsProto;


    /// <summary>
    /// Компонент длительности эффекта.
    /// </summary>
    public struct EffectDurationComponent : IProtoAutoReset<EffectDurationComponent>
    {
        /// <summary>
        /// Длительность эффекта.
        /// </summary>
        public float Duration;
        
        /// <summary>
        /// Время создания эффекта. Если <see cref="CreatingTime"/> + <see cref="Duration"/>
        /// больше или равен текущему времени, то эффект будет уничтожен.
        /// </summary>
        public float CreatingTime;
        
        public void SetHandlers(IProtoPool<EffectDurationComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref EffectDurationComponent c)
        {
            c.Duration = 0.0f;
            c.CreatingTime = float.MinValue;
        }
    }
}