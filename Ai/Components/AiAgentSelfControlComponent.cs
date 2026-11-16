namespace UniGame.Ecs.Proto.AI.Components
{
    using System;

    /// <summary>
    /// Агентом управляют вручную (отладка): ИИ не перепланирует его действия сам.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AiAgentSelfControlComponent
    {
    }
}
