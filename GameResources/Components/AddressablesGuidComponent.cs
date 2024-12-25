namespace UniGame.Ecs.Proto.GameResources.Components
{
    using System;

    /// <summary>
    /// Component containing a unique string identifier (GUID)
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AddressablesGuidComponent
    {
        public string Guid;
    }
}