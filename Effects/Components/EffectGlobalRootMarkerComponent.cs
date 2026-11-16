namespace UniGame.Ecs.Proto.Effects.Components
{
    using System;

    /// <summary>
    /// Метка: сущность — общий контейнер, куда спавнятся визуалы эффектов, если у цели нет своего.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct EffectGlobalRootMarkerComponent
    {
        
    }
}