namespace UniGame.Ecs.Proto.Effects.Components
{
    using System;

    /// <summary>
    /// Запрос «сработай сейчас»: на это реагируют системы конкретных эффектов (урон, лечение…). Живёт один
    /// кадр.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ApplyEffectSelfRequest
    {
    }
}