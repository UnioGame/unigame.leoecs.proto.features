namespace UniGame.Ecs.Proto.Effects.Components
{
    using System;

    /// <summary>
    /// В какую точку на модели прицепить визуал этого эффекта.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct EffectRootIdComponent
    {
        public int Value;
    }
}