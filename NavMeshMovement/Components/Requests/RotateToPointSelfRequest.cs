namespace UniGame.Ecs.Proto.Movement.Components
{
    using System;
    using Unity.Mathematics;

    /// <summary>
    /// Запрос развернуться к точке. Поворот только по горизонтали, вокруг оси Y. Живёт один кадр.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct RotateToPointSelfRequest
    {
        public float3 Point;
    }
}