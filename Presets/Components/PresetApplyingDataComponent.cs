namespace UniGame.Ecs.Proto.Presets.Components
{
    using System;
    using Leopotam.EcsProto.QoL;
    
    /// <summary>
    /// data of applying process
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct PresetApplyingDataComponent
    {
        public ProtoPackedEntity Source;
        public float Duration;
        public float StartTime;
    }
}