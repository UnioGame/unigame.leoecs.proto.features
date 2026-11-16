namespace UniGame.Ecs.Proto.Movement.Components
{
    using System;
    using Unity.Mathematics;

    /// <summary>
    /// Запрос «иди сюда»: NavMeshAgent получает эту точку как пункт назначения. Живёт один кадр.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct MovementPointSelfRequest
    {
        public float3 Value;
    }
}