namespace UniGame.Ecs.Proto.AI.Components
{
    using System;

    /// <summary>
    /// Как далеко агент «видит» вокруг себя.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AiSensorComponent
    {
        public float Range;
    }
}