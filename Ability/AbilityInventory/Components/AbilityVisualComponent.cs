namespace UniGame.Ecs.Proto.AbilityInventory.Components
{
    using System;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    /// <summary>
    /// Ability visual description
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityVisualComponent
    {
        public string Name;
        public string Description;
        public string ManaCost;
        public AssetReferenceT<Sprite> Icon;
    }
}