namespace UniGame.Ecs.Proto.Movement.Components
{
    using System;
    using Unity.Mathematics;

    /// <summary>
    /// Точка, к которой агент идёт «до упора»: каждый кадр снова отправляет его туда, пока он не подойдёт
    /// ближе 0.1 м. Потом компонент снимается.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ComePointComponent
    {
        public float3 Value;
    }
}