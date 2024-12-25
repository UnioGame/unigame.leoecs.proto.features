namespace UniGame.Ecs.Proto.Characteristics.Base.Components
{
    using System;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif
    
    /// <summary>
    /// characteristics marker
    /// </summary>
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct CharacteristicComponent<TType>
    {
        public float Value;
        public float BaseValue;
        public float MinValue;
        public float MaxValue;
    }
    
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct CharacteristicComponent
    {
        public float Value;
        public float BaseValue;
        public float MinValue;
        public float MaxValue;
    }
}