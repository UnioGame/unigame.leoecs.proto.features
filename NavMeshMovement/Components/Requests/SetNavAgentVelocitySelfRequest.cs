namespace UniGame.Ecs.Proto.Movement.Components
{
    using System;
    using Unity.Mathematics;

    /// <summary>
    /// Запрос задать агенту скорость напрямую, мимо пункта назначения.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SetNavAgentVelocitySelfRequest
    {
        public float3 Value;
    }
}