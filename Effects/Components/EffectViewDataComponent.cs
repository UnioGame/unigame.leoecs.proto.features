namespace UniGame.Ecs.Proto.Effects.Components
{
    using System;
    using Data;
    using Game.Code.Configuration.Runtime.Effects;
    using UnityEngine.AddressableAssets;

    /// <summary>
    /// Что показать для эффекта и как: префаб визуала, сколько он живёт и куда его прицепить.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct EffectViewDataComponent
    {
        public AssetReferenceGameObject View;
        public float LifeTime;
        public ViewInstanceType ViewInstanceType;
        public bool UseEffectRoot;
        public EffectRootId EffectRoot;
        public bool AttachToSource;
    }
}