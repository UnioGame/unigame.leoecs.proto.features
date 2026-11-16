namespace UniGame.Ecs.Proto.Effects.Components
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Заспавненный визуал эффекта и время, когда его убрать.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct EffectViewComponent
    {
        public GameObject ViewInstance;
        public float DeadTime;
    }
}