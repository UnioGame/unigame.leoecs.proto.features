namespace UniGame.Ecs.Proto.Presets.Components
{
    using System;

    /// <summary>
    /// duration of preset applying
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct PresetDurationComponent
    {
        public float Value;
    }
}