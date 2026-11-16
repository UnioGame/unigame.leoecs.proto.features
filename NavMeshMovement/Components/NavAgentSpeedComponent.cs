namespace UniGame.Ecs.Proto.Movement.Components
{
    using System;

    /// <summary>
    /// Скорость движения, которую мы выставляем NavMeshAgent.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct NavAgentSpeedComponent
    {
        public float Value;
    }
}