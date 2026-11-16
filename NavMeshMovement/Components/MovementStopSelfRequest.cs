namespace UniGame.Ecs.Proto.Movement.Components
{
    using System;

    /// <summary>
    /// Запрос «стоп»: агент останавливается на месте (isStopped). Новый запрос движения снимает остановку.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct MovementStopSelfRequest
    {
    }
}