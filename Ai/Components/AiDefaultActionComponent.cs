namespace UniGame.Ecs.Proto.AI.Components
{
    using System;

    /// <summary>
    /// Действие ИИ по умолчанию, когда больше делать нечего.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AiDefaultActionComponent
    {
    
    }
}
