namespace UniGame.Ecs.Proto.Effects.Components
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Transform, к которому прицепить визуал эффекта.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct EffectParentComponent
    {
        public Transform Value;
    }
}