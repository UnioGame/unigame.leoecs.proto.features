namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Components
{
    using System;
    using UniGame.LeoEcs.Shared.Abstract;

    /// <summary>
    /// Настройка ИИ «иди к точкам интереса»: на каком расстоянии точка считается достигнутой.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct MoveByPoiComponent : IApplyableComponent<MoveByPoiComponent>
    {
        /// <summary>
        /// Расстояние после которого будет считаться, что достигли цели
        /// </summary>
        public float ReachRange;
        
        public void Apply(ref MoveByPoiComponent component)
        {
            component.ReachRange = ReachRange;
        }
    }
}